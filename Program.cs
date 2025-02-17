using System;
using System.Collections.Generic;
using System.Linq;
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
                        Console.Clear();
                        KirajzolFejlec("=== Játék Statisztikák ===");
                        Console.WriteLine($"\nEloltott épületek száma: {varos.EloltottakSzama}");
                        Console.WriteLine($"Leégett épületek száma: {varos.LeegtekSzama}");
                        Console.WriteLine($"Felhasznált víz összmennyisége: {varos.OsszesVizFelhasznalva} liter");
                        Console.WriteLine("\nNyomj egy gombot a kilépéshez.");
                        Console.ReadKey();
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
            Console.ForegroundColor = ConsoleColor.Red;
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

            EpuletTipus tipus = EpuletTipus.Lako;
            bool ervenyesValasztas = false;

            do
            {
                Console.WriteLine("Épület típusa (1: Lakó, 2: Iroda, 3: Gyár): ");
                string tipusValasztas = Console.ReadLine()!;
                switch (tipusValasztas)
                {
                    case "1":
                        tipus = EpuletTipus.Lako;
                        ervenyesValasztas = true;
                        break;
                    case "2":
                        tipus = EpuletTipus.Iroda;
                        ervenyesValasztas = true;
                        break;
                    case "3":
                        tipus = EpuletTipus.Gyar;
                        ervenyesValasztas = true;
                        break;
                    default:
                        HibaKiirasa("Érvénytelen típus. Add meg újra!");
                        break;
                }
            } while (!ervenyesValasztas);

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

            foreach (var epulet in varos.Epuletek)
            {
                if (!epulet.TuzVan && rnd.NextDouble() < 0.3)
                {
                    var tuzTipus = (TuzTipus)rnd.Next(0, 3);
                    try
                    {
                        epulet.TuzKiindul(tuzTipus);
                        Console.WriteLine($"Tűz tört ki a(z) {epulet.Cim} épületben. (Típus: {tuzTipus})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hiba a tűzindításnál a(z) {epulet.Cim} épületnél: {ex.Message}");
                    }
                }
            }

            foreach (var epulet in varos.Epuletek.ToList())
            {
                if (epulet.TuzVan)
                {
                    try
                    {
                        int felhasznaltViz = varos.Tuzoltas(epulet);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Sikeresen oltották a tüzet a(z) {epulet.Cim} épületben. Használt víz: {felhasznaltViz} liter.");
                        Console.ForegroundColor = ConsoleColor.White;
                        varos.EloltottakSzama++;
                        varos.OsszesVizFelhasznalva += felhasznaltViz;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Nem sikerült oltani a tüzet a(z) {epulet.Cim} épületben: {ex.Message}");
                        try
                        {
                            varos.EpuletElveszt(epulet);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"A(z) {epulet.Cim} épület leégett!");
                            Console.ForegroundColor = ConsoleColor.White;
                            varos.LeegtekSzama++;
                        }
                        catch (Exception tuzEx)
                        {
                            Console.WriteLine($"Hiba a leégett épület kezelésében: {tuzEx.Message}");
                        }
                    }
                }
            }

            varos.Visszaallit();

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }
    }
}
