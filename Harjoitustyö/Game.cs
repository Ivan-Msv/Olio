using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyö
{
    internal class Game
    {
        Ritari pelaaja;
        Kauppa kauppa;
        List<Varuste> varasto;
        List<Viholliset> enemies;
        string enemyName;
        bool allEnemiesDead = false;
        bool enemyTurn = false;
        public void Run()
        {
            varasto = new List<Varuste>();
            pelaaja = new Ritari(AskName(), 20, 100, varasto);
            kauppa = new Kauppa(pelaaja);
            varasto.Add(new StartingWeapon());
            varasto.Add(new StartingArmor());
            varasto.Add(new TaikaJuoma());
            varasto.Add(new TaikaJuoma());
            pelaaja.SelectedWeapon = varasto[0];
            pelaaja.SelectedArmor = varasto[1];

            if (pelaaja.Nimi == "test")
            {
                pelaaja.ChangeMoney(900, true);
            } 

            Console.Clear();
            while (true)
            {
                GameText();
                KeyBinds();
            }
        }
        public void KeyBinds()
        {
            ConsoleKey userinput = Console.ReadKey(true).Key;

            switch (userinput)
            {
                case ConsoleKey.K:
                    Console.Clear();
                    kauppa.AvaaKauppa();
                    break;
                case ConsoleKey.T:
                    ChooseEnemyBattle();
                    break;
            }
        }
        private string AskName()
        {
            Program.ColorLine("Mikä on sinun nimesi?", ConsoleColor.White);
            string finalname = "";
            while (true)
            {
                string? userinput = Console.ReadLine();

                if (userinput != null)
                {
                    if (userinput.Length < 3)
                    {
                        Program.ColorLine("Nimi ei saa olla alle 3 kirjainta.", ConsoleColor.Red);
                    }
                    else if (userinput.Length > 9)
                    {
                        Program.ColorLine("Nimi ei saa olla yli 9 kirjainta.", ConsoleColor.Red);
                    }
                    else
                    {
                        finalname = userinput.ToLower();
                        break;
                    }
                }
                else
                {
                    Program.ColorLine("Nimi ei saa olla tyhjä.", ConsoleColor.Red);
                }
            }
            return finalname;
        }
        private void GameText()
        {
            pelaaja.ShowMoneyAmount();
            Program.ColorLine($"Mitä haluat tehdä?", ConsoleColor.DarkCyan);
            Program.MenuText("[K]", "auppa", ConsoleColor.DarkGray);
            Console.SetCursorPosition(Console.CursorLeft + 5, Console.CursorTop);
            Program.MenuText("[T]", "aistelu", ConsoleColor.DarkGray);
            Console.SetCursorPosition(0, 0);
        }
        private void ChooseEnemyBattle()
        {
            while (true)
            {
                Console.Clear();

                Program.EscapeText();
                Program.ColorLine($"Ketä haluat taistella? (Et voi vaihtaa varusteet taistelun aikana)", ConsoleColor.Blue);
                Program.ColorNoLine("Lauma ", ConsoleColor.DarkGray);
                Program.MenuText("[P]", "eikkoja", ConsoleColor.DarkGray);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Program.ColorNoLine("Lauma ", ConsoleColor.DarkGray);
                Program.MenuText("[S]", "usia", ConsoleColor.DarkGray);
                Console.SetCursorPosition(6, Console.CursorTop + 1);
                Program.MenuText("[K]", "yklooppi", ConsoleColor.DarkGray);
                Console.SetCursorPosition(0, 0);


                ConsoleKey userinput = Console.ReadKey(true).Key;

                if (userinput == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }

                switch (userinput)
                {
                    case ConsoleKey.P:
                        EnemyChoice(new Peikko());
                        break;
                    case ConsoleKey.S:
                        EnemyChoice(new Susi());
                        break;
                    case ConsoleKey.K:
                        EnemyChoice(new Kyklooppi());
                        break;
                }
            }
        }
        private void Battle(List<Viholliset> tempEnemies)
        {
            Console.Clear();
            enemies = tempEnemies;
            enemyName = GetEnemyNames();
            int enemyAmount = enemies.Count;

            string enemyString = enemies.Count > 1 ? $"{enemyAmount} {enemyName}." : $"{enemyName}."; // enemy string vaihto määrän perusteella
            Program.ColorLine($"Olet kohdannut {enemyString}", ConsoleColor.DarkYellow);

            while (true)
            {
                EnemyTurnToAttack();
                Program.RemoveEscapeText();
                pelaaja.ShowPlayerHP();

                allEnemiesDead = enemies.All(enemies => enemies.state == EnemyState.Kuollut);

                if (!allEnemiesDead)
                {
                    Program.ColorLine("Mitä haluat tehdä?", ConsoleColor.Blue);
                    Program.MenuText("[H]", "yökkää", ConsoleColor.DarkGray);
                    Console.SetCursorPosition(Console.CursorLeft + 5, Console.CursorTop);
                    Program.MenuText("[A]", "vaa reppu", ConsoleColor.DarkGray);
                    Console.SetCursorPosition(Console.CursorLeft + 5, Console.CursorTop);
                    Program.ColorNoLine("Yrittää ", ConsoleColor.DarkGray);
                    Program.MenuText("[K]", "arata", ConsoleColor.DarkGray);
                }
                Console.SetCursorPosition(0, Console.CursorTop + 1);

                if (!allEnemiesDead)
                {
                    ConsoleKeyInfo userinput = Console.ReadKey(true);

                    if (userinput.Key == ConsoleKey.K)
                    {
                        Program.ClearLines(2);
                        if (FleeChance())
                        {
                            Console.ReadKey(true);
                            Console.Clear();
                            break;
                        }
                    }

                    switch (userinput.Key)
                    {
                        case ConsoleKey.H:
                            Program.ClearLines(2);
                            AttackInterface();
                            break;
                        case ConsoleKey.A:
                            Program.ClearLines(2);
                            BattleInventory();
                            break;
                        case ConsoleKey.K:
                            break;
                        default:
                            Program.ClearLines(2);
                            break;
                    }
                }
                else
                {
                    string enemyReward = "";
                    foreach (var deadEnemy in enemies)
                    {
                        switch (deadEnemy)
                        {
                            case Peikko:
                                pelaaja.Inventory.Add(new PeikkoMateriaali());
                                enemyReward = enemies.Count > 1 ? "peikon korvaa" : "Peikon korvan";
                                break;
                            case Susi:
                                pelaaja.Inventory.Add(new SusiMateriaali());
                                enemyReward = enemies.Count > 1 ? "suden nahkaa" : "Suden nahkan";
                                break;
                            case Kyklooppi:
                                pelaaja.Inventory.Add(new KyklooppiMateriaali());
                                enemyReward = enemies.Count > 1 ? "kykloopin hampaan" : "Kykloopin hampaita";
                                break;
                        }
                    }
                    string enemiesDead = enemies.Count > 1 ? $"Olet tappanut {enemies.Count} {enemyName}" : $"Tapoit {enemyName}";
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
                    Program.ColorLine($"{enemiesDead}, ansaitset {enemies.Count} {enemyReward}", ConsoleColor.White);
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                }
            }
        }
        private void AttackInterface()
        {
            Program.EscapeText();
            int peikkoIndex = 1;
            Program.ColorLine("Kenet haluat hyökätä?", ConsoleColor.Magenta);
            foreach (var enemyCount in enemies)
            {
                switch (enemyCount.state)
                {
                    case EnemyState.Elossa:
                        Program.ColorLine($"{enemyCount.GetType().Name} {peikkoIndex},  {enemyCount.state} ({enemyCount.Health} HP)", ConsoleColor.Red);
                        break;
                    case EnemyState.Kuollut:
                        Program.ColorLine($"{enemyCount.GetType().Name} {peikkoIndex},  {enemyCount.state}", ConsoleColor.Red);
                        break;
                }
                peikkoIndex++;
            }
            while (!allEnemiesDead)
            {
                ConsoleKeyInfo userinput = Console.ReadKey(true);
                char userinputChar = userinput.KeyChar;

                if (char.IsDigit(userinputChar) && userinputChar >= '1' && userinputChar <= '0' + enemies.Count)
                {
                    int index = userinputChar - '1';
                    Viholliset vihollinen = enemies[index];

                    Program.ClearLines(enemies.Count + 1);
                    vihollinen.GetAttacked(pelaaja);

                    enemyTurn = true;
                    break;
                }
                else if (userinput.Key == ConsoleKey.Escape)
                {
                    Program.RemoveEscapeText();
                    Program.ClearLines(enemies.Count + 1);
                    break;
                }
            }
        }
        private void EnemyTurnToAttack()
        {
            if (enemyTurn)
            {
                List<int> temp = new List<int>();
                Random random = new Random();
                float specialAttackChance = random.NextSingle();
                switch (specialAttackChance)
                {
                    case float x when x <= 0.15f:
                        enemies[0].SpecialAttack(pelaaja, enemies);
                        break;
                    default:
                        int enemyTotalDamage = 0;
                        foreach (var enemy in enemies)
                        {
                            if (enemy.state == EnemyState.Elossa)
                            {
                                enemyTotalDamage += enemy.ArmorCheck(pelaaja);
                                enemy.BasicAttack(pelaaja);
                                temp.Add(enemies.IndexOf(enemy) + 1);
                            }
                        }
                        string amountString = string.Join(", ", temp);
                        if (temp.Count > 0)
                        {
                            Program.ColorLine($"{enemies[0].GetType().Name} {amountString} hyökkäsi! Teki pelaajaan {enemyTotalDamage} vahinkoa.", ConsoleColor.Red);
                        }
                        break;
                }
                enemyTurn = false;
            }
        }
        private void BattleInventory()
        {
            Program.EscapeText();
            Program.ColorLine("Avaat taikajuomarepun:", ConsoleColor.Magenta);

            int lesserPotionAmount = 0;
            int potionAmount = 0;
            foreach (var item in pelaaja.Inventory)
            {
                if (item.Type == Type.Potion)
                {
                    switch (item)
                    {
                        case TaikaJuoma:
                            lesserPotionAmount++;
                            break;
                        case TiivistettyTaikaJuoma:
                            potionAmount++;
                            break;
                    }
                }
            }

            if (potionAmount < 1 && lesserPotionAmount < 1)
            {
                Program.ColorLine($"Sinulla ei ole yhtään käytettäviä juomia.", ConsoleColor.Red);
            }
            else
            {
                Program.ColorLine($"1. Taikajuomat ({lesserPotionAmount})", ConsoleColor.DarkCyan);
                Program.ColorLine($"2. Tiivistetyt taikajuomat ({potionAmount})", ConsoleColor.Cyan);
            }

            while (true)
            {
                ConsoleKeyInfo userinput = Console.ReadKey(true);
                char userinputChar = userinput.KeyChar;

                if (userinputChar == '1' && lesserPotionAmount >= 1)
                {
                    pelaaja.DrinkPotion(1);
                    break;
                }
                else if (userinputChar == '2' && potionAmount >= 1)
                {
                    pelaaja.DrinkPotion(2);
                    break;
                }
                else if (userinput.Key == ConsoleKey.Escape)
                {
                    Program.ClearLines(3);
                    break;
                }
            }
        }
        private void EnemyChoice<T>(T enemy) where T : Viholliset, new() // https://stackoverflow.com/questions/4737970/what-does-where-t-class-new-mean
        {
            List<Viholliset> temp = new List<Viholliset>();
            Random random = new Random();

            switch (enemy)
            {
                case Peikko:
                    int peikkoAmount = random.Next(2, 4);
                    for (int i = 0; i < peikkoAmount; i++)
                    {
                        temp.Add(new T());
                    }
                    break;
                case Susi:
                    int susiAmount = random.Next(1, 3);
                    for (int i = 0; i < susiAmount; i++)
                    {
                        temp.Add(new T());
                    }
                    break;
                case Kyklooppi:
                    temp.Add(new T());
                    break;
            }

            Battle(temp);
        }
        private bool FleeChance()
        {
            Random random = new Random();
            float chanceToFlee = random.NextSingle();
            switch (chanceToFlee)
            {
                case float x when x <= 0.2f:
                    Program.ColorLine("Pääsit karkuun ennen kuin viholliset tajusivat! (20%)", ConsoleColor.White);
                    return true;
                case float x when x <= 0.5f:
                    Program.ColorLine("Pääsit karkuun, mutta viholliset huomasivat. Otat 3 vahinkoa! (50%)", ConsoleColor.DarkGray);
                    pelaaja.TakeDamage(3);
                    return true;
                default:
                    Program.ColorLine("Et päässy karkuun. Vihollisten vuoro.", ConsoleColor.Red);
                    enemyTurn = true;
                    return false;
            }
        }
        private string GetEnemyNames()
        {
            string finalName = "";
            if (enemies.Count > 1)
            {
                switch (enemies[0])
                {
                    case Peikko:
                        finalName = "peikkoa";
                        break;
                    case Susi:
                        finalName = "sudet";
                        break;
                    case Kyklooppi:
                        finalName = "kykloopit";
                        break;
                }
            }
            else
            {
                switch (enemies[0])
                {
                    case Peikko:
                        finalName = "peikon";
                        break;
                    case Susi:
                        finalName = "suden";
                        break;
                    case Kyklooppi:
                        finalName = "kykloopin";
                        break;
                }
            }
            return finalName;
        }
    }
}
