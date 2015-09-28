namespace RoboProject.Robo
{
    public interface INXTRobotPosition
    {
        void MoveForward(int distance);

        void MoveBackward(int distance);

        void RotateRight();

        void RotateLeft();

        int[] GetObjectPosition(double distance);

        NXTRobotDiraction GetNXTRobotDiraction();

        int[] Position { get; }
    }
}
