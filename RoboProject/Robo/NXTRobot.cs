using System;
using System.Diagnostics;
using System.Threading;
using MapData.MapService;
using NKH.MindSqualls;

namespace RoboProject.Robo
{
    public class NxtRobot : INxtRobot
    {
        private const uint RotateTachoLimit = 530;
        private const sbyte Power = 50;

        private NxtBrick _brick;
        private NxtMotorSync _motorPair;

        public INXTRobotPosition Position { get; set; }

        private void OnPositionChanged()
        {
            try
            {
                var map = InitializableSingleton<Map>.Instance;
                map[Position.Position].CountVisited++;
            }
            catch (Exception)
            {
                //ignore
            }
            
        }

        public bool IsRunning
        {
            get
            {
                var statusValues = _brick.CommLink.GetOutputState(NxtMotorPort.PortA);
                return statusValues.HasValue && statusValues.Value.runState == NxtMotorRunState.MOTOR_RUN_STATE_RUNNING;
            }
        }

        public NXTRobotDiraction Diraction
        {
            get { return Position.GetNXTRobotDiraction(); }
        }

        public NxtRobot(byte port)
        {
            try
            {
                Position = new NXTRobotPosition();
                OnPositionChanged();
                _brick = new NxtBrick(NxtCommLinkType.Bluetooth, port)
                {
                    MotorA = new NxtMotor(),
                    MotorB = new NxtMotor(),
                    MotorC = new NxtMotor()
                };
                _motorPair = new NxtMotorSync(_brick.MotorA, _brick.MotorB);
                _brick.Connect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Disconnect();
            }
        }

        public void MoveForward(int distance)
            //distance расчитывается в дм
        {
            if (_brick != null && _brick.IsConnected)
            {
                _motorPair.Run(Power, 0, 0);
                //за 1 секунду проходим 10 см
                Thread.Sleep(distance * 1000);
                _motorPair.Brake();
                Position.MoveForward(distance);
                OnPositionChanged();
            }
        }

        public void MoveBackward(int distance)
        //distance расчитывается в дм
        {
            if (_brick != null && _brick.IsConnected)
            {
                _motorPair.Run(-Power, 0, 0);
                //за 1 секунду проходим 10 см
                Thread.Sleep(distance * 1000);
                _motorPair.Brake();
                Position.MoveBackward(distance);
                OnPositionChanged();
            }
        }

        public void RotateLeft()
        {
            if (_brick != null && _brick.IsConnected)
            {
                _brick.MotorA.Run(Power, RotateTachoLimit);
                _brick.MotorB.Run(-Power, RotateTachoLimit);
                while (IsRunning)
                {
                    Thread.Sleep(100);
                }
                Position.RotateLeft();
                OnPositionChanged();
            }
        }

        public void RotateRight()
        {
            if (_brick != null && _brick.IsConnected)
            {
                _brick.MotorA.Run(-Power, RotateTachoLimit);
                _brick.MotorB.Run(Power, RotateTachoLimit);
                while (IsRunning)
                {
                    Thread.Sleep(100);
                }
                Position.RotateRight();
                OnPositionChanged();
            }
        }

        public void Idle()
        {
            if (_brick != null && _brick.IsConnected)
            {
                _motorPair.Idle();
            }
        }

        public void Disconnect()
        {
            if (_brick != null && _brick.IsConnected)
                _brick.Disconnect();

            _brick = null;
            _motorPair = null;
        }
    }
}
