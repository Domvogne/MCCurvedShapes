using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDApp
{
    struct IntPoint
    {
        public int X;
        public int Y;
        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static IntPoint operator +(IntPoint x, IntPoint y)
        {
            return new IntPoint(x.X + y.X, x.Y + y.Y);
        }
        public static IntPoint operator -(IntPoint x, IntPoint y)
        {
            return new IntPoint(x.X - y.X, x.Y - y.Y);
        }
    }
}
