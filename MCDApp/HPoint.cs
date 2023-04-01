using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDApp
{
    public struct HPoint
    {
        public double X, Y;

        public HPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static HPoint operator +(HPoint p1, HPoint p2) => new HPoint(p1.X + p2.X, p1.Y + p2.Y);
        public static HPoint operator -(HPoint p1, HPoint p2) => new HPoint(p1.X - p2.X, p1.Y - p2.Y);
        public static HPoint operator *(HPoint p1, double k) => new HPoint(p1.X * k, p1.Y * k);


    }
}
