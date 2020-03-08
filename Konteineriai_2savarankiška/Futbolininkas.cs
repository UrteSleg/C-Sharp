using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_savarankiska
{
    class Futbolininkas : Zaidejas
    {
        public int GeltonosKorteles { get; set; }

        public Futbolininkas(string komandosPavadinimas, string pavarde, string vardas, int rungtyniuSk, int taskai, int geltonosKorteles)
            : base(komandosPavadinimas, pavarde, vardas, rungtyniuSk, taskai) // pakeiciau taskus i ivarciu sk // atkeiciau
        {
            GeltonosKorteles = geltonosKorteles;
        }

        public override string ToString()
        {
            return base.ToString()+String.Format(" {0,5} |", GeltonosKorteles);
        }
    }
}
