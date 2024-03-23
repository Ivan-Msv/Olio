using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyö
{
    internal interface IAttackable
    {
        void Attack();
    }
    internal class Ritari
    {
        public string Nimi { get; private set; }
        public int CurrentMoney { get; set; }
        public int Health { get; private set; }
        public List<Varuste> Inventory { get; private set; }
        public Varuste? SelectedWeapon { get; set; }
        public Varuste? SelectedArmor { get; set; }
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public Ritari(string nimi, int health, int currentMoney, List<Varuste> varasto)
        {
            this.Nimi = nimi;
            this.CurrentMoney = currentMoney;
            this.Health = health;
            this.Inventory = varasto;
            MaxHealth = health;
        }
        public void ChangeMoney(int amount, bool give) // En halunnu tehä 2 eri MoneyAdd ja MoneyRemove
        {
            if (give)
            {
                CurrentMoney += amount;
            }
            else
            {
                CurrentMoney -= amount;
            }
        }
        public void ShowMoneyAmount()
        {
            int currentCursorTop = Console.CursorTop;
            int currentCursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(45, 0);
            Program.MenuText("[R]", "ahat", ConsoleColor.DarkGray);
            Console.SetCursorPosition(45, 1);
            Program.ColorLine($"{this.CurrentMoney} G", ConsoleColor.DarkYellow);
            Console.SetCursorPosition(currentCursorLeft, currentCursorTop);
        }

        public void ShowInventory(List<Varuste> inventory)
        {
            Console.Clear();
            string tavaraChange = inventory.Count == 1 ? "tavara" : "tavaraa"; // 1 tavaraa kuulostaa huonolta, niin se vaihtaa sen paremman kuuloseks

            while (true)
            {
                Program.EscapeText();
                List<Varuste> sortedInventory = inventory.OrderBy(item => item.Type).ToList();
                Program.ColorLine($"Sinulla on tällä hetkellä {inventory.Count} {tavaraChange}", ConsoleColor.DarkYellow);
                if (inventory.Count > 0)
                {

                    Program.ColorLine("Varusteet ovat seuraavat:", ConsoleColor.DarkYellow);
                    int i = 1;
                    foreach (var item in sortedInventory)
                    {
                        if (item == SelectedArmor || item == SelectedWeapon)
                        {
                            Program.ColorLine($"{i}. {item.Name} (Päällä)", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Program.ColorLine($"{i}. {item.Name}", ConsoleColor.Magenta);
                        }
                        i++;
                    }
                }


                ConsoleKeyInfo userinput = Console.ReadKey(true);
                char userinputChar = userinput.KeyChar;

                if (userinput.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }

                if (char.IsDigit(userinputChar) && userinputChar >= '1' && userinputChar <= '0' + inventory.Count)
                {
                    int index = userinputChar - '1';
                    EquipFromInventory(sortedInventory[index]);
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 3 + inventory.Count);
                    Program.ColorNoLine("Mitään ei tapahdu...", ConsoleColor.DarkGray);
                    Console.SetCursorPosition(0, 0);
                }
            }
        }
        public void ShowPlayerHP()
        {
            int currentCursorTop = Console.CursorTop;
            Console.SetCursorPosition(50, 0);
            Program.MenuText("[HP] ", $" {Health} / {MaxHealth} ", ConsoleColor.Green);
            Console.SetCursorPosition(0, currentCursorTop);
        }
        public void EquipFromInventory(Varuste item)
        {
            if (Inventory.Contains(item) && item != SelectedWeapon || item != SelectedArmor)
            {
                switch (item.Type)
                {
                    case Type.Weapon:
                        SelectedWeapon = item;
                        Console.Clear();
                        break;
                    case Type.Armor: 
                        SelectedArmor = item;
                        Console.Clear();
                        break;
                    default:
                        Program.ColorLine("Tätä asiaa ei voi varustaa", ConsoleColor.Red);
                        Program.WaitForSeconds(1);
                        Console.Clear();
                        break;
                }
            }
        }
#pragma warning disable CS8604
        public void DrinkPotion(int index)
        {
            int healthToAdd = 0;
            Varuste? potionToDelete = null;
            switch (index)
            {
                case 1:
                    potionToDelete = Inventory.FirstOrDefault(item => item.GetType() == typeof(TaikaJuoma));
                    break;
                case 2:
                    potionToDelete = Inventory.FirstOrDefault(item => item.GetType() == typeof(TiivistettyTaikaJuoma));
                    break;
            }

            if (MaxHealth - Health <= 5)
            {
                healthToAdd = MaxHealth - Health;
            }
            else
            {
                switch (index)
                {
                    case 1:

                        healthToAdd = 5;
                        break;
                    case 2:
                        healthToAdd = MaxHealth - Health;
                        break;
                }
            }
            switch (Health)
            {
                case int x when x == MaxHealth:
                    Program.ClearLines(3);
                    Program.ColorLine("Sinun terveyspisteet ovat täynnä, et voi juoda taikajuoman.", ConsoleColor.Red);
                    break;
                default:
                    Inventory.Remove(potionToDelete);
                    Health += healthToAdd;
                    Program.ClearLines(3);
                    string potionName = index > 1 ? "tiivistetyn taikajuoman!" : "taikajuoman!";
                    Program.ColorLine($"Joit {potionName} +{healthToAdd} terveyspistettä.", ConsoleColor.Green);
                    break;
            }
        }
#pragma warning restore CS8604

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            ShowPlayerHP();
            if (this.Health < 1)
            {
                this.Health = 0;
                ShowPlayerHP();
                GameOver();
            }
        }
        private void GameOver()
        {
            Program.ColorLine("Sinä kuolit...", ConsoleColor.DarkGray);
            Program.GameStart();
        }
    }
}
