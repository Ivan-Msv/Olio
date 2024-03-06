namespace VäritetytTavarat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VaritettyTavara<Tavara> varitettyTavara1 = new VaritettyTavara<Tavara>(new Jousi(), ConsoleColor.Red);
            VaritettyTavara<Tavara> varitettyTavara2 = new VaritettyTavara<Tavara>(new Miekka(), ConsoleColor.DarkGreen);
            VaritettyTavara<Tavara> varitettyTavara3 = new VaritettyTavara<Tavara>(new Vesi(), ConsoleColor.Blue);
            varitettyTavara1.NaytaTavara();
            varitettyTavara2.NaytaTavara();
            varitettyTavara3.NaytaTavara();
        }
    }
    internal class VaritettyTavara<T>
    {
        public T tavara;
        public ConsoleColor color;

        public VaritettyTavara(T tavara, ConsoleColor color)
        {
            this.tavara = tavara; 
            this.color = color;
        }

        public void NaytaTavara()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(tavara);
            Console.ResetColor();
        }
    }
    internal class Tavara
    {
        public float Tilavuus { get; private set; }
        public float Paino { get; private set; }
        public override string ToString()
        {
            return GetType().Name.ToString();
        }

        public Tavara(float paino, float tilavuus)
        {
            Tilavuus = tilavuus;
            Paino = paino;
        }
    }
    internal class Nuoli : Tavara
    {
        public Nuoli() : base(0.1f, 0.05f) { }
    }
    internal class Jousi : Tavara
    {
        public Jousi() : base(1, 4) { }
    }
    internal class Köysi : Tavara
    {
        public Köysi() : base(1, 1.5f) { }
    }
    internal class Vesi : Tavara
    {
        public Vesi() : base(2, 2) { }
    }
    internal class RuokaAnnos : Tavara
    {
        public RuokaAnnos() : base(1, 0.5f) { }
    }
    internal class Miekka : Tavara
    {
        public Miekka() : base(5, 3) { }
    }
}
