using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seikkailijanreppu
{
    class Tavara
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
