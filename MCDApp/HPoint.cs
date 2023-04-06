using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MCDApp
{
    public struct HPoint
    {

        public static readonly HPoint Zero = new HPoint(0, 0);
        public double X, Y;

        public HPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static HPoint operator +(HPoint p1, HPoint p2) => new HPoint(p1.X + p2.X, p1.Y + p2.Y);
        public static HPoint operator -(HPoint p1, HPoint p2) => new HPoint(p1.X - p2.X, p1.Y - p2.Y);
        public static HPoint operator *(HPoint p1, double k) => new HPoint(p1.X * k, p1.Y * k);

        public void Round()
        {
            X = Math.Round(X);
            Y = Math.Round(Y);
        }
        public static HPoint Vector(double angle)
        {
            return new HPoint(Math.Cos(angle), Math.Sin(angle));
        }
        public override string ToString()
        {
            return $"X:{Math.Round(X, 3)}; Y:{Math.Round(Y, 3)}";
        }
        public IntPoint ToIntPoint()
        {
            var ret = new IntPoint();
            ret.X = (int)Math.Ceiling(X);
            ret.Y = (int)Math.Ceiling(Y);
            return ret;
        }
        public double Lenght => Math.Sqrt(X * X + Y * Y);
        public HPoint Resuffled() => new HPoint(Y, X);
    }
}
