using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TuzoltosagSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Varos varos = new Varos();
            bool futas = true;

            while (futas)
            {
                Console.Clear();
                Console.WriteLine("=== Tűzoltó Szimulátor ===");
                Console.WriteLine("1. Épület hozzáadása");
                Console.WriteLine("2. Tűzoltó hozzáadása");
                Console.WriteLine("3. Tűzoltóautó hozzáadása");
                Console.WriteLine("4. Vízkút hozzáadása");
                Console.WriteLine("5. Tűz indítása");
                Console.WriteLine("6. Tűz oltása");
                Console.WriteLine("7. Szimuláció indítása");
                Console.WriteLine("8. Kilépés");
                Console.Write("Válassz egy opciót: ");

                string valasztas = Console.ReadLine()!;

                switch (valasztas)
                {
                    case "1":
                        EpuletHozzaad(varos);
                        break;
                    case "2":
                        TuzoltoHozzaad(varos);
                        break;
                    case "3":
                        TuzoltoautoHozzaad(varos);
                        break;
                    case "4":
                        VizforrasHozzaad(varos);
                        break;
                    case "5":
                        TuzInditas(varos);
                        break;
                    case "6":
                        TuzOltas(varos);
                        break;
                    case "7":
                        SzimulacioInditasa(varos);
                        break;
                    case "8":
                        futas = false;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás. Nyomj egy gombot az újrapróbálkozáshoz.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void EpuletHozzaad(Varos varos)
        {
            Console.WriteLine("--------------------------------------------------\n");
            Console.Write("Épület címe: ");
            string cim = Console.ReadLine()!;

            EpuletTipus tipus;
            while (true)
            {
                Console.WriteLine("Épület típusa (1: Lakó, 2: Iroda, 3: Gyár): ");
                string tipusValasztas = Console.ReadLine()!;
                try
                {
                    tipus = tipusValasztas switch
                    {
                        "1" => EpuletTipus.Lako,
                        "2" => EpuletTipus.Iroda,
                        "3" => EpuletTipus.Gyar,
                        _ => throw new Exception("Érvénytelen típus.")
                    };
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Érvénytelen típus. Add meg újra!");
                }
            }

            varos.EpuletHozzaad(new Epulet(cim, tipus));
            Console.WriteLine("Épület hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void TuzoltoHozzaad(Varos varos)
        {
            Console.WriteLine("--------------------------------------------------\n");
            Console.Write("Tűzoltó neve: ");
            string nev = Console.ReadLine()!;
            varos.TuzoltoHozzaad(new Tuzolto(nev));
            Console.WriteLine("Tűzoltó hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void TuzoltoautoHozzaad(Varos varos)
        {
            string rendszam;
            while (true)
            {
                Console.WriteLine("--------------------------------------------------\n");
                Console.Write("Tűzoltóautó rendszáma (Formátum: ABC123): ");
                rendszam = Console.ReadLine()!.ToUpper();
                if (Regex.IsMatch(rendszam, @"^[A-Z]{3}\d{3}$"))
                {
                    break;
                }
                Console.WriteLine("Érvénytelen formátum! Használj 3 betűt és 3 számot (pl. ABC123).");
            }

            varos.TuzoltoautoHozzaad(new Tuzoltoauto(rendszam));
            Console.WriteLine("Tűzoltóautó hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void VizforrasHozzaad(Varos varos)
        {
            Console.WriteLine("--------------------------------------------------\n");
            Console.Write("Vízkút helye: ");
            string hely = Console.ReadLine()!;
            int kapacitas;
            while (true)
            {
                Console.Write("Vízkút kapacitása: ");
                if (int.TryParse(Console.ReadLine(), out kapacitas) && kapacitas > 0)
                {
                    break;
                }
                Console.WriteLine("Érvénytelen kapacitás. Add meg újra!");
            }

            varos.VizforrasHozzaad(new Vizforras(hely, kapacitas));
            Console.WriteLine("Vízkút hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void TuzInditas(Varos varos)
        {
            Console.WriteLine("--------------------------------------------------\n");
            if (varos.Epuletek.Count == 0)
            {
                Console.WriteLine("Nincs épület hozzáadva. Nyomj egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Válassz egy épületet a tűz indításához:");
            for (int i = 0; i < varos.Epuletek.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {varos.Epuletek[i].Cim}");
            }

            int epuletIndex;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out epuletIndex) && epuletIndex > 0 && epuletIndex <= varos.Epuletek.Count)
                {
                    epuletIndex--;
                    break;
                }
                Console.WriteLine("Érvénytelen választás. Add meg újra!");
            }

            TuzTipus tuzTipus;
            while (true)
            {
                Console.WriteLine("Tűz típusa (1: Közönséges, 2: Olaj, 3: Elektromos): ");
                string tuzTipusValasztas = Console.ReadLine()!;
                try
                {
                    tuzTipus = tuzTipusValasztas switch
                    {
                        "1" => TuzTipus.Kozonseges,
                        "2" => TuzTipus.Olaj,
                        "3" => TuzTipus.Elektromos,
                        _ => throw new Exception("Érvénytelen típus.")
                    };
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Érvénytelen típus. Add meg újra!");
                }
            }

            varos.Epuletek[epuletIndex].TuzKiindul(tuzTipus);
            Console.WriteLine($"Tűz indítva a(z) {varos.Epuletek[epuletIndex].Cim} címen. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void TuzOltas(Varos varos)
        {
            Console.WriteLine("--------------------------------------------------\n");
            if (varos.Epuletek.Count == 0)
            {
                Console.WriteLine("Nincs épület hozzáadva. Nyomj egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Válassz egy épületet a tűz oltásához:");
            for (int i = 0; i < varos.Epuletek.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {varos.Epuletek[i].Cim} (Tűz: {(varos.Epuletek[i].TuzVan ? "Van" : "Nincs")})");
            }

            int epuletIndex;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out epuletIndex) && epuletIndex > 0 && epuletIndex <= varos.Epuletek.Count)
                {
                    epuletIndex--;
                    break;
                }
                Console.WriteLine("Érvénytelen választás. Add meg újra!");
            }

            try
            {
                int felhasznaltViz = varos.Tuzoltas(varos.Epuletek[epuletIndex]);
                Console.WriteLine($"Tűz sikeresen eloltva a(z) {varos.Epuletek[epuletIndex].Cim} címen. Felhasznált víz: {felhasznaltViz} liter.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }

            Console.WriteLine("Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void SzimulacioInditasa(Varos varos)
        {
            if (varos.Epuletek.Count == 0)
            {
                Console.WriteLine("Nincs épület a szimulációhoz. Nyomj egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            Random rnd = new Random();
            List<Epulet> tuzesEpuletek = new List<Epulet>();

            foreach (var epulet in varos.Epuletek)
            {
                if (rnd.NextDouble() < 0.4 && !epulet.TuzVan)
                {
                    try
                    {
                        var tuzTipus = (TuzTipus)rnd.Next(0, 3);
                        epulet.TuzKiindul(tuzTipus);
                        tuzesEpuletek.Add(epulet);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hiba a(z) {epulet.Cim} épületnél: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("\nSzimuláció eredménye:");
            if (tuzesEpuletek.Count > 0)
            {
                Console.WriteLine("A következő épületekben történt tűz:");
                foreach (var epulet in tuzesEpuletek)
                {
                    Console.WriteLine($"- {epulet.Cim} ({epulet.TuzTipus} típusú tűz)");
                }
            }
            else
            {
                Console.WriteLine("Egyetlen épületben sem történt tűz.");
            }

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }
    }
}