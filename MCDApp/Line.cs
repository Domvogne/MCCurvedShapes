using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCDApp
{
    class Line
    {
        public HPoint from;
        public HPoint to;
        public Line(HPoint start, HPoint end)
        {
            from = start;
            to = end;
        }
        public List<IntPoint> GetPixels()
        {
            Debug.WriteLine($"Line started");
            var shape = new List<HPoint>();
            HPoint vector = to - from;
            if (Math.Abs(vector.X) >= Math.Abs(vector.Y))
            {
                for (int i = 0; i < Math.Abs(vector.X); i++)
                {
                    var k = i / Math.Abs(vector.X);
                    var currentPoint = vector * k;
                    shape.Add(currentPoint);
                }
            }
            else
            {
                for (int i = 0; i <= Math.Abs(vector.Y); i++)
                {
                    var k = i / Math.Abs(vector.Y);
                    var currentPoint = vector * k;
                    shape.Add(currentPoint);
                }
            }
            shape = shape.Select(i => i + from).ToList();
            shape.ForEach(i => Debug.WriteLine($"{i} -> {i.ToIntPoint()}"));
            var pts = shape.Select(i => i.ToIntPoint()).ToList();
            return pts;
        }

        public static List<IntPoint> GetPixelsTrign(double angle, double len)
        {
            Debug.WriteLine($"Line {angle} rads started");

            var shape = new List<HPoint>();
            var s = Math.Sin(angle);
            var c = Math.Cos(angle);
            var tg = s / c;
            var ctg = c / s;
            var until = Math.Max(s, c) * len;
            var k = Math.Min(tg, ctg);
            var range = Enumerable.Range(0, (int)until);
            foreach (var i in range)
            {
                shape.Add(new HPoint(i, i * Math.Min(s, c)));
            }
            if (s > c)
                shape = shape.Select(i => i.Resuffled()).ToList();
            shape.ForEach(i => Debug.WriteLine($"{i} -> {i.ToIntPoint()}"));
            var pix = shape.Select(i => i.ToIntPoint()).ToList();
            return pix;
        }
    }
}
