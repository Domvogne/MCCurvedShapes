using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace MCDApp
{


    class OvalView : IFigureView
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
                if (int.TryParse(value, out heigh))
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

        public event ShemeUpdate OnNewSheme;
        public double NormalizedLenth(double x, double y)
        {
            var hWidth = width / 2;
            var hHeith = heigh / 2;
            return new Vector2((float)(x / hWidth), (float)(y / hHeith)).Length();
        }

        public void Rebuild()
        {
            int RoundToInt(double value) => (int)Math.Round(value);
            var shape = new List<IntPoint>();
            var halfWidth = RoundToInt(width / 2);
            var halfHeight = RoundToInt(heigh / 2);
            List<int> yRange = Enumerable.Range(-halfHeight, heigh).ToList();
            var xRange = Enumerable.Range(-halfWidth, width).ToList();
            void addNormalized(int x, int y)
            {
                shape.Add(new IntPoint(x + RoundToInt(halfWidth), y + RoundToInt(halfHeight)));
            }
            bool evenWidth = width % 2 == 0;
            bool evenHeight = heigh % 2 == 0;
            int move = evenHeight ? 1 : 0;
            int maxH = halfHeight - move;
            foreach (var x in xRange)
            {
                double lX = x;
                lX = x > 0 && evenWidth ? x + 1 : x;
                int y = (int)Math.Ceiling(Math.Sqrt(1 - Math.Pow(lX / (double)halfWidth, 2)) * halfHeight);
                shape.Add(new IntPoint(x, y - move));
                shape.Add(new IntPoint(x, -y));
                yRange.Remove(y - move);
                yRange.Remove(-y);
            }
            move = width % 2 == 0 ? 1 : 0;
            foreach (var y in yRange)
            {
                double lY = y;
                lY = y > 0 && evenHeight ? y + 1 : y;
                int x = (int)Math.Ceiling(Math.Sqrt(1 - Math.Pow(lY / (double)halfHeight, 2)) * halfWidth);
                shape.Add(new IntPoint(x - move, y));
                shape.Add(new IntPoint(-x, y));
            }
            int maxW = halfWidth - move;
            shape = shape.Select(i => new IntPoint(i.X + halfWidth, i.Y + halfHeight)).ToList();
            shape = shape.Where(i => i.Y >= top && i.Y <= bottom && i.X <= right && i.X >= left).ToList();
            OnNewSheme.Invoke(shape);
        }
    }
}
