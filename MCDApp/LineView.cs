using System;
using System.Linq;

namespace MCDApp
{
    public class LineView : IFigureView
    {
        public event ShemeUpdate OnNewSheme;

        private double angle = 45;
        private int lenght = 10;

        public int Lenght
        {
            get { return lenght; }
            set
            {
                lenght = value;
                Rebuild();
            }
        }

        public int Angle
        {
            get { return (int)angle; }
            set
            {
                angle = value;
                Rebuild();
            }
        }


        public void Rebuild()
        {
            var endPoint = HPoint.Vector(angle == 90 ? Math.PI / 2 : angle / 360 * Math.Tau) * lenght;

            Line line = new Line(HPoint.Zero, endPoint);
            var shape = line.GetPixels();
            shape = shape.Select(i => new IntPoint(i.X, -i.Y)).ToList();
            var yMove = shape.Select(p => p.Y).Min();
            shape = shape.Select(p => new IntPoint(p.X, p.Y - yMove)).ToList();
            OnNewSheme.Invoke(shape);
        }
    }
}
