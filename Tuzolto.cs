using System;

namespace TuzoltosagSim
{
    public class Tuzolto
    {
        public string Nev { get; private set; }
        public bool Szabad { get; private set; }

        public Tuzolto(string nev)
        {
            Nev = nev;
            Szabad = true;
        }

        public void Riasztas(Epulet epulet)
        {
            if (!Szabad)
                throw new InvalidOperationException($"{Nev} már foglalt, nem tud riasztásra reagálni.");

            Szabad = false;
        }

        public void Tuzoltas(Epulet epulet)
        {
            if (Szabad)
                throw new InvalidOperationException($"{Nev} szabad, nem tud tüzet oltani.");

            epulet.TuzEloltva();
            Szabad = true;
        }
    }
}
