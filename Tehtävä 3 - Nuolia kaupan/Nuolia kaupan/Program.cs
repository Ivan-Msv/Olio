using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuolia_kaupan
{
    class Program
    {
        enum Karki
        {
            Puu, Teräs, Timantti
        }
        enum Pera
        {
            Lehti, Kanansulka, Kotkansulka
        }
    static void Main(string[] args)
        {
            Console.Write($"Minkälainen kärki ({Karki.Puu}, {Karki.Teräs}, {Karki.Timantti})?: ");
            string karkivastaus = Console.ReadLine().ToString().ToLower();

            Console.Write($"Minkälaiset sulat ({Pera.Lehti}, {Pera.Kanansulka}, {Pera.Kotkansulka})?: ");
            string peravastaus = Console.ReadLine().ToString().ToLower();

            Console.Write("Nuolen pituus: (60-100cm): ");
            int nuolenpituus = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Tämän nuolen hinta on: {PalautaHinta(karkivastaus, peravastaus, nuolenpituus)}");
            Console.ReadKey();
        }
        static int PalautaHinta(string karkiv, string perav, int npituus)
        {
            int karkihinta;
            if (karkiv == Karki.Puu.ToString().ToLower())
            {
                karkihinta = 3;
            }
            else if (karkiv == Karki.Teräs.ToString().ToLower())
            {
                karkihinta = 5;
            }
            else
            {
                karkihinta = 50;
            }

            int perahinta;
            if (perav == Pera.Kanansulka.ToString().ToLower())
            {
                perahinta = 1;
            }
            else if (perav == Pera.Kotkansulka.ToString().ToLower())
            {
                perahinta = 5;
            }
            else
            {
                perahinta = 0;
            }

            int pituushinta = (int)(npituus * 0.05);
            int kokonaishinta = karkihinta + perahinta + pituushinta;
            return kokonaishinta;
        }
    }
}
