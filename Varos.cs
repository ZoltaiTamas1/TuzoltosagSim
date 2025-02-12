using System;
using System.Collections.Generic;
using System.Linq;

namespace TuzoltosagSim
{
    public class Varos
    {
        public List<Epulet> Epuletek { get; private set; }
        public List<Tuzolto> Tuzoltok { get; private set; }
        public List<Tuzoltoauto> TuzoltoAutok { get; private set; }
        public List<Vizforras> Vizforrasok { get; private set; }

        public Varos()
        {
            Epuletek = new List<Epulet>();
            Tuzoltok = new List<Tuzolto>();
            TuzoltoAutok = new List<Tuzoltoauto>();
            Vizforrasok = new List<Vizforras>();
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
            foreach (var tuzolto in Tuzoltok)
            {
                if (tuzolto.Szabad)
                {
                    szabadTuzolto = tuzolto;
                    break;
                }
            }

            Tuzoltoauto szabadAuto = null!;
            foreach (var auto in TuzoltoAutok)
            {
                if (auto.Szabad)
                {
                    szabadAuto = auto;
                    break;
                }
            }

            if (szabadTuzolto == null || szabadAuto == null)
            {
                throw new Exception("Nincs szabad tűzoltó vagy tűzoltóautó.");
            }

            szabadTuzolto.Riasztas(epulet);
            szabadAuto.Kivonul(szabadTuzolto, epulet);
            szabadTuzolto.Tuzoltas(epulet);
            szabadAuto.Visszaerkezik();

            int felhasznaltViz = VizetHasznalTuzoltasra(epulet);
            return felhasznaltViz;
        }

        private int VizetHasznalTuzoltasra(Epulet epulet)
        {
            int alapVizIgeny = 0;
            if (epulet.TuzTipus == TuzTipus.Közönséges)
            {
                alapVizIgeny = 500;
            }
            else if (epulet.TuzTipus == TuzTipus.Olaj)
            {
                alapVizIgeny = 800;
            }
            else if (epulet.TuzTipus == TuzTipus.Elektromos)
            {
                alapVizIgeny = 300;
            }

            double szorzo = 1.0;
            if (epulet.Tipus == BuildingType.Lako)
            {
                szorzo = 1.0;
            }
            else if (epulet.Tipus == BuildingType.Iroda)
            {
                szorzo = 1.2;
            }
            else if (epulet.Tipus == BuildingType.Gyar)
            {
                szorzo = 1.5;
            }

            int szuksegesViz = (int)(alapVizIgeny * szorzo);

            Vizforras megfeleloVizforras = null!;
            foreach (var vizforras in Vizforrasok)
            {
                if (vizforras.VizMennyiseg >= szuksegesViz)
                {
                    megfeleloVizforras = vizforras;
                    break;
                }
            }

            if (megfeleloVizforras != null)
            {
                megfeleloVizforras.VizetHasznal(szuksegesViz);
                return szuksegesViz;
            }
            else
            {
                throw new Exception("Nincs elegendő víz a tűzoltáshoz.");
            }
        }
    }
}
