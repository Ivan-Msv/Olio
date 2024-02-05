using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuolia_kaupan
{
    enum Karki
    {
        Puu, Teräs, Timantti
    }
    enum Pera
    {
        Lehti, Kanansulka, Kotkansulka
    }
    class Program
    {
    static void Main(string[] args)
        {
            Console.WriteLine("Hei, haluatko tehdä oman(O) nuolen vai käyttää valmiin(V) pohjan?");
            Nuoli uusinuoli;

            while (true)
            {
                ConsoleKey userinput = Console.ReadKey(true).Key;
                if (userinput == ConsoleKey.V)
                {
                    Console.Clear();
                    Console.WriteLine("Valitse seuraavista pohjista:");
                    Console.WriteLine("(A) Aloittelija Nuoli \n(B) Perus Nuoli \n(C) Eliitti Nuoli");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                            uusinuoli = Nuoli.LuoAloittelijanuoli();
                            break;
                        case ConsoleKey.B:
                            uusinuoli = Nuoli.LuoPerusNuoli();
                            break;
                        case ConsoleKey.C:
                            uusinuoli = Nuoli.LuoEliittiNuoli();
                            break;
                        default:
                            uusinuoli = Nuoli.LuoAloittelijanuoli();
                            Console.WriteLine("Nuolipohjaa ei löytynyt, käytetään Aloittelija Nuolta.");
                            break;
                    }
                    break;
                }
                else if (userinput == ConsoleKey.O)
                {
                    uusinuoli = TeeNuoliValinnat();
                    break;
                }
                else
                {
                    Console.WriteLine("Käytä O tai V kirjaimia.");
                }
            }
            Console.Write($"Tämän nuolen hinta on: {uusinuoli.PalautaHinta()}");
            Console.ReadKey();
        }
        private static Nuoli TeeNuoliValinnat()
        {
            Console.Write($"Minkälainen kärki ({Karki.Puu}, {Karki.Teräs}, {Karki.Timantti})?: ");
            Karki karkivastaus;
            if (Enum.TryParse(Console.ReadLine(), true, out Karki sKarki))
            {
                karkivastaus = sKarki;
            }
            else
            {
                Console.WriteLine("Kärki ei löydy, käytetään Puu kärkeä.");
                karkivastaus = Karki.Puu;
            }

            Console.Write($"Minkälaiset sulat ({Pera.Lehti}, {Pera.Kanansulka}, {Pera.Kotkansulka})?: ");
            Pera peravastaus;
            if (Enum.TryParse(Console.ReadLine(), true, out Pera sPera))
            {
                peravastaus = sPera;
            }
            else
            {
                Console.WriteLine("Perä ei löydy, käytetään Lehti perää.");
                peravastaus = Pera.Lehti;
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
            Nuoli uusinuoli = new Nuoli();
            uusinuoli.Karki = karkivastaus;
            uusinuoli.Pera = peravastaus;
            uusinuoli.Nuolipituus = nuolenpituus;
            return uusinuoli;
        }
    }
}
