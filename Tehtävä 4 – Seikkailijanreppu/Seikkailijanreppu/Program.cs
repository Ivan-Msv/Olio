using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seikkailijanreppu
{
    class Program
    {
        static void Main(string[] args)
        {
            Reppu uusreppu = new Reppu(10, 30, 20);

            while (true)
            {
                uusreppu.Tilanne(uusreppu);
                ConsoleKey userinput = Console.ReadKey(true).Key;
                Tavara userchoice = null;
                switch (userinput)
                {
                    case ConsoleKey.D1:
                        userchoice = new Nuoli();
                        break;
                    case ConsoleKey.D2:
                        userchoice = new Jousi();
                        break;
                    case ConsoleKey.D3:
                        userchoice = new Köysi();
                        break;
                    case ConsoleKey.D4:
                        userchoice = new Vesi();
                        break;
                    case ConsoleKey.D5:
                        userchoice = new RuokaAnnos();
                        break;
                    case ConsoleKey.D6:
                        userchoice = new Miekka();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Käytä pyydettyjä numeroita.");
                        Console.ResetColor();
                        continue;
                }
                if (uusreppu.Lisää(userchoice))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Tavara oli lisätty reppuun.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lisääminen epäonnistui, sillä reppu ylittää sen kapasiteetin");
                    Console.ResetColor();
                }
            }
        }
    }
}
