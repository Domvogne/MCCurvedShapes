using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDApp
{
    public static class Utils
    {
        public static double ToDegrees(this double value) => value / Math.Tau * 360;
    }
}
