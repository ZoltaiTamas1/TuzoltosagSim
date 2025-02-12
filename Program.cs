using System;

namespace TuzoltosagSim
{
    class Program
    {
        static void Main(string[] args)
        {
            int successfulIncidents = 0;
            int failedIncidents = 0;
            int totalWaterUsed = 0;

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
                    string buildingTypeInput = Console.ReadLine()!;
                    BuildingType buildingType;
                    if (!Enum.TryParse<BuildingType>(buildingTypeInput, true, out buildingType))
                    {
                        Console.WriteLine("Érvénytelen épület típus, alapértelmezetten 'Lakó' lesz használva.");
                        buildingType = BuildingType.Lako;
                    }

                    Console.WriteLine("Adja meg a tűz típusát (Közönséges, Olaj, Elektromos):");
                    string tuzTipusInput = Console.ReadLine()!;
                    TuzTipus tuzTipus;
                    if (!Enum.TryParse<TuzTipus>(tuzTipusInput, true, out tuzTipus))
                    {
                        Console.WriteLine("Érvénytelen tűz típus, alapértelmezetten 'Közönséges' lesz használva.");
                        tuzTipus = TuzTipus.Közönséges;
                    }

                    Epulet userEpulet = new Epulet(cim, buildingType);
                    varos.EpuletHozzaad(userEpulet);

                    userEpulet.TuzKiindul(tuzTipus);
                    Console.WriteLine($"Tűz indult az épületen: {userEpulet.Cim} (épület típus: {userEpulet.Tipus}, tűz típus: {userEpulet.TuzTipus})");

                    Console.WriteLine($"Tűzoltás indul az épület címen: {userEpulet.Cim}...");
                    int waterUsed = varos.Tuzoltas(userEpulet);
                    totalWaterUsed += waterUsed;
                    Console.WriteLine($"Tűzoltás befejeződött az épület címen: {userEpulet.Cim}. {waterUsed} liter vizet használtak.");

                    successfulIncidents++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt: {ex.Message}");
                    failedIncidents++;
                }

                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Szimuláció statisztikák:");
                Console.WriteLine($"Megoldott esetek: {successfulIncidents}");
                Console.WriteLine($"Hibás esetek: {failedIncidents}");
                Console.WriteLine($"Összesen felhasznált víz: {totalWaterUsed} liter");
                Console.WriteLine(new string('-', 50));

                Console.WriteLine("Kilépéshez írja be a 0-át, vagy bármilyen más billentyűvel folytathatja a szimulációt.");
                string exitChoice = Console.ReadLine()!;
                if (exitChoice.Trim() == "0")
                {
                    break;
                }
            }

            Console.WriteLine("A szimuláció befejeződött. Viszlát!");
        }
    }
}
