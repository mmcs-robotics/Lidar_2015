using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoboProject.Robo;

namespace RoboProject
{
    class Program
    {
        static void Main(string[] args)
        {
            NxtRobot robot = new NxtRobot(4);
            for (int i = 0; i < 8; i++)
            {
                robot.RotateRight();
            }
            robot.Disconnect();
            //robot.Stop();
        }
    }
}
