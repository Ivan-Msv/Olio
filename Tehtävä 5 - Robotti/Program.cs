namespace Robotti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
            System.Console.InputEncoding = System.Text.Encoding.Unicode;
            Robotti robotti = new Robotti();

            for (int i = 0; i < 3; i++)
            {
                IRobottiKäsky? käsky;
                Console.WriteLine("Mitä komentoja syötetään robotille? Vaihtoehdot:");
                Console.WriteLine("Käynnistä, Sammuta, Ylös, Alas, Oikea, Vasen.");
                while (true)
                {
                    string? userinput = Console.ReadLine();
                    switch (userinput?.ToLower())
                    {
                        case "käynnistä":
                            käsky = new Käynnistä();
                            break;
                        case "sammuta":
                            käsky = new Sammuta();
                            break;
                        case "ylös":
                            käsky = new Ylöskäsky();
                            break;
                        case "alas":
                            käsky = new Alaskäsky();
                            break;
                        case "oikea":
                            käsky = new Oikeakäsky();
                            break;
                        case "vasen":
                            käsky = new Vasenkäsky();
                            break;
                        default:
                            käsky = null;
                            break;
                    }
                    if (käsky != null)
                    {
                        robotti.AnnaKäsky(käsky);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kirjoitit väärin, yritä uudelleen.");
                        continue;
                    }
                }
            }
            robotti.Suorita();
        }
    }
    public class Robotti
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool OnKäynnissä { get; set; }

        // Taulukko kannattaa olla private, muuten
        // voi helposti tulla lukeminen tai kirjoitus sen
        // ulkopuolelle
        private IRobottiKäsky?[] Käskyt;

        public Robotti()
        {
            Käskyt = new IRobottiKäsky[3];
        }

        // Palauttaa true jos käskyn antaminen onnistui
        // Palauttaa false, jos Käskyt taulukko on jo täynnä
        public bool AnnaKäsky(IRobottiKäsky käsky)
        {
            // Onko tilaa uudelle käskylle?
            for (int i = 0; i < Käskyt.Length; i++)
            {
                // Onko tässä vapaa kohta?
                if (Käskyt[i] == null)
                {
                    // Käskyn antaminen onnistui
                    Käskyt[i] = käsky;
                    return true;
                }
            }
            // Käskyn antaminen ei onnistunut
            return false;
        }

        public void Suorita()
        {
            foreach (IRobottiKäsky? käsky in Käskyt)
            {
                käsky?.Suorita(this);
                Console.WriteLine($"[{X} {Y} {OnKäynnissä}]");
            }
        }
    }
    public interface IRobottiKäsky
    {
        void Suorita(Robotti robotti);
    }
    public class Käynnistä : IRobottiKäsky
    {
        public void Suorita(Robotti robotti)
        {
            robotti.OnKäynnissä = true;
        }
    }
    public class Sammuta : IRobottiKäsky
    {
        public void Suorita(Robotti robotti)
        {
            robotti.OnKäynnissä = false;
        }
    }
    public class Ylöskäsky : IRobottiKäsky
    {
        public void Suorita(Robotti robotti)
        {
            if (robotti.OnKäynnissä)
            {
                robotti.Y++;
            }
        }
    }
    public class Alaskäsky : IRobottiKäsky
    {
        public void Suorita(Robotti robotti)
        {
            if (robotti.OnKäynnissä)
            {
                robotti.Y--;
            }
        }
    }
    public class Vasenkäsky : IRobottiKäsky
    {
        public void Suorita(Robotti robotti)
        {
            if (robotti.OnKäynnissä)
            {
                robotti.X--;
            }
        }
    }
    public class Oikeakäsky : IRobottiKäsky
    {
        public void Suorita(Robotti robotti)
        {
            if (robotti.OnKäynnissä)
            {
                robotti.X++;
            }
        }
    }
}
