using System;

namespace TuzoltosagSim
{
    public class Epulet
    {
        public string Cim { get; private set; }
        public bool TuzVan { get; private set; }
        public TuzTipus TuzTipus { get; private set; }
        public EpuletTipus Tipus { get; private set; }

        public Epulet(string cim, EpuletTipus tipus)
        {
            Cim = cim;
            Tipus = tipus;
            TuzVan = false;
            TuzTipus = TuzTipus.Kozonseges;
        }

        public void TuzKiindul(TuzTipus tuzTipus)
        {
            if (TuzVan)
                throw new Exception($"A(z) {Cim} címen már van tűz.");

            TuzVan = true;
            TuzTipus = tuzTipus;
        }

        public void TuzEloltva()
        {
            if (!TuzVan)
                throw new Exception($"A(z) {Cim} címen nincs tűz.");

            TuzVan = false;
        }
    }
}
