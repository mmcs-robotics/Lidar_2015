using System.Collections.Generic;
using System.Drawing;

namespace MapData.MapService
{
    public class MapRow
    {
        public static int MinIndex, MaxIndex;

        public MapRow()
        {
            _row = new SortedDictionary<int, MapContainer>();
        }

        private readonly SortedDictionary<int, MapContainer> _row;

        public MapContainer this[int i]
        {
            get
            {
                UpdateMinMaxIndexes(i);
                if (!_row.ContainsKey(i))
                {
                    _row.Add(i, new MapContainer());
                }
                return _row[i]; 
                
            }
            set
            {
                UpdateMinMaxIndexes(i);
                if (!_row.ContainsKey(i))
                {
                    _row.Add(i, value);
                }
                else
                {
                    _row[i] = value;
                }
            }
        }

        private void UpdateMinMaxIndexes(int i)
        {
            if (i < MinIndex)
            {
                MinIndex = i;
            }
            if (i > MaxIndex)
            {
                MaxIndex = i;
            }
        }

        private static readonly Brush Brush = new SolidBrush(Color.Black);

        public void Draw(Graphics graphics, int ppx)
        {
            for (int i = MinIndex; i < MaxIndex; i++)
            {
                if (_row.ContainsKey(i) && _row[i].Points.Count > 0)
                {
                    graphics.FillEllipse(Brush, (int) (20*_row[i].Points[0].X - Map.MinIndex + 20*ppx), (int)(20*_row[i].Points[0].Y - MinIndex + 20 * ppx), ppx, ppx);
                }
            }
        }
    }
}
