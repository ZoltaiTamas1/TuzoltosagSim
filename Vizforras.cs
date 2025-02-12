using System;

namespace TuzoltosagSim
{
    public class Vizforras
    {
        public string Hely { get; private set; }
        public int VizMennyiseg { get; private set; }

        public Vizforras(string hely, int vizMennyiseg)
        {
            Hely = hely;
            VizMennyiseg = vizMennyiseg;
        }

        public void VizetHasznal(int mennyiseg)
        {
            if (VizMennyiseg < mennyiseg)
                throw new Exception($"Nincs elég víz a(z) {Hely} helyen.");

            VizMennyiseg -= mennyiseg;
        }
    }
}
