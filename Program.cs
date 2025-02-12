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
            int osszPoroltoMennyiseg = 10;

            Varos varos = new Varos();

            varos.TuzoltoHozzaad(new Tuzolto("Kiss János"));
            varos.TuzoltoHozzaad(new Tuzolto("Nagy Péter"));

            varos.TuzoltoautoHozzaad(new Tuzoltoauto("ABC-123"));
            varos.TuzoltoautoHozzaad(new Tuzoltoauto("XYZ-456"));

            varos.VizforrasHozzaad(new Vizforras("Civert", 4000));
            varos.VizforrasHozzaad(new Vizforras("Fő vízállomás", 15000));

            string kilepesValasztas;
            do
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("Új tűzeset szimulációja");

                try
                {
                    Console.WriteLine("Adja meg az épület címét, ahol a tűz kezdődött:");
                    string cim = Console.ReadLine()!;

                    Console.WriteLine("Adja meg az épület típusát (Lakó, Iroda, Gyar):");
                    string epuletTipusBemenet = Console.ReadLine()!;
                    EpuletTipus epuletTipus;

                    switch (epuletTipusBemenet.Trim().ToLower())
                    {
                        case "lakó":
                        case "lako":
                            epuletTipus = EpuletTipus.Lako;
                            break;
                        case "iroda":
                            epuletTipus = EpuletTipus.Iroda;
                            break;
                        case "gyár":
                        case "gyar":
                            epuletTipus = EpuletTipus.Gyar;
                            break;
                        default:
                            Console.WriteLine("Érvénytelen épület típus, alapértelmezetten 'Lakó' lesz használva.");
                            epuletTipus = EpuletTipus.Lako;
                            break;
                    }

                    Console.WriteLine("Adja meg a tűz típusát (Közönséges, Olaj, Elektromos):");
                    string tuzTipusBemenet = Console.ReadLine()!;
                    TuzTipus tuzTipus;

                    switch (tuzTipusBemenet.Trim().ToLower())
                    {
                        case "közönséges":
                        case "kozonseges":
                            tuzTipus = TuzTipus.Kozonseges;
                            break;
                        case "olaj":
                            tuzTipus = TuzTipus.Olaj;
                            break;
                        case "elektromos":
                            tuzTipus = TuzTipus.Elektromos;
                            break;
                        default:
                            Console.WriteLine("Érvénytelen tűz típus, alapértelmezetten 'Közönséges' lesz használva.");
                            tuzTipus = TuzTipus.Kozonseges;
                            break;
                    }

                    if (tuzTipus == TuzTipus.Elektromos)
                    {
                        osszPoroltoMennyiseg -= 2;
                        Epulet felhasznaloEpulet = new Epulet(cim, epuletTipus);
                        varos.EpuletHozzaad(felhasznaloEpulet);
                        if (osszPoroltoMennyiseg < 0)
                        {
                            Console.WriteLine($"\nTűzoltás sikertelen volt az épület címen: {felhasznaloEpulet.Cim}, mivel elfogytak a poroltók.");
                            sikertelenEsemenyek += 1;
                        }
                        else
                        {
                            felhasznaloEpulet.TuzKiindul(tuzTipus);
                            Console.WriteLine($"\nTűz indult az épületen: {felhasznaloEpulet.Cim} (épület típus: {felhasznaloEpulet.Tipus}, tűz típus: {felhasznaloEpulet.TuzTipus})");
                            Console.WriteLine($"Tűzoltás indul az épület címen: {felhasznaloEpulet.Cim}...");
                            Console.WriteLine($"Tűzoltás befejeződött az épület címen: {felhasznaloEpulet.Cim}. {10 - osszPoroltoMennyiseg} poroltót használtak fel.");
                            sikeresEsemenyek++;
                        }
                    }
                    else if (tuzTipus == TuzTipus.Olaj)
                    {
                        osszPoroltoMennyiseg -= 1;
                        Epulet felhasznaloEpulet = new Epulet(cim, epuletTipus);
                        varos.EpuletHozzaad(felhasznaloEpulet);
                        if (osszPoroltoMennyiseg < 0)
                        {
                            Console.WriteLine($"\nTűzoltás sikertelen volt az épület címen: {felhasznaloEpulet.Cim}, mivel elfogytak a poroltók.");
                            sikertelenEsemenyek += 1;
                        }
                        else
                        {
                            felhasznaloEpulet.TuzKiindul(tuzTipus);
                            Console.WriteLine($"\nTűz indult az épületen: {felhasznaloEpulet.Cim} (épület típus: {felhasznaloEpulet.Tipus}, tűz típus: {felhasznaloEpulet.TuzTipus})");
                            Console.WriteLine($"Tűzoltás indul az épület címen: {felhasznaloEpulet.Cim}...");
                            Console.WriteLine($"Tűzoltás befejeződött az épület címen: {felhasznaloEpulet.Cim}. {10 - osszPoroltoMennyiseg} poroltót használtak fel.");
                            sikeresEsemenyek++;
                        }
                    }
                    else
                    {
                        Epulet felhasznaloEpulet = new Epulet(cim, epuletTipus);
                        varos.EpuletHozzaad(felhasznaloEpulet);
                        felhasznaloEpulet.TuzKiindul(tuzTipus);
                        Console.WriteLine($"\nTűz indult az épületen: {felhasznaloEpulet.Cim} (épület típus: {felhasznaloEpulet.Tipus}, tűz típus: {felhasznaloEpulet.TuzTipus})");
                        Console.WriteLine($"Tűzoltás indul az épület címen: {felhasznaloEpulet.Cim}...");
                        int felhasznaltViz = varos.Tuzoltas(felhasznaloEpulet);
                        osszesFelhasznaltViz += felhasznaltViz;
                        Console.WriteLine($"Tűzoltás befejeződött az épület címen: {felhasznaloEpulet.Cim}. {felhasznaltViz} liter vizet használtak.");
                        sikeresEsemenyek++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nHiba történt: {ex.Message}");
                    sikertelenEsemenyek++;
                }

                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("Szimuláció statisztikák:");
                Console.WriteLine($"Megoldott esetek: {sikeresEsemenyek}");
                Console.WriteLine($"Hibás esetek: {sikertelenEsemenyek}");
                Console.WriteLine($"Összesen felhasznált víz: {osszesFelhasznaltViz} liter");
                Console.WriteLine($"Maradék poroltó: {osszPoroltoMennyiseg}");
                Console.WriteLine("--------------------------------------------------\n");

                Console.WriteLine("\nKilépéshez írja be a 0-át, vagy bármilyen más billentyűvel folytathatja a szimulációt.");
                kilepesValasztas = Console.ReadLine()!;
            }
            while (kilepesValasztas.Trim() != "0");

            Console.WriteLine("A szimuláció befejeződött. Viszlát!");
        }
    }
}
