using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_22
{
    class ElektrinisVirdulys : Prietaisas
    {
        public string Galia { get; set; }
        public string Turis { get; set; }

        public ElektrinisVirdulys()
        {

        }
        public ElektrinisVirdulys(string gamintojas, string modelis, string energijosKlase, string spalva, int kaina, string galia, string turis)
            : base(gamintojas,modelis,energijosKlase,spalva,kaina)

        {
            Galia = galia;
            Turis = turis;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}", Gamintojas, Modelis, EnergijosKlase, Spalva, Kaina, Galia, Turis);
        }
    }
}
