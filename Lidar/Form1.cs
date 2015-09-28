using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lidar.Services;
using Lidar.Settings;
using MapData.MapService;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
using RoboProject;
using RoboProject.Robo;
using Point = OpenCvSharp.CPlusPlus.Point;

namespace Lidar
{
    public partial class Form1 : Form
    {
        private readonly CvCapture _webCam1;
        private readonly CvCapture _webCam2;
        private readonly Mat _temp = new Mat("../../template.jpg");
        private Graphics g;
        private readonly INxtRobot _robot;
        private readonly Map _map;
        private bool IsStopped { get; set; }
        private const int MilisecondsSleep = 2000;
        private const int ScanningSleep = 200;

        public Form1()
        {
            InitializeComponent();
            resultMap.Image = new Bitmap(resultMap.Width, resultMap.Height);
            _robot = InitializableSingleton<INxtRobot>.Instance;
            _map = InitializableSingleton<Map>.Instance;
            _webCam1 = CvCapture.FromCamera(CaptureDevice.DShow, Config.LeftCamera);
            _webCam1.Brightness = 50;
            _webCam1.Contrast = 100;
            _webCam2 = CvCapture.FromCamera(CaptureDevice.DShow, Config.RightCamera);
            _webCam2.Brightness = 50;
            _webCam2.Contrast = 100;
            timer1.Start();
            IsStopped = false;
            Action roboScan = BuildMap;
            Task.Run(roboScan);
        }

        private double GetDistanceToObject()
        {
            Mat mat1 = new Mat(_webCam1.QueryFrame());
            CvPoint webCamPos1 = GetMaxLoc(mat1);
            Mat mat2 = new Mat(_webCam2.QueryFrame());
            CvPoint webCamPos2 = GetMaxLoc(mat2);
            return RoboService.GetDistance(webCamPos1, webCamPos2);
        }

        private void BuildMap()
        {
            FirstScanAround();
            while (!IsStopped)
            {
                RoboMoving();
                RoboScanningAround();
            }
        }

        private void FirstScanAround()
        {
            for (int i = 0; i < 4; i++)
            {
                RoboScanning();
                _robot.RotateRight();
            }
        }

        private void RoboScanningAround()
        {
            _robot.RotateLeft();
            RoboScanning();
            _robot.RotateRight();
            _robot.RotateRight();
            RoboScanning();
            _robot.RotateLeft();
        }

        private double GetDistance()
        {
            var res = new double[5];
            for (int i = 0; i < res.Length; i++)
            {
                Thread.Sleep(ScanningSleep);
                res[i] = GetDistanceToObject();
            }
            return res.Min() / 10;
        }

        private void RoboScanning()
        {
            Thread.Sleep(MilisecondsSleep);
            var distanceToObject = GetDistance();
            if (distanceToObject < 7)
            {
                var objectPosition = _robot.Position.GetObjectPosition(distanceToObject);
                _map[objectPosition].Points.Add(new MapPoint { X = objectPosition[0], Y = objectPosition[1], Z = 0 });
            }
        }

        private void RoboMoving()
        {
            var diraction = GetNextDiraction();
            var diff = diraction - _robot.Diraction;
            for (int i = 0; i < Math.Abs(diff); i++)
            {
                if (diff > 0)
                {
                    _robot.RotateRight();
                }
                else
                {
                    _robot.RotateLeft();
                }
            }
            _robot.MoveForward(1);
        }

        private NXTRobotDiraction GetNextDiraction()
        {
            var pos = _robot.Position.Position;
            var result =
                NXTRobotPosition.AllDiractions.Select(NXTRobotPosition.GetDiractionValue)
                    .Where(d => _map[pos[0] + d[0]][pos[1] + d[1]].Points.Count == 0 && _map[pos[0] + 2*d[0]][pos[1] + 2*d[1]].Points.Count == 0)
                    .OrderBy(d => _map[d].CountVisited)
                    .First();
            return NXTRobotPosition.GetNxtRobotDiraction(result);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mat mat1 = new Mat(_webCam1.QueryFrame());
            CvPoint webCamPos1 = GetMaxLoc(mat1);
            webCameraImage1.Image = mat1.ToBitmap();
            g = Graphics.FromImage(webCameraImage1.Image);
            g.DrawLine(new Pen(Color.Gold), webCamPos1.X + _temp.Width / 2, 0, webCamPos1.X + _temp.Width / 2, 480);
            g.DrawLine(new Pen(Color.Gold), 0, webCamPos1.Y + _temp.Height / 2, 640, webCamPos1.Y + _temp.Height / 2);

            
            Mat mat2 = new Mat(_webCam2.QueryFrame());
            CvPoint webCamPos2 = GetMaxLoc(mat2);
            webCameraImage2.Image = mat2.ToBitmap();
            g = Graphics.FromImage(webCameraImage2.Image);
            g.DrawLine(new Pen(Color.Gold), webCamPos2.X + _temp.Width / 2, 0, webCamPos2.X + _temp.Width / 2, 480);
            g.DrawLine(new Pen(Color.Gold), 0, webCamPos2.Y + _temp.Height / 2, 640, webCamPos2.Y + _temp.Height / 2);

            var d = RoboService.GetDistance(webCamPos1, webCamPos2);
            distance.Text = d.ToString(CultureInfo.InvariantCulture);
            var a = RoboService.GetAngle(webCamPos1);
        }

        private CvPoint GetMaxLoc(Mat mat)
        {
            Mat res = new Mat(Cv.Size(mat.Width - _temp.Width + 1, mat.Height - _temp.Height + 1), MatType.CV_32F);
            Cv2.MatchTemplate(mat, _temp, res, MatchTemplateMethod.CCoeffNormed);
            Point p1 = new Point();
            Point p2 = new Point();
            //Cv2.Normalize(res, res, 8, 0, NormType.MinMax);
            Cv2.MinMaxLoc(res, out p1, out p2);
            return p2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mat mat1 = new Mat(_webCam1.QueryFrame());
            mat1.WriteToStream(File.OpenWrite("leftCam.png"));

            Mat mat2 = new Mat(_webCam2.QueryFrame());
            mat2.WriteToStream(File.OpenWrite("rightCam.png"));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _robot.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsStopped = true;
            DrawMap();
        }

        private void DrawMap()
        {
            int ppx = Math.Max(5, Math.Min((Map.MaxIndex - Map.MinIndex)/resultMap.Width,
                (MapRow.MaxIndex - MapRow.MinIndex)/resultMap.Height));
            _map.Draw(Graphics.FromImage(resultMap.Image), ppx);
            resultMap.Invalidate();
        }
    }
}
