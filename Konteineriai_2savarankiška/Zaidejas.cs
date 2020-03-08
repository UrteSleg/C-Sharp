using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_savarankiska
{
    class Zaidejas
    {
        public string KomandosPavadinimas { get; set; }
        public string Pavarde { get; set; }
        public string Vardas { get; set; }
        public int RungtyniuSk { get; set; }
        public int Taskai { get; set; }

        public Zaidejas(string komandosPavadinimas, string pavarde, string vardas, int rungtyniuSk, int taskai)
        {
            KomandosPavadinimas = komandosPavadinimas;
            Pavarde = pavarde;
            Vardas = vardas;
            RungtyniuSk = rungtyniuSk;
            Taskai = taskai;
        }

        public override string ToString()
        {
            return String.Format("| {0,12} | {1,12} | {2,12} | {3,5} | {4,5} |", KomandosPavadinimas, Pavarde, Vardas, RungtyniuSk, Taskai);
        }
    }
}
