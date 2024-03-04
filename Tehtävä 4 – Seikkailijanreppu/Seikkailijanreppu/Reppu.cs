using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seikkailijanreppu
{
    class Reppu
    {
        Tavara[] tavarat;
        int maxTavarat;
        float maxKantoPaino;
        float maxTilavuus;

        float kokonaisPaino;
        float kokonaisTilavuus;
        int kokonaisMäärä;
        public override string ToString()
        {
            string items = "";
            for (int i=0; i < kokonaisMäärä; i++)
            {
                items += tavarat[i] + " ";
            }
            return items;
        }

        public Reppu(int MaxTavarat, float MaxKantopaino, float MaxTilavuus)
        {
            maxTavarat = MaxTavarat;
            maxKantoPaino = MaxKantopaino;
            maxTilavuus = MaxTilavuus;

            kokonaisPaino = 0;
            kokonaisTilavuus = 0;
            kokonaisMäärä = 0;

            tavarat = new Tavara[maxTavarat];
        }

        public bool Lisää(Tavara tavara)
        {
            if (kokonaisPaino + tavara.Paino <= maxKantoPaino && kokonaisTilavuus + tavara.Tilavuus <= maxTilavuus && kokonaisMäärä < maxTavarat)
            {
                kokonaisPaino += tavara.Paino;
                kokonaisTilavuus += tavara.Tilavuus;
                tavarat[kokonaisMäärä] = tavara;
                kokonaisMäärä++;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Tilanne(Reppu currentreppu)
        {
            Console.WriteLine($"Reppussa on seuraavat tavarat: {currentreppu}");
            Console.WriteLine($"Repussa on tällä hetkellä {kokonaisMäärä}/{maxTavarat} tavaraa, {kokonaisPaino}/{maxKantoPaino} painoa ja {kokonaisTilavuus}/{maxTilavuus} tilavuus.");
            Console.WriteLine("Mitä haluat lisätä?");
            Console.WriteLine(" 1. Nuoli \n 2. Jousi \n 3. Köysi \n 4. Vettä \n 5. Ruokaa \n 6. Miekka");
        }
    }
}
