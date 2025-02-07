using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuzoltosagSim
{
    public class Varos
    {
        public List<Epulet> Epuletek { get; private set; }
        public List<Tuzolto> TuzoltoK { get; private set; }
        public List<Tuzoltoauto> TuzoltoAutok { get; private set; }

        public Varos()
        {
            Epuletek = new List<Epulet>();
            TuzoltoK = new List<Tuzolto>();
            TuzoltoAutok = new List<Tuzoltoauto>();
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

        public void Tuzoltas(Epulet epulet)
        {
            var szabadTuzolto = TuzoltoK.FirstOrDefault(t => t.Szabad);
            var szabadAuto = TuzoltoAutok.FirstOrDefault(a => a.Szabad);

            if (szabadTuzolto == null || szabadAuto == null)
                throw new InvalidOperationException("Nincs szabad tűzoltó vagy tűzoltóautó.");

            szabadTuzolto.Riasztas(epulet);
            szabadAuto.Kivonul(szabadTuzolto, epulet);
            szabadTuzolto.Tuzoltas(epulet);
            szabadAuto.Visszaérkezik();
        }
    }
}
