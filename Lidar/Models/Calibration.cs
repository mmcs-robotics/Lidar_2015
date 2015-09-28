using System;

namespace Lidar.Models
{
    public class Calibration
    {
        public int From { get; set; }

        public int To { get; set; }

        public double Radians { get; set; }

        public double RadiansInPixel
        {
            get { return Radians/Math.Abs(From - To); }
        }
    }
}
