using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyö
{
    enum Type
    {
        Armor,
        Weapon,
        Potion,
        Material
    }
    internal class Varuste
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Type Type { get; private set; }
        public int Price { get; private set; }
        public Varuste(string nimi, string desc, Type type, int price)
        {
            Name = nimi;
            Description = desc;
            this.Type = type;
            Price = price;
        }
    }

    // Aseet
    internal class StartingWeapon : Varuste
    {
        public StartingWeapon() : base("Huono miekka", "Miekka, jonka terä on tylsä, koska sitä ei ole hoidettu kunnolla.", Type.Weapon, 20) { }
    }
    internal class Miekka : Varuste
    {
        public Miekka() : base("Hyvä Miekka", "Perusmiekka, ei mitään erikoista. Käytetään kahdella kädellä.", Type.Weapon, 150) { }
    }
    internal class Keihas : Varuste
    {
        public Keihas() : base("Keihäs", "Pitkä Keihäs, voi auttaa jossain tilanteissa paremmin kuin miekka. Käytetään kahdella kädellä.", Type.Weapon, 135) { }
    }

    // Haarniskat
    internal class StartingArmor : Varuste
    {
        public StartingArmor() : base("Huono haarniska", "Goblini nahasta tehty haarniska, todella huonolaatuinen.", Type.Armor, 15) { }
    }
    internal class Haarniska : Varuste
    {
        public Haarniska() : base("Hyvä Haarniska", "Nahasta valmistettu haarniska, joka mitätöi puolet ottamasta vahingosta.", Type.Armor, 200) { }
    }

    // Juomat
    internal class TaikaJuoma : Varuste
    {
        public TaikaJuoma() : base("Taikajuoma", "Juoma, jota valmistettiin ensiavuksi (ei paranna suuria vammoja)", Type.Potion, 50) { }
    }
    internal class TiivistettyTaikaJuoma : Varuste
    {
        public TiivistettyTaikaJuoma() : base("Tiivistetty taikajuoma", "Kolmen taikajuoman tiiviste, joka parantaa jopa suuria vammoja.", Type.Potion, 95) { }
    }

    // Materiaalit 
    internal class PeikkoMateriaali : Varuste
    {
        public PeikkoMateriaali() : base("Peikon korvat", "Korvat, jota käytetään erilaisiin juomiin.", Type.Material, 40) { }
    }
    internal class SusiMateriaali : Varuste
    {
        public SusiMateriaali() : base("Suden nahka", "Nahka, jota voidaan käyttää esimerkiksi haarniskalle", Type.Material, 80) { }
    }
    internal class KyklooppiMateriaali : Varuste
    {
        public KyklooppiMateriaali() : base("Kykloopin hampaat", "Hampaat, jotka voidaan käyttää koruihin.", Type.Material, 200) { }
    }
}
