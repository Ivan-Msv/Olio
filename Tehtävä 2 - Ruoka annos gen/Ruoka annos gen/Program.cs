using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruoka_annos_gen
{
    class Program
    {
        enum RaakaAine
        {
            Nautaa, Kanaa, Kasviksia
        }
        enum Lisuke
        {
            Perunaa, Riisiä, Pastaa
        }
        enum Kastike
        {
            Curry, Hapanimelä, Chili
        }
        static void Main(string[] args)
        {
            List<string> ruokaAnnos = new List<string>();
            while (true)
            {
                Console.Write($"Pääraaka-Aine ({RaakaAine.Nautaa}, {RaakaAine.Kanaa}, {RaakaAine.Kasviksia}): ");
                if (Enum.TryParse(Console.ReadLine(), true, out RaakaAine sRaakaAine))
                {
                    ruokaAnnos.Add(sRaakaAine.ToString());
                    break;
                }
                else
                {
                    Console.WriteLine("Väärä Pääraaka-Aine, yritä uudelleen.");
                    continue;
                }
            }
            while (true)
            {
                Console.Write($"Lisukkeet ({Lisuke.Perunaa}, {Lisuke.Riisiä}, {Lisuke.Pastaa}): ");
                if (Enum.TryParse(Console.ReadLine(), true, out Lisuke sLisuke))
                {
                    ruokaAnnos.Add(sLisuke.ToString());
                    break;
                }
                else
                {
                    Console.WriteLine("Väärä Lisuke, yritä uudelleen.");
                    continue;
                }
            }
            while (true)
            {
                Console.Write($"Kastike ({Kastike.Chili}, {Kastike.Curry}, {Kastike.Hapanimelä}): ");
                if (Enum.TryParse(Console.ReadLine(), true, out Kastike sKastike))
                {
                    ruokaAnnos.Add(sKastike.ToString());
                    break;
                }
                else
                {
                    Console.WriteLine("Väärä Kastike, yritä uudelleen.");
                    continue;
                }
            }
            Console.WriteLine($"{ruokaAnnos[0]} ja {ruokaAnnos[1]} {ruokaAnnos[2]}-kastikkeella.");
            Console.ReadKey();
        }
    }
}
