using System.Collections.Generic;

namespace MapData.MapService
{
    public class MapContainer
    {
        public List<MapPoint> Points { get; set; }

        public int CountVisited { get; set; }

        public MapContainer()
        {
            Points = new List<MapPoint>();
            CountVisited = 0;
        }
    }
}
