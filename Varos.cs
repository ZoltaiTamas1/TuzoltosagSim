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
    }
}
