using System;
using System.Linq;

namespace RoboProject.Robo
{
    public class NXTRobotPosition : INXTRobotPosition
    {
        private readonly int[] _diraction;

        public static readonly int[] LeftDiraction = {0, -1};
        public static readonly int[] RightDiraction = { 0, 1 };
        public static readonly int[] ForwardDiraction = { 1, 0 };
        public static readonly int[] BackwardDiraction = { -1, 0 };

        public static readonly NXTRobotDiraction[] AllDiractions =
        {
            NXTRobotDiraction.Forward, NXTRobotDiraction.Right,
            NXTRobotDiraction.Backward, NXTRobotDiraction.Left,
        };

        public int[] Position { get; private set; }

        public NXTRobotPosition()
        {
            Position = new int[2];
            _diraction = new int[2];
            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] = 0;
            }
            _diraction[0] = 1;
            _diraction[1] = 0;
        }

        public NXTRobotPosition(int[] position, int[] diraction)
        {
            Position = position;
            _diraction = diraction;
        }

        public void MoveForward(int distance)
        {
            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] += (int)Math.Round((double) (distance*_diraction[i]));
            }
        }

        public void MoveBackward(int distance)
        {
            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] -= (int)Math.Round((double) (distance * _diraction[i]));
            }
        }

        public void RotateRight()
        {
            double x = _diraction[0];
            double y = _diraction[1];
            _diraction[0] = (int) Math.Round(x*Math.Cos(-Math.PI/2) - y * Math.Sin(-Math.PI/2));
            _diraction[1] = (int) Math.Round(x*Math.Sin(-Math.PI/2) + y*Math.Cos(-Math.PI/2));
        }

        public void RotateLeft()
        {
            double x = _diraction[0];
            double y = _diraction[1];
            _diraction[0] = (int) Math.Round(x * Math.Cos(Math.PI/2) - y * Math.Sin(Math.PI/2));
            _diraction[1] = (int) Math.Round(x * Math.Sin(Math.PI/2) + y * Math.Cos(Math.PI/2));
        }

        public int[] GetObjectPosition(double distance)
        {
            int[] position = new int[2];
            for (int i = 0; i < _diraction.Length; i++)
            {
                position[i] = (int)Math.Round(Position[i] + distance*_diraction[i]);
            }

            return position;
        }

        public static int[] GetDiractionValue(NXTRobotDiraction diraction)
        {
            switch (diraction)
            {
                case NXTRobotDiraction.Right:
                    return RightDiraction;
                case NXTRobotDiraction.Left:
                    return LeftDiraction;
                case NXTRobotDiraction.Forward:
                    return ForwardDiraction;
                case NXTRobotDiraction.Backward:
                    return BackwardDiraction;
                default:
                    throw new ArgumentException("Unknown diraction");
            }
        }

        public static NXTRobotDiraction GetNxtRobotDiraction(int[] diraction)
        {
            if (EqualArray(diraction, RightDiraction))
            {
                return NXTRobotDiraction.Right;
            }
            if (EqualArray(diraction, LeftDiraction))
            {
                return NXTRobotDiraction.Left;
            }
            if (EqualArray(diraction, ForwardDiraction))
            {
                return NXTRobotDiraction.Forward;
            }
            if (EqualArray(diraction, BackwardDiraction))
            {
                return NXTRobotDiraction.Backward;
            }

            throw new ArgumentException("Unknown diraction");
        }

        private static bool EqualArray<T>(T[] arr1, T[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                throw new ArgumentException("Lengths of arrays not ewual");
            }

            return !arr1.Where((t, i) => !t.Equals(arr2[i])).Any();
        }

        public NXTRobotDiraction GetNXTRobotDiraction()
        {
            if (EqualArray(_diraction, RightDiraction))
            {
                return NXTRobotDiraction.Right;
            }
            if (EqualArray(_diraction, LeftDiraction))
            {
                return NXTRobotDiraction.Left;
            }
            if (EqualArray(_diraction, ForwardDiraction))
            {
                return NXTRobotDiraction.Forward;
            }
            if (EqualArray(_diraction, BackwardDiraction))
            {
                return NXTRobotDiraction.Backward;
            }

            throw new ArgumentException("Unknown diraction");
        }
    }
}
