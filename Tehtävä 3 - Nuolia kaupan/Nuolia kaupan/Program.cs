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
            string karkivastaus;
            if (Enum.TryParse(Console.ReadLine(), true, out Karki sKarki))
            {
                karkivastaus = sKarki.ToString();
            }
            else
            {
                Console.WriteLine("Kärki ei löydy, käytetään Puu kärkeä.");
                karkivastaus = Karki.Puu.ToString();
            }

            Console.Write($"Minkälaiset sulat ({Pera.Lehti}, {Pera.Kanansulka}, {Pera.Kotkansulka})?: ");
            string peravastaus;
            if (Enum.TryParse(Console.ReadLine(), true, out Pera sPera))
            {
                peravastaus = sPera.ToString();
            }
            else
            {
                Console.WriteLine("Perä ei löydy, käytetään Lehti perää.");
                peravastaus = Pera.Lehti.ToString();
            }

            int nuolenpituus;
            while (true)
            {
                Console.Write("Nuolen pituus: (60-100cm): ");
                if (!int.TryParse(Console.ReadLine(), out var numero))
                {
                    continue;
                }
                else
                {
                    nuolenpituus = numero;
                }
                if (nuolenpituus <= 100 && nuolenpituus >= 60)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nuolen pituus pitäisi olla 60-100cm.");
                    continue;
                }
            }

            Console.Write($"Tämän nuolen hinta on: {PalautaHinta(karkivastaus, peravastaus, nuolenpituus)}");
            Console.ReadKey();
        }
        static int PalautaHinta(string karkiv, string perav, int npituus)
        {
            int karkihinta;
            if (karkiv == Karki.Puu.ToString())
            {
                karkihinta = 3;
            }
            else if (karkiv == Karki.Teräs.ToString())
            {
                karkihinta = 5;
            }
            else
            {
                karkihinta = 50;
            }

            int perahinta;
            if (perav == Pera.Kanansulka.ToString())
            {
                perahinta = 1;
            }
            else if (perav == Pera.Kotkansulka.ToString())
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
