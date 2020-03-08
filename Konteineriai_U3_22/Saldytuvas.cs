using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_22
{
    class Saldytuvas : Prietaisas 
    {
        public int Talpa { get; set; }
        public string MontavimoTipas { get; set; }
        public string TuriSaldikli { get; set; }
        public int Aukstis { get; set; }
        public int Plotis { get; set; }
        public int Gylis { get; set; }

        public Saldytuvas()
        {
        }
        public Saldytuvas(string gamintojas, string modelis, string energijosKlase, string spalva, int kaina, int talpa, string montavimoTipas, string turiSaldikli, int aukstis, int plotis, int gylis)
            : base(gamintojas, modelis, energijosKlase, spalva, kaina)
        {
            Talpa = talpa;
            MontavimoTipas = montavimoTipas;
            TuriSaldikli = turiSaldikli;
            Aukstis = aukstis;
            Plotis = plotis;
            Gylis = gylis;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", Gamintojas, Modelis, EnergijosKlase, Spalva, Kaina, Talpa, MontavimoTipas, TuriSaldikli, Aukstis, Plotis, Gylis);
        }
    }
}