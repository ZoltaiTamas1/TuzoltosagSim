using System;

namespace TuzoltosagSim
{
    public class Epulet
    {
        public string Cim { get; private set; }
        public bool TuzVan { get; private set; }
        public TuzTipus TuzTipus { get; private set; }

        public Epulet(string cim)
        {
            Cim = cim;
            TuzVan = false;
            TuzTipus = TuzTipus.Közönséges;
        }

        public void TuzKiindul(TuzTipus tuzTipus)
        {
            if (TuzVan)
                throw new InvalidOperationException($"A(z) {Cim} címen már van tűz.");

            TuzVan = true;
            TuzTipus = tuzTipus;
            // (No direct output here.)
        }

        public void TuzEloltva()
        {
            if (!TuzVan)
                throw new InvalidOperationException($"A(z) {Cim} címen nincs tűz.");

            TuzVan = false;
            // (No direct output here.)
        }
    }
}
