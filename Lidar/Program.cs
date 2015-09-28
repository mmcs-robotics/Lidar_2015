using System;
using System.Windows.Forms;
using Lidar.Settings;
using MapData.MapService;
using RoboProject;
using RoboProject.Robo;

namespace Lidar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            InitializableSingleton<Map>.Init(new Map());
            InitializableSingleton<INxtRobot>.Init(new NxtRobot(Config.RobotPort));
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
