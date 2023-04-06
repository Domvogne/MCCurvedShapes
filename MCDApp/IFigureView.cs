using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDApp
{
    public delegate void ShemeUpdate(List<IntPoint> points);
    public delegate void ListUpdate();
    internal interface IFigureView
    {
        void Rebuild();
        public event ShemeUpdate OnNewSheme;
    }
}
