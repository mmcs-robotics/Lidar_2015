namespace RoboProject.Robo
{
    public interface INxtRobot
    {
        bool IsRunning { get; }

        INXTRobotPosition Position { get; set; }

        NXTRobotDiraction Diraction { get; }

        void MoveForward(int distance);

        void MoveBackward(int distance);

        void RotateLeft();

        void RotateRight();

        void Idle();

        void Disconnect();
    }
}
