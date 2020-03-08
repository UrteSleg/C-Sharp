using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_22
{
    class MikrobanguKrosnele : Prietaisas
    {
        public string Galingumas { get; set; }
        public int ProgramuSkaicius { get; set; }

        public MikrobanguKrosnele()
        {
        }

        public MikrobanguKrosnele(string gamintojas, string modelis, string energijosKlase, string spalva, int kaina, string galingumas, int programuSkaicius)
            : base(gamintojas, modelis, energijosKlase, spalva, kaina)
        {
            Galingumas = galingumas;
            ProgramuSkaicius = programuSkaicius;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}", Gamintojas, Modelis, EnergijosKlase, Spalva, Kaina, Galingumas, ProgramuSkaicius );
        }
    }
}
