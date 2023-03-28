using System.Numerics;

namespace MCDesiger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var ls = new List<Vector2>();
            for (int i = -40; i < 40; i++)
            {
                ls.Add(new Vector2(i/4, MathF.Sqrt(1600 - i * i)));
            }
            Drawer.DrawList(ls);
            Console.ReadLine();
        }
    }
}