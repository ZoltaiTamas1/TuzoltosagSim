using System;

namespace TuzoltosagSim
{
    class Program
    {
        static void Main(string[] args)
        {
            int sikeresEsemenyek = 0;
            int sikertelenEsemenyek = 0;
            int osszesFelhasznaltViz = 0;

            Varos varos = new Varos();

            varos.TuzoltoHozzaad(new Tuzolto("Kiss János"));
            varos.TuzoltoHozzaad(new Tuzolto("Nagy Péter"));

            varos.TuzoltoautoHozzaad(new Tuzoltoauto("ABC-123"));
            varos.TuzoltoautoHozzaad(new Tuzoltoauto("XYZ-456"));

            varos.VizforrasHozzaad(new Vizforras("Civert", 10000));
            varos.VizforrasHozzaad(new Vizforras("Fő vízállomás", 15000));

            while (true)
            {
                Console.WriteLine(new string('=', 50));
                Console.WriteLine("Új tűzeset szimulációja");

                try
                {
                    Console.WriteLine("Adja meg az épület címét, ahol a tűz kezdődött:");
                    string cim = Console.ReadLine()!;

                    Console.WriteLine("Adja meg az épület típusát (Lakó, Iroda, Gyar):");
                    string epuletTipusBemenet = Console.ReadLine()!;
                    EpuletTipus epuletTipus;
                    if (!Enum.TryParse<EpuletTipus>(epuletTipusBemenet, true, out epuletTipus))
                    {
                        Console.WriteLine("Érvénytelen épület típus, alapértelmezetten 'Lakó' lesz használva.");
                        epuletTipus = EpuletTipus.Lako;
                    }

                    Console.WriteLine("Adja meg a tűz típusát (Közönséges, Olaj, Elektromos):");
                    string tuzTipusBemenet = Console.ReadLine()!;
                    TuzTipus tuzTipus;
                    if (!Enum.TryParse<TuzTipus>(tuzTipusBemenet, true, out tuzTipus))
                    {
                        Console.WriteLine("Érvénytelen tűz típus, alapértelmezetten 'Közönséges' lesz használva.");
                        tuzTipus = TuzTipus.Kozonseges;
                    }

                    Epulet felhasznaloEpulet = new Epulet(cim, epuletTipus);
                    varos.EpuletHozzaad(felhasznaloEpulet);

                    felhasznaloEpulet.TuzKiindul(tuzTipus);
                    Console.WriteLine($"Tűz indult az épületen: {felhasznaloEpulet.Cim} (épület típus: {felhasznaloEpulet.Tipus}, tűz típus: {felhasznaloEpulet.TuzTipus})");

                    Console.WriteLine($"Tűzoltás indul az épület címen: {felhasznaloEpulet.Cim}...");
                    int felhasznaltViz = varos.Tuzoltas(felhasznaloEpulet);
                    osszesFelhasznaltViz += felhasznaltViz;
                    Console.WriteLine($"Tűzoltás befejeződött az épület címen: {felhasznaloEpulet.Cim}. {felhasznaltViz} liter vizet használtak.");

                    sikeresEsemenyek++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt: {ex.Message}");
                    sikertelenEsemenyek++;
                }

                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Szimuláció statisztikák:");
                Console.WriteLine($"Megoldott esetek: {sikeresEsemenyek}");
                Console.WriteLine($"Hibás esetek: {sikertelenEsemenyek}");
                Console.WriteLine($"Összesen felhasznált víz: {osszesFelhasznaltViz} liter");
                Console.WriteLine(new string('-', 50));

                Console.WriteLine("Kilépéshez írja be a 0-át, vagy bármilyen más billentyűvel folytathatja a szimulációt.");
                string kilepesValasztas = Console.ReadLine()!;
                if (kilepesValasztas.Trim() == "0")
                {
                    break;
                }
            }

            Console.WriteLine("A szimuláció befejeződött. Viszlát!");
        }
    }
}
