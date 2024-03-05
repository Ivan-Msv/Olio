using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Kordinaatisto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
            System.Console.InputEncoding = System.Text.Encoding.Unicode;


            RuudukkoKordinaatisto kordinaatti1 = new RuudukkoKordinaatisto(1, 0);
            RuudukkoKordinaatisto kordinaatti2 = new RuudukkoKordinaatisto(5, 0);
            RuudukkoKordinaatisto kordinaatti3 = new RuudukkoKordinaatisto(6, 0);

            bool vieressa1 = kordinaatti1.ViereinenPiste(kordinaatti2);
            bool vieressa2 = kordinaatti2.ViereinenPiste(kordinaatti3);

            Console.WriteLine($"Annettu kordinaatti {kordinaatti1.X},{kordinaatti1.Y} {(vieressa1 ? "on" : "ei ole")} kordinaatin {kordinaatti2.X},{kordinaatti2.Y} vieressä");
            Console.WriteLine($"Annettu kordinaatti {kordinaatti2.X},{kordinaatti2.Y} {(vieressa2 ? "on" : "ei ole")} kordinaatin {kordinaatti3.X},{kordinaatti3.Y} vieressä");
        }
    }
    public struct RuudukkoKordinaatisto
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public RuudukkoKordinaatisto(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool ViereinenPiste(RuudukkoKordinaatisto kordi)
        {
            bool xVieressa = MathF.Abs(X - kordi.X) == 1 && Y == kordi.Y;
            bool yVieressa = MathF.Abs(Y - kordi.Y) == 1 && X == kordi.X;

            return xVieressa || yVieressa;
        }
    }
}
