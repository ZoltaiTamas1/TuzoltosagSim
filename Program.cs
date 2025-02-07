using System;

namespace TuzoltosagSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Varos varos = new Varos();
            Tuzolto tuzolto1 = new Tuzolto("Kiss János");
            Tuzolto tuzolto2 = new Tuzolto("Nagy Péter");
            Tuzoltoauto auto1 = new Tuzoltoauto("ABC-123");
            Tuzoltoauto auto2 = new Tuzoltoauto("XYZ-456");

            varos.TuzoltoHozzaad(tuzolto1);
            varos.TuzoltoHozzaad(tuzolto2);
            varos.TuzoltoautoHozzaad(auto1);
            varos.TuzoltoautoHozzaad(auto2);

            while (true)
            {
                try
                {
                    Console.WriteLine("Adja meg az épület címét, ahol a tűz kezdődött:");
                    string cim = Console.ReadLine()!;

                    Console.WriteLine("Adja meg a tűz típusát (Közönséges, Olaj, Elektromos):");
                    string tuzTipusInput = Console.ReadLine()!;

                    if (!Enum.TryParse<TuzTipus>(tuzTipusInput, true, out TuzTipus tuzTipus))
                    {
                        Console.WriteLine("Érvénytelen tűz típus megadás. Alapértelmezettként 'Közönséges' lesz használva.");
                        tuzTipus = TuzTipus.Közönséges;
                    }

                    Epulet userEpulet = new Epulet(cim);
                    varos.EpuletHozzaad(userEpulet);

                    userEpulet.TuzKiindul(tuzTipus);
                    Console.WriteLine($"Tűz indult az épületen: {userEpulet.Cim} (típus: {tuzTipus})");

                    Console.WriteLine($"Tűzoltás indul az épület címen: {userEpulet.Cim}");
                    varos.Tuzoltas(userEpulet);
                    Console.WriteLine($"Tűzoltás befejeződött az épület címen: {userEpulet.Cim}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt: {ex.Message}");
                }

                Console.WriteLine("\nHa ki akar lépni, írja be a 0-át. Bármilyen más billentyűvel folytathatja a szimulációt.");
                string exitChoice = Console.ReadLine()!;
                if (exitChoice.Trim() == "0")
                {
                    break;
                }

                Console.WriteLine(new string('-', 50));
            }
        }
    }
}
