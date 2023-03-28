using System;
using System.Collections.Generic;

namespace MCDApp
{
    public delegate void ShemeUpdate(List<(int, int)> points);
    class OvalView
    {
        int width = 10;
        int heigh = 10;
        int top = 100;
        int bottom = 100;
        int left = 100;
        int right = 100;
        public string WidthStr
        {
            get => width.ToString();
            set => int.TryParse(value, out width);
        }
        public string HeigthStr
        {
            get => heigh.ToString();
            set => int.TryParse(value, out heigh);
        }
        public int Width
        {
            get => width; set
            {
                width = value;
                Rebuild();
            }
        }
        public int Height
        {
            get => heigh; set
            {
                heigh = value;
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
            var len = 2 * Math.PI * Math.Sqrt((width * width + heigh * heigh) / 8);
            var grade = (Math.Tau / len) / 5;
            for (double i = 0; i <= Math.Tau; i += grade)
            {
                var sin = -(int)Math.Round((Math.Sin(i) * heigh));
                var cos = (int)Math.Round((Math.Cos(i) * width));
                shape.Add((cos, sin));
            }
            OnNewSheme.Invoke(shape);

        }
        public event ShemeUpdate OnNewSheme;
    }
}
