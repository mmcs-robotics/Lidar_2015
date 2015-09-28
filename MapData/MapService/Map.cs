using System;
using System.Collections.Generic;
using System.Drawing;

namespace MapData.MapService
{
    public class Map
    {
        private readonly SortedDictionary<int, MapRow> _map;

        public static int MinIndex, MaxIndex;

        public MapRow this[int i]
        {
            get
            {
                UpdateMinMaxIndexes(i);
                if (!_map.ContainsKey(i))
                {
                    _map.Add(i, new MapRow());
                }
                return _map[i];
            }
            set
            {
                UpdateMinMaxIndexes(i);
                if (!_map.ContainsKey(i))
                {
                    _map.Add(i, new MapRow());
                }
                else
                {
                    _map[i] = value;
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

        public MapContainer this[int[] arr]
        {
            get
            {
                if (arr.Length < 2)
                {
                    throw new ArgumentException("arr should have 2 or more elements");
                }
                return this[arr[0]][arr[1]];
            }
        }

        public Map()
        {
            _map = new SortedDictionary<int, MapRow>();
        }

        public void Draw(Graphics graphics, int ppx)
        {
            for (int i = MinIndex; i < MaxIndex; i++)
            {
                if (_map.ContainsKey(i))
                {
                    _map[i].Draw(graphics, ppx);
                }
            }
        }
    }
}
