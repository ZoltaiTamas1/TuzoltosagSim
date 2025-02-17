using System;

namespace TuzoltosagSim
{
    public class Tuzoltoauto
    {
        public string Rendszam { get; private set; }
        public bool Szabad { get; private set; }

        public Tuzoltoauto(string rendszam)
        {
            Rendszam = rendszam;
            Szabad = true;
        }

        public void Kivonul(Tuzolto tuzolto, Epulet epulet)
        {
            if (!Szabad)
                throw new Exception($"{Rendszam} rendszámú autó már foglalt.");

            Szabad = false;
        }

        public void Reset()
        {
            Szabad = true;
        }
    }
}