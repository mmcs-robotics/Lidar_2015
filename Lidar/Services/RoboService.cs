using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Lidar.Models;
using Lidar.Settings;
using OpenCvSharp;

namespace Lidar.Services
{
    public class RoboService
    {
        private static Dictionary<int, Calibration> _calibrations;

        public const double RadinsInPixel = Math.PI/180*45/640;

        public static Dictionary<int, Calibration> Calibrations
        {
            get
            {
                return _calibrations ??
                       (_calibrations = File.ReadAllLines(Config.FileCalibrations).SelectMany(s =>
                       {
                           var ss = s.Split(' ');
                           return new[]
                           {
                               new Calibration
                               {
                                   From = int.Parse(ss[0]),
                                   To = int.Parse(ss[1]),
                                   Radians = double.Parse(ss[2])
                               },
                               new Calibration
                               {
                                   From = 640 - int.Parse(ss[1]),
                                   To = 640 - int.Parse(ss[0]),
                                   Radians = double.Parse(ss[2])
                               }
                           };
                       }).OrderBy(c => c.To).ToDictionary(c => c.From, c => c));
            }
        }

        public static double GetDistance(CvPoint leftPoint, CvPoint rightPoint)
        {
            if (Math.Abs(rightPoint.Y - leftPoint.Y) > 50 || leftPoint.X < 320 && rightPoint.X > 320)
            {
                return double.NaN;
            }
            else
            {
                double hX, hY, angle1, angle2;
                if (rightPoint.X < 320 && leftPoint.X < 320)
                {
                    angle1 = GetAngle(rightPoint);
                    angle2 = GetAngle(leftPoint);
                    hX = GetXDistance(angle1, angle2);
                    hY = GetYDistance(angle1, hX);
                }
                else if (rightPoint.X < 320 && leftPoint.X >= 320)
                {
                    angle1 = GetAngle(rightPoint);
                    angle2 = GetAngle(leftPoint);
                    hX = GetXDistance(angle1, angle2, true);
                    var h = hX > Config.DistanceBetweenCameras / 2
                        ? Config.DistanceBetweenCameras / 2 - (Config.DistanceBetweenCameras - hX)
                        : Config.DistanceBetweenCameras / 2 - hX;
                    hY = GetYDistance(angle1, hX);
                }
                else
                {
                    angle1 = GetAngle(rightPoint);
                    angle2 = GetAngle(leftPoint);
                    hX = GetXDistance(angle2, angle1);
                    hY = GetYDistance(angle2, hX);
                }
                var e = Math.Sqrt(Math.Pow(hY, 2) + Math.Pow(hX, 2));
                var o = Math.Pow(e/20, 2);
                Debug.WriteLine("e = {0}, o = {1}", e, o);
                return (e > 136) ? double.PositiveInfinity : e - 1.05 * o;
            }
        }

        public static double GetYDistance(double alpha, double h)
        {
            return h/Math.Tan(alpha);
        }

        public static double GetAngle(CvPoint point)
        {
            return Math.Abs(320 - point.X) * RadinsInPixel;
        }

        private static double GetXDistance(double alpha1, double alpha2, bool betweenCams = false)
        {
            double tanAlpha1 = Math.Tan(alpha1);
            double tanAlpha2 = Math.Tan(alpha2);
            double denominator = betweenCams ? tanAlpha2 + tanAlpha1 : tanAlpha2 - tanAlpha1;
            return Config.DistanceBetweenCameras * tanAlpha1 / denominator;
        }

        
    }
}
