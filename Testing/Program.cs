using System.Drawing;

namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int n = int.Parse(Console.ReadLine());
            float R = float.Parse(Console.ReadLine());
            float rot = float.Parse(Console.ReadLine());
            Bitmap bm = new Bitmap(300, 300);
            var g = Graphics.FromImage(bm);
            var pts = new List<PointF>();
            var step = MathF.Tau / n;
            for(float i = 0; i < float.Tau; i += step)
            {
                float a = i + step / 2;
                pts.Add(new PointF(MathF.Cos(a) * R, MathF.Sin(a) * R));
            }
            var xMove = pts.Select(p => p.X).Min();
            var yMove = pts.Select(p => p.Y).Min();
            pts = pts.Select(p => new PointF(p.X - xMove, p.Y - yMove)).ToList();
            g.DrawPolygon(new Pen(Color.Black, 1), pts.ToArray());
            g.Save();
            bm.Save("out.bmp");
        }
    }
}