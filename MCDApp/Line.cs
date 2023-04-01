using System;
using System.Collections.Generic;
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
        public List<(int, int)> GetPixels()
        {
            var shape = new List<HPoint>();
            HPoint vector = to - from;
            if (Math.Abs(vector.X) > Math.Abs(vector.Y))
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
                for (int i = 0; i < Math.Abs(vector.Y); i++)
                {
                    var k = i / Math.Abs(vector.Y);
                    var currentPoint = vector * k;
                    shape.Add(currentPoint);
                }
            }
            shape = shape.Select(i => i + from).ToList();
            var pts = shape.Select(CeilVector).ToList();
            return pts;
        }
        public static (int, int) CeilVector(HPoint vector)
        {
            return ((int)Math.Ceiling(vector.X), (int)Math.Ceiling(vector.Y));
        }
    }
}
