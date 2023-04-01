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
            List<(int, int)> shape = new List<(int, int)>();

            void DrawFromTo(HPoint s, HPoint e)
            {
                var line = new Line(s, e);

                var linePixs = line.GetPixels();
                shape.AddRange(linePixs);
            }
            var pts = new List<HPoint>();
            var step = MathF.Tau / corners;
            for (float i = 0; i < float.Tau; i += step)
            {
                var alpha = i;
                alpha += step * rotate / 360 * float.Tau;
                pts.Add(new HPoint(Math.Cos(alpha) * radius, Math.Sin(alpha) * radius));
            }
            var xMove = pts.Select(p => p.X).Min();
            var yMove = pts.Select(p => p.Y).Min();
            pts = pts.Select(p => new HPoint(p.X - xMove, p.Y - yMove)).ToList();
            shape = pts.Select(Line.CeilVector).ToList();

            //var line = new Line(pts[0], pts[1]);
            //shape.AddRange(line.GetPixels());
            //line = new Line(pts[1], pts[2]);
            //shape.AddRange(line.GetPixels());
            //line = new Line(pts[2], pts[0]);
            //shape.AddRange(line.GetPixels());
            LinkedList<HPoint> vecs = new LinkedList<HPoint>(pts);
            var start = vecs.First;
            while (null != start.Next)
            {
                var end = start.Next.Value;
                DrawFromTo(start.Value, end);
                start = start.Next;
            }
            DrawFromTo(vecs.First.Value, vecs.Last.Value);
            OnNewSheme.Invoke(shape);
        }

    }
}
