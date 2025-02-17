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
                KirajzolFejlec("=== Tűzoltó Szimulátor ===");
                Console.WriteLine("1. Épület hozzáadása");
                Console.WriteLine("2. Tűzoltó hozzáadása");
                Console.WriteLine("3. Tűzoltóautó hozzáadása");
                Console.WriteLine("4. Vízkút hozzáadása");
                Console.WriteLine("5. Szimuláció indítása");
                Console.WriteLine("6. Kilépés");
                Console.Write("\nVálassz egy opciót: ");

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
                        SzimulacioInditasa(varos);
                        break;
                    case "6":
                        futas = false;
                        break;
                    default:
                        HibaKiirasa("Érvénytelen választás. Nyomj egy gombot az újrapróbálkozáshoz.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void KirajzolFejlec(string cim)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', cim.Length));
            Console.WriteLine(cim);
            Console.WriteLine(new string('=', cim.Length));
            Console.ResetColor();
            Console.WriteLine();
        }

        static void HibaKiirasa(string uzenet)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(uzenet);
            Console.ResetColor();
        }

        static void SikerKiirasa(string uzenet)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(uzenet);
            Console.ResetColor();
        }

        static void EpuletHozzaad(Varos varos)
        {
            Console.Clear();
            KirajzolFejlec("=== Épület Hozzáadása ===");
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
                    HibaKiirasa("Érvénytelen típus. Add meg újra!");
                }
            }

            varos.EpuletHozzaad(new Epulet(cim, tipus));
            SikerKiirasa("Épület hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void TuzoltoHozzaad(Varos varos)
        {
            Console.Clear();
            KirajzolFejlec("=== Tűzoltó Hozzáadása ===");
            Console.Write("Tűzoltó neve: ");
            string nev = Console.ReadLine()!;
            varos.TuzoltoHozzaad(new Tuzolto(nev));
            SikerKiirasa("Tűzoltó hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void TuzoltoautoHozzaad(Varos varos)
        {
            Console.Clear();
            KirajzolFejlec("=== Tűzoltóautó Hozzáadása ===");
            string rendszam;
            while (true)
            {
                Console.Write("Tűzoltóautó rendszáma (Formátum: ABC123): ");
                rendszam = Console.ReadLine()!.ToUpper();
                if (Regex.IsMatch(rendszam, @"^[A-Z]{3}\d{3}$"))
                    break;

                HibaKiirasa("Érvénytelen formátum! Használj 3 betűt és 3 számot (pl. ABC123).");
            }

            varos.TuzoltoautoHozzaad(new Tuzoltoauto(rendszam));
            SikerKiirasa("Tűzoltóautó hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void VizforrasHozzaad(Varos varos)
        {
            Console.Clear();
            KirajzolFejlec("=== Vízkút Hozzáadása ===");
            Console.Write("Vízkút helye: ");
            string hely = Console.ReadLine()!;
            int kapacitas;
            while (true)
            {
                Console.Write("Vízkút kapacitása: ");
                if (int.TryParse(Console.ReadLine(), out kapacitas) && kapacitas > 0)
                    break;

                HibaKiirasa("Érvénytelen kapacitás. Add meg újra!");
            }

            varos.VizforrasHozzaad(new Vizforras(hely, kapacitas));
            SikerKiirasa("Vízkút hozzáadva. Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        static void SzimulacioInditasa(Varos varos)
        {
            Console.Clear();
            KirajzolFejlec("=== Szimuláció Indítása ===");
            if (varos.Epuletek.Count == 0)
            {
                HibaKiirasa("Nincs épület a szimulációhoz. Nyomj egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            Random rnd = new Random();
            List<Epulet> tuzesEpuletek = new List<Epulet>();
            List<Epulet> leegtek = new List<Epulet>();

            foreach (var epulet in varos.Epuletek)
            {
                if (epulet.TuzVan)
                {
                    if (rnd.NextDouble() < 0.5)
                    {
                        varos.EpuletElveszt(epulet);
                        leegtek.Add(epulet);
                        HibaKiirasa($"A(z) {epulet.Cim} címen lévő épület leégett!");
                    }
                }
                else if (rnd.NextDouble() < 0.4)
                {
                    try
                    {
                        var tuzTipus = (TuzTipus)rnd.Next(0, 3);
                        epulet.TuzKiindul(tuzTipus);
                        tuzesEpuletek.Add(epulet);
                    }
                    catch (Exception ex)
                    {
                        HibaKiirasa($"Hiba a(z) {epulet.Cim} épületnél: {ex.Message}");
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

            if (leegtek.Count > 0)
            {
                Console.WriteLine("\nLeégett épületek:");
                foreach (var epulet in leegtek)
                {
                    Console.WriteLine($"- {epulet.Cim}");
                }
            }

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }
    }
}