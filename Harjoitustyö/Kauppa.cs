using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyö
{
    internal class Kauppa
    {
        public List<Varuste> varusteet = new List<Varuste>();
        public List<Varuste> temp = new List<Varuste>();
        static int userinputamount = 0;
        Ritari pelaaja;
        Random random = new Random();

        public Kauppa(Ritari player)
        {
            pelaaja = player;
            varusteet.Add(new Miekka());
            varusteet.Add(new Keihas());
            varusteet.Add(new Haarniska());
            varusteet.Add(new TaikaJuoma());
            varusteet.Add(new TaikaJuoma());
            varusteet.Add(new TiivistettyTaikaJuoma());
            varusteet.Add(new TiivistettyTaikaJuoma());
        }

        public void AvaaKauppa()
        {
            while (true)
            {
                Program.ColorNoLine("Tervetuloa kauppaan.", ConsoleColor.Green);
                Console.SetCursorPosition(0, 1);
                Program.MenuText("[M]", "yynti", ConsoleColor.DarkGray);

                Console.SetCursorPosition(9, 1);
                Program.MenuText("[O]", "sto", ConsoleColor.DarkGray);

                Console.SetCursorPosition(16, 1);
                Program.MenuText("[V]", "arusteet", ConsoleColor.DarkGray);

                pelaaja.ShowMoneyAmount();

                Console.SetCursorPosition(0, 0);
                Program.EscapeText();

                ConsoleKey userinput = Console.ReadKey(true).Key;

                if (userinput == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }

                switch (userinput)
                {
                    case ConsoleKey.M:
                        MyyTavara();
                        break;
                    case ConsoleKey.O:
                        OstaTavara();
                        break;
                    case ConsoleKey.V:
                        pelaaja.ShowInventory(pelaaja.Inventory);
                        break;
                }
            }
        }
        public void MyyTavara()
        {
            RefreshSellingItems();
            Program.EscapeText();
            pelaaja.ShowMoneyAmount();

            while (true)
            {
                ConsoleKeyInfo userinput = Console.ReadKey(true);
                char userinputChar = userinput.KeyChar;

                if (userinput.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }

                if (char.IsDigit(userinputChar) && userinputChar >= '1' && userinputChar <= '0' + pelaaja.Inventory.Count) // Outo et sun pitää laittaa sekä '0' että '1' et se toimis...
                {
                    int index = userinputChar - '1';
                    if (pelaaja.Inventory[index] == pelaaja.SelectedWeapon)
                    {
                        TavaraSell(pelaaja.Inventory[index], pelaaja.Inventory);
                        pelaaja.SelectedWeapon = null;
                    }
                    else if (pelaaja.Inventory[index] == pelaaja.SelectedArmor)
                    {
                        TavaraSell(pelaaja.Inventory[index], pelaaja.Inventory);
                        pelaaja.SelectedArmor = null;
                    }
                    else
                    {
                        TavaraSell(pelaaja.Inventory[index], pelaaja.Inventory);
                    }
                }
                else
                {
                    Program.ColorLine("Mitään ei tapahdu...", ConsoleColor.DarkGray);
                }
            }
        }
        public void OstaTavara()
        {
            RefreshBuyingItems();
            Program.EscapeText();
            pelaaja.ShowMoneyAmount();

            while (true)
            {
                ConsoleKeyInfo userinput = Console.ReadKey(true);
                char userinputChar = userinput.KeyChar;

                if (userinput.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }

                if (char.IsDigit(userinputChar) && userinputChar >= '1' && userinputChar <= '0' + varusteet.Count) // Outo et sun pitää laittaa sekä '0' että '1' et se toimis...
                {
                    int index = userinputChar - '1';
                    TavaraBuy(varusteet[index], pelaaja.Inventory, varusteet);
                    Console.ReadKey(true);
                    RefreshBuyingItems();
                    Program.EscapeText();
                    pelaaja.ShowMoneyAmount();
                }
                else
                {
                    EasterEgg();
                }

            }
        }

        public void TavaraBuy(Varuste tavara, List<Varuste> destinationPlace, List<Varuste> sourcePlace)
        {
            Program.ColorLine(tavara.Description, ConsoleColor.DarkYellow);
            Program.ColorLine("Oletko varma että haluat ostaa tämän asian? (Y/N)", ConsoleColor.White);
            ConsoleKey userinput = Console.ReadKey(true).Key;

            if (userinput == ConsoleKey.Y)
            {
                if (pelaaja.CurrentMoney >= tavara.Price)
                {
                    destinationPlace.Add(tavara);
                    sourcePlace.Remove(tavara);
                    pelaaja.CurrentMoney -= tavara.Price;
                    Program.ColorLine($"{tavara.Name} ostettu!", ConsoleColor.Green);
                    Program.ColorLine($"Sinulla on enää {pelaaja.CurrentMoney} G jäljellä.", ConsoleColor.DarkYellow);
                }
                else
                {
                    int puuttuu = tavara.Price - pelaaja.CurrentMoney;
                    Program.ColorLine($"Rahat ei riitä (puuttuu {puuttuu} G), peruutetaan...", ConsoleColor.White);
                }
            }
            else
            {
                Program.ColorLine("Perutetaan...", ConsoleColor.White);
            }
        }
        public void TavaraSell(Varuste tavara, List<Varuste> sourcePlace)
        {
            Program.ColorLine($"Yrität myydä {tavara.Name}.", ConsoleColor.DarkYellow);
            Program.ColorLine("Oletko varma että haluat myydä tämän asian? (Y/N)", ConsoleColor.White);
            ConsoleKey userinput = Console.ReadKey(true).Key;

            if (userinput == ConsoleKey.Y)
            {
                double adjustedPrice = tavara.Price * 0.8;
                sourcePlace.Remove(tavara);
                pelaaja.CurrentMoney += (int)Math.Round(adjustedPrice);
                Program.ColorLine($"{tavara.Name} myyty!", ConsoleColor.Green);
                Program.ColorLine($"Sinulla on nyt {pelaaja.CurrentMoney} G !", ConsoleColor.Green);
                Console.ReadKey(true);
                RefreshSellingItems();
                Program.EscapeText();
                pelaaja.ShowMoneyAmount();
            }
            else
            {
                Program.ColorLine("Perutetaan...", ConsoleColor.White);
            }
        }
        public void EasterEgg()
        {
            if (userinputamount >= 5 && userinputamount < 15)
            {
                Program.ColorLine("Jotain alkaa tapahtumaan...", ConsoleColor.Gray);
            }
            else if (userinputamount == 15)
            {
                int randomNum = random.Next(70, 150);
                Program.ColorLine($"Löysit lompakon hyllyn alta! (+{randomNum} G)", ConsoleColor.White);
                pelaaja.ChangeMoney(randomNum, true);
                pelaaja.ShowMoneyAmount();
            }
            else
            {
                Program.ColorLine("Mitään ei tapahdu...", ConsoleColor.DarkGray);
            }
            userinputamount++;
        }
        public void RefreshBuyingItems()
        {
            Console.Clear();
            Program.ColorLine("Täältä voit ostaa seuraavat asiat:", ConsoleColor.White);

            int i = 1;
            foreach (var tavara in varusteet)
            {
                Program.ColorLine($"{i}. {tavara.Name}, hinta on {tavara.Price} G", ConsoleColor.Blue);
                i++;
            }
            Program.ColorLine("Saat lisää tietoa klikkaamalla asioiden numeroita.", ConsoleColor.DarkGray);
        }
        public void RefreshSellingItems()
        {
            Console.Clear();
            if (pelaaja.Inventory.Count <= 0)
            {
                Program.ColorLine("Sinulla ei ole mitään myytävää...", ConsoleColor.Red);
            }
            else
            {
                Program.ColorLine("Tänne voit myydä seuraavat asiat:", ConsoleColor.White);
            }
            int i = 1;

            foreach (var tavara in pelaaja.Inventory)
            {
                Program.ColorLine($"{i}. {tavara.Name}, saat {tavara.Price * 0.8} G", ConsoleColor.DarkGreen);
                i++;
            }
        }
    }
}
