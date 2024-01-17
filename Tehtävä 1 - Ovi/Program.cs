using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovi
{
    class Program
    {
        enum OviTilat
        {
            Lukossa, Kiinni, Auki
        }
        static void Main(string[] args)
        {
            string ovi = OviTilat.Lukossa.ToString();
            while (true)
            {
                Console.WriteLine($"Ovi on {ovi}. Mitä haluat tehdä?");
                string vastaus = Console.ReadLine().ToLower();
                if (vastaus == "avaa lukko" && ovi == OviTilat.Lukossa.ToString())
                {
                    ovi = OviTilat.Kiinni.ToString();
                }
                else if (vastaus == "avaa" && ovi == OviTilat.Kiinni.ToString())
                {
                    ovi = OviTilat.Auki.ToString();
                }
                else if (vastaus == "sulje" && ovi == OviTilat.Auki.ToString())
                {
                    ovi = OviTilat.Kiinni.ToString();
                }
                else if (vastaus == "lukitse")
                {
                    if (ovi == OviTilat.Kiinni.ToString())
                    {
                        ovi = OviTilat.Lukossa.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Oven pitää ensin sulkea.");
                    }
                }
                else
                {
                    Console.WriteLine("Mitään ei tapahdu...");
                }
            }
        }
    }
}
