using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyö
{
    enum EnemyState
    {
        Elossa,
        Kuollut
    }
    internal abstract class Viholliset
    {
        public int Health { get; set; }
        public int Damage { get; private set; }

        public EnemyState state { get; set; }

        public Viholliset(int startingHealth, int damage)
        {
            Health = startingHealth;
            Damage = damage;
        }
        public int EnemyTakeDamage(Ritari pelaaja, Viholliset enemy)
        {
            int damageAmount = 0;
            switch (pelaaja.SelectedWeapon)
            {
                case StartingWeapon:
                    damageAmount = 4;
                    break;
                case Miekka:
                    damageAmount = 8;
                    break;
                case Keihas:
                    switch (enemy)
                    {
                        case Kyklooppi:
                            damageAmount = 16;
                            break;
                        default:
                            damageAmount = 6;
                            break;
                    }
                    break;
                case null:
                    damageAmount = 1;
                    break;
            }
            this.Health -= damageAmount;
            if (this.Health <= 0)
            {
                Die();
            }
            return damageAmount;
        }
        public int ArmorCheck(Ritari pelaaja)
        {
            int finalDamage = 0;
            switch (pelaaja.SelectedArmor)
            {
                case null:
                    finalDamage = this.Damage * 2;
                    break;
                case StartingArmor:
                    if (this.Damage > 1)
                    {
                        finalDamage = this.Damage - 1;
                    }
                    else
                    {
                        finalDamage = this.Damage;
                    }
                    break;
                case Haarniska:
                    if (this.Damage > 1)
                    {
                        finalDamage = this.Damage / 2;
                    }
                    else
                    {
                        finalDamage = this.Damage;
                    }
                    break;
            }
            return finalDamage;
        }
        public void BasicAttack(Ritari pelaaja)
        {
            pelaaja.TakeDamage(ArmorCheck(pelaaja));
        }
        public abstract void SpecialAttack(Ritari pelaaja, List<Viholliset> enemies);
        public abstract void GetAttacked(Ritari pelaaja);
        public abstract void Die();
    }
    internal class Peikko : Viholliset
    {
        public Peikko() : base(10, 2) { }

        public override void SpecialAttack(Ritari pelaaja, List<Viholliset> enemies)
        {
            int newDamage = ArmorCheck(pelaaja) * 2 * enemies.Count;
            pelaaja.TakeDamage(newDamage);
            Program.ColorLine($"Kaikki peikot hyppäsivät päälle samaan aikaan, tekivät TUPLASTI vahinkoa ({newDamage}).", ConsoleColor.Red);
        }
        public override void GetAttacked(Ritari pelaaja)
        {
            int damageAmount = EnemyTakeDamage(pelaaja, this);
            if (state == EnemyState.Elossa)
            {
                Program.ColorLine($"Peikko otti {damageAmount} vahinkoa.", ConsoleColor.Green);
                Program.ColorLine($"Peikolla jäi {Health} terveyspisteitä", ConsoleColor.DarkGreen);
            }
        }
        public override void Die()
        {
            Program.ColorLine($"Tämä lyönti oli tappava! Peikko kuoli.", ConsoleColor.Magenta);
            state = EnemyState.Kuollut;
        }
    }
    internal class Susi : Viholliset
    {
        public Susi() : base(20, 4) { }

        public override void SpecialAttack(Ritari pelaaja, List<Viholliset> enemies)
        {
            bool allDead = enemies.All(enemy => enemy.state == EnemyState.Kuollut);
            if (!allDead)
            {
                Program.ColorLine($"Sudet ulvoo! kaikki saa +5 terveyspistettä.", ConsoleColor.Red);
                foreach (var enemy in enemies)
                {
                    if (enemy.state == EnemyState.Elossa)
                    {
                        enemy.Health += 5;
                    }
                }
            }
        }
        public override void GetAttacked(Ritari pelaaja)
        {
            int damageAmount = EnemyTakeDamage(pelaaja, this);
            if (state == EnemyState.Elossa)
            {
                Program.ColorLine($"Susi otti {damageAmount} vahinkoa.", ConsoleColor.Green);
                Program.ColorLine($"Sudella jäi {Health} terveyspisteitä", ConsoleColor.DarkGreen);
            }
        }

        public override void Die()
        {
            Program.ColorLine($"Tämä lyönti oli tappava! Susi kuoli.", ConsoleColor.Magenta);
            state = EnemyState.Kuollut;
        }
    }
    internal class Kyklooppi : Viholliset
    {
        public Kyklooppi() : base(70, 15) { }

        public override void SpecialAttack(Ritari pelaaja, List<Viholliset> enemies)
        {
            pelaaja.TakeDamage(this.Damage);
            Program.ColorLine($"Kyklooppi löi TÄYSVOIMALLA! Haarniska ei suojannut yhtään. ({this.Damage}).", ConsoleColor.Red);
        }

        public override void GetAttacked(Ritari pelaaja)
        {
            int damageAmount = EnemyTakeDamage(pelaaja, this);
            if (state == EnemyState.Elossa)
            {
                Program.ColorLine($"Kyklooppi otti {damageAmount} vahinkoa.", ConsoleColor.Green);
                Program.ColorLine($"Kykloopilla jäi {Health} terveyspisteitä", ConsoleColor.DarkGreen);
            }
        }

        public override void Die()
        {
            Program.ColorLine($"Tämä lyönti oli tappava! Kyklooppi kuoli.", ConsoleColor.Magenta);
            state = EnemyState.Kuollut;
        }
    }
}
