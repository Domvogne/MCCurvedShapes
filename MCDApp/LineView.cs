using System;

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
            //var endPoint = HPoint.Vector(angle / 360 * Math.Tau) * (lenght);

            //Line line = new Line(HPoint.Zero, endPoint);

            OnNewSheme.Invoke(Line.GetPixelsTrign(angle / 360 * Math.Tau, lenght));
        }
    }
}