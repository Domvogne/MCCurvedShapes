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
                int.TryParse(value, out width);
                if (width < right)
                    right = width;
                Rebuild();
            }
        }
        public string HeigthStr
        {
            get => heigh.ToString();
            set
            {
                int.TryParse(value, out heigh);
                if (bottom > heigh)
                    bottom = heigh;
                Rebuild();
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
            var shape = new List<(int, int)>();
            var len = Math.PI * Math.Sqrt((width * width + heigh * heigh) / 8);
            var grade = (Math.Tau / len) / 5;
            for (double i = 0; i <= Math.Tau; i += grade)
            {
                var sin = (int)(Math.Round((Math.Sin(i) * heigh) / 2) + heigh / 2);
                var cos = (int)(Math.Round((Math.Cos(i) * width) / 2) + width / 2);
                shape.Add((cos, sin));
            }
            shape = shape.Where(i => i.Item2 >= top && i.Item2 <= bottom && i.Item1 <= right && i.Item1 >= left).ToList();
            OnNewSheme.Invoke(shape);
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
