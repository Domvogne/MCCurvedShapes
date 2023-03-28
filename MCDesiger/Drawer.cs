using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MCDesiger
{
    internal class Drawer
    {
        public static void DrawList(List<Vector2> vs, char c = '#')
        {
            Console.Clear();
            Vector2 move = new Vector2(vs.Select(i => i.X).Min(), vs.Select(i => i.Y).Min()) * -1;
            foreach(Vector2 v in vs)
            {
                Console.SetCursorPosition((int)(v.X + move.X), (int)(v.Y + move.Y));
                Console.Write(c);
            }
        }
    }
}
