using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MCDApp
{
    internal class PolygonView : IFigureView
    {
        public event ShemeUpdate OnNewSheme;
        private int corners = 3;
        private int radius = 5;
        private int rotate;

        public int Rotate
        {
            get { return rotate; }
            set
            {
                rotate = value;
                Rebuild();
            }
        }

        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                Rebuild();
            }
        }

        public int Corners
        {
            get { return corners; }
            set
            {
                corners = value;
                Rebuild();
            }
        }

        public void Rebuild()
        {
            List<IntPoint> shape = new List<IntPoint>();
            void DrawFromTo(HPoint s, HPoint e)
            {
                var line = new Line(s, e);

                var linePixs = line.GetPixels();
                shape.AddRange(linePixs);
            }
            var len = radius - 1;
            double alpha = Math.Tau / corners;
            var start = new HPoint();
            for (int i = 0; i < corners; i++) {
                double a = alpha * i;
                var vec = new HPoint(Math.Cos(a), Math.Sin(a)) * len + start;
                vec.Round();
                DrawFromTo(start, vec);
                start = vec;
            }
            var xMove = shape.Select(p => p.X).Min();
            var yMove = shape.Select(p => p.Y).Min();
            shape = shape.Select(p => new IntPoint(p.X - xMove, p.Y - yMove)).ToList();
            //var line = new Line(pts[0], pts[1]);
            //shape.AddRange(line.GetPixels());
            //line = new Line(pts[1], pts[2]);
            //shape.AddRange(line.GetPixels());
            //line = new Line(pts[2], pts[0]);
            //shape.AddRange(line.GetPixels());
            //LinkedList<HPoint> vecs = new LinkedList<HPoint>(pts);
            //var start = vecs.First;
            //while (null != start.Next)
            //{
            //    var end = start.Next.Value;
            //    DrawFromTo(start.Value, end);
            //    start = start.Next;
            //}
            //DrawFromTo(vecs.First.Value, vecs.Last.Value);
            OnNewSheme.Invoke(shape);
        }

    }
}
