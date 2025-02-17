using System;
using System.Collections.Generic;

namespace TuzoltosagSim
{
    public class Varos
    {
        public List<Epulet> Epuletek { get; private set; }
        public List<Epulet> Leegtek { get; private set; }
        public List<Tuzolto> Tuzoltok { get; private set; }
        public List<Tuzoltoauto> TuzoltoAutok { get; private set; }
        public List<Vizforras> Vizforrasok { get; private set; }

        public int EloltottakSzama { get; set; }
        public int LeegtekSzama { get; set; }
        public int OsszesVizFelhasznalva { get; set; }

        public Varos()
        {
            Epuletek = new List<Epulet>();
            Leegtek = new List<Epulet>();
            Tuzoltok = new List<Tuzolto>();
            TuzoltoAutok = new List<Tuzoltoauto>();
            Vizforrasok = new List<Vizforras>();

            EloltottakSzama = 0;
            LeegtekSzama = 0;
            OsszesVizFelhasznalva = 0;
        }

        public void EpuletHozzaad(Epulet epulet)
        {
            Epuletek.Add(epulet);
        }

        public void TuzoltoHozzaad(Tuzolto tuzolto)
        {
            Tuzoltok.Add(tuzolto);
        }

        public void TuzoltoautoHozzaad(Tuzoltoauto auto)
        {
            TuzoltoAutok.Add(auto);
        }

        public void VizforrasHozzaad(Vizforras vizforras)
        {
            Vizforrasok.Add(vizforras);
        }

        public int Tuzoltas(Epulet epulet)
        {
            Tuzolto szabadTuzolto = null!;
            for (int i = 0; i < Tuzoltok.Count; i++)
            {
                if (Tuzoltok[i].Szabad)
                {
                    szabadTuzolto = Tuzoltok[i];
                    break;
                }
            }

            Tuzoltoauto szabadAuto = null!;
            for (int i = 0; i < TuzoltoAutok.Count; i++)
            {
                if (TuzoltoAutok[i].Szabad)
                {
                    szabadAuto = TuzoltoAutok[i];
                    break;
                }
            }

            if (szabadTuzolto == null || szabadAuto == null)
                throw new Exception("Nincs szabad tűzoltó vagy tűzoltóautó.");

            Console.WriteLine($"Tűzoltó {szabadTuzolto.Nev} és tűzoltóautó {szabadAuto.Rendszam} indul a tüzet oltani a(z) {epulet.Cim} épületben.");

            szabadTuzolto.Riasztas(epulet);
            szabadAuto.Kivonul(szabadTuzolto, epulet);
            szabadTuzolto.Tuzoltas(epulet);
            int felhasznaltViz = VizetHasznalTuzoltasra(epulet);
            return felhasznaltViz;
        }

        private int VizetHasznalTuzoltasra(Epulet epulet)
        {
            int alapVizIgeny = 0;
            switch (epulet.TuzTipus)
            {
                case TuzTipus.Kozonseges:
                    alapVizIgeny = 500;
                    break;
                case TuzTipus.Olaj:
                    alapVizIgeny = 800;
                    break;
                case TuzTipus.Elektromos:
                    alapVizIgeny = 300;
                    break;
                default:
                    throw new Exception("Ismeretlen tűztípus.");
            }

            double szorzo = 1.0;
            switch (epulet.Tipus)
            {
                case EpuletTipus.Lako:
                    szorzo = 1.0;
                    break;
                case EpuletTipus.Iroda:
                    szorzo = 1.2;
                    break;
                case EpuletTipus.Gyar:
                    szorzo = 1.5;
                    break;
                default:
                    throw new Exception("Ismeretlen épülettípus.");
            }

            int szuksegesViz = (int)(alapVizIgeny * szorzo);

            Vizforras megfeleloVizforras = null!;
            for (int i = 0; i < Vizforrasok.Count; i++)
            {
                if (Vizforrasok[i].VizMennyiseg >= szuksegesViz)
                {
                    megfeleloVizforras = Vizforrasok[i];
                    break;
                }
            }

            if (megfeleloVizforras == null)
                throw new Exception("Nincs elegendő víz a tűzoltáshoz.");

            megfeleloVizforras.VizetHasznal(szuksegesViz);
            return szuksegesViz;
        }

        public void EpuletElveszt(Epulet epulet)
        {
            if (Epuletek.Contains(epulet))
            {
                Epuletek.Remove(epulet);
                Leegtek.Add(epulet);
            }
        }

        public void Visszaallit()
        {
            for (int i = 0; i < Tuzoltok.Count; i++)
            {
                Tuzoltok[i].Reset();
            }

            for (int i = 0; i < TuzoltoAutok.Count; i++)
            {
                TuzoltoAutok[i].Reset();
            }
        }
    }
}