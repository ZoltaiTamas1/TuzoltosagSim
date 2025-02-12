using System;
using System.Collections.Generic;
using System.Linq;

namespace TuzoltosagSim
{
    public class Varos
    {
        public List<Epulet> Epuletek { get; private set; }
        public List<Tuzolto> TuzoltoK { get; private set; }
        public List<Tuzoltoauto> TuzoltoAutok { get; private set; }
        public List<Vizforras> Vizforrasok { get; private set; }

        public Varos()
        {
            Epuletek = new List<Epulet>();
            TuzoltoK = new List<Tuzolto>();
            TuzoltoAutok = new List<Tuzoltoauto>();
            Vizforrasok = new List<Vizforras>();
        }

        public void EpuletHozzaad(Epulet epulet)
        {
            Epuletek.Add(epulet);
        }

        public void TuzoltoHozzaad(Tuzolto tuzolto)
        {
            TuzoltoK.Add(tuzolto);
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
            var szabadTuzolto = TuzoltoK.FirstOrDefault(t => t.Szabad);
            var szabadAuto = TuzoltoAutok.FirstOrDefault(a => a.Szabad);

            if (szabadTuzolto == null || szabadAuto == null)
                throw new Exception("Nincs szabad tűzoltó vagy tűzoltóautó.");

            szabadTuzolto.Riasztas(epulet);
            szabadAuto.Kivonul(szabadTuzolto, epulet);
            szabadTuzolto.Tuzoltas(epulet);
            szabadAuto.Visszaérkezik();

            int waterUsed = UseWaterForFire(epulet);
            return waterUsed;
        }

        private int UseWaterForFire(Epulet epulet)
        {
            int baseWaterNeeded = 0;
            switch (epulet.TuzTipus)
            {
                case TuzTipus.Közönséges:
                    baseWaterNeeded = 500;
                    break;
                case TuzTipus.Olaj:
                    baseWaterNeeded = 800;
                    break;
                case TuzTipus.Elektromos:
                    baseWaterNeeded = 300;
                    break;
            }

            double factor = 1.0;
            switch (epulet.Tipus)
            {
                case BuildingType.Lako:
                    factor = 1.0;
                    break;
                case BuildingType.Iroda:
                    factor = 1.2;
                    break;
                case BuildingType.Gyar:
                    factor = 1.5;
                    break;
            }

            int waterNeeded = (int)(baseWaterNeeded * factor);

            var waterSource = Vizforrasok.FirstOrDefault(v => v.VizMennyiseg >= waterNeeded);
            if (waterSource != null)
            {
                waterSource.VizetHasznal(waterNeeded);
                return waterNeeded;
            }
            else
            {
                throw new Exception("Nincs elegendo viz a tuzeszoltashoz.");
            }
        }
    }
}
