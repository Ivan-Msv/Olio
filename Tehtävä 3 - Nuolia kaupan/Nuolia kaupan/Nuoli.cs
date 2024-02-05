using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuolia_kaupan
{
    class Nuoli
    {
        public Karki Karki { get; set; }
        public Pera Pera { get; set; }
        public int Nuolipituus { get; set; }

        public static Nuoli LuoEliittiNuoli()
        {
            Nuoli EliittiNuoli = new Nuoli();
            EliittiNuoli.Karki = Karki.Timantti;
            EliittiNuoli.Pera = Pera.Kotkansulka;
            EliittiNuoli.Nuolipituus = 100;

            return EliittiNuoli;
        }
        public static Nuoli LuoPerusNuoli()
        {
            Nuoli PerusNuoli = new Nuoli();
            PerusNuoli.Karki = Karki.Teräs;
            PerusNuoli.Pera = Pera.Kanansulka;
            PerusNuoli.Nuolipituus = 85;

            return PerusNuoli;
        }

        public static Nuoli LuoAloittelijanuoli()
        {
            Nuoli AloittelijaNuoli = new Nuoli();
            AloittelijaNuoli.Karki = Karki.Puu;
            AloittelijaNuoli.Pera = Pera.Kanansulka;
            AloittelijaNuoli.Nuolipituus = 70;

            return AloittelijaNuoli;
        }

        public int PalautaHinta()
        {
            int karkihinta;
            if (Karki == Karki.Puu)
            {
                karkihinta = 3;
            }
            else if (Karki == Karki.Teräs)
            {
                karkihinta = 5;
            }
            else
            {
                karkihinta = 50;
            }

            int perahinta;
            if (Pera == Pera.Kanansulka)
            {
                perahinta = 1;
            }
            else if (Pera == Pera.Kotkansulka)
            {
                perahinta = 5;
            }
            else
            {
                perahinta = 0;
            }

            int pituushinta = (int)(Nuolipituus * 0.05);
            int kokonaishinta = karkihinta + perahinta + pituushinta;
            return kokonaishinta;
        }
    }
}
