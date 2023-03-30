  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace MCDApp
{
    public delegate void ShemeUpdate(List<(int, int)> points);
    class OvalView
    {
        int width = 10;
        int heigh = 10;
        int top = 0;
        int bottom = 10;
        int left = 0;
        int right = 10;
        public string WidthStr
        {
            get => width.ToString();
            set
            {
                if (int.TryParse(value, out width))
                {
                    if (width < right)
                        right = width;
                    Rebuild();
                }
            }
        }
        public string HeigthStr
        {
            get => heigh.ToString();
            set
            {
                if(int.TryParse(value, out heigh))
                {
                    if (bottom > heigh)
                        bottom = heigh;
                    Rebuild();
                }

            }
        }
        public int Width
        {
            get => width; set
            {
                width = value;
                if (width < right)
                    right = width;
                Rebuild();
            }
        }
        public int Heigth
        {
            get => heigh; set
            {
                heigh = value;
                if (bottom > heigh)
                    bottom = heigh;
                Rebuild();
            }
        }
        public int TopLimit
        {
            get => top; set
            {
                top = value;
                Rebuild();
            }
        }
        public int BottomLimit
        {
            get => bottom; set
            {
                bottom = value;
                Rebuild();
            }
        }
        public int LeftLimit
        {
            get => left; set
            {
                left = value;
                Rebuild();
            }
        }
        public int RightLimit
        {
            get => right; set
            {
                right = value;
                Rebuild();
            }
        }

        void Rebuild()
        {
            int RoundToInt(double value) => (int)Math.Round(value);
            var shape = new List<(int, int)>();
            var hW = RoundToInt(width / 2d);
            var hH = RoundToInt(heigh / 2d);
            List<int> yRange = Enumerable.Range(0, hH).ToList();
            void addNormalized(int x, int y)
            {
                shape.Add((x + RoundToInt(hW), y + RoundToInt(hH)));
            }
            int hWindex = hW - 1;
            int hHindex = hH - 1;
            for (int x = 0; x < hW; x++)
            {
                int y = (int)Math.Round(Math.Sqrt(1 - Math.Pow(x / (double)hWindex, 2)) * hHindex);
                shape.Add((x, y));
                yRange.Remove(y);
            }
            foreach (int y in yRange)
            {
                int x = (int)Math.Round(Math.Sqrt(1 - Math.Pow(y / (double)hHindex, 2)) * hWindex);
                shape.Add((x, y));
            }
            //var preshape = new List<(int, int)>();
            //shape.ForEach(i => preshape.Add(i));
            //preshape.ForEach(i => shape.Add((-i.Item1, i.Item2)));
            //shape.ForEach(i => preshape.Add(i));
            //preshape.ForEach(i => shape.Add((i.Item1, -i.Item2)));
            //shape.Distinct();
            //shape = shape.Select(i => (i.Item1 + hHindex, i.Item2 + hWindex)).ToList();
            shape = shape.Where(i => i.Item2 >= top && i.Item2 <= bottom && i.Item1 <= right && i.Item1 >= left).ToList();
            OnNewSheme.Invoke(shape);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event ShemeUpdate OnNewSheme;

        public double NormalizedLenth(double x, double y)
        {
            var hWidth = width / 2;
            var hHeith = heigh / 2;
            return new Vector2((float)(x / hWidth), (float)(y / hHeith)).Length();
        }
    }
}
