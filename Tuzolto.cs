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
                throw new Exception($"{Nev} már foglalt, nem tud riasztásra reagálni.");

            Szabad = false;
        }

        public void Tuzoltas(Epulet epulet)
        {
            if (Szabad)
                throw new Exception($"{Nev} szabad, nem tud tüzet oltani.");

            epulet.TuzEloltva();
        }

        public void Reset()
        {
            Szabad = true;
        }
    }
}