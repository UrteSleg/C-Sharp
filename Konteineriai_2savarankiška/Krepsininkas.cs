using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_savarankiska
{
    class Krepsininkas : Zaidejas
    {
        public int AtkovotiKamuoliai { get; set; }
        public int RezultatyvusPerdavimai { get; set; }

        public Krepsininkas(string komandosPavadinimas, string pavarde, string vardas, int rungtyniuSk, int taskai, int atkovotiKamuoliai, int rezultatyvusPerdavimai)
            : base(komandosPavadinimas, pavarde, vardas, rungtyniuSk, taskai)
        {
            AtkovotiKamuoliai = atkovotiKamuoliai;
            RezultatyvusPerdavimai = rezultatyvusPerdavimai;
        }

        public override string ToString()
        {
            return base.ToString() + String.Format(" {0,5} | {1,5} |", AtkovotiKamuoliai, RezultatyvusPerdavimai);
        }
    }
}
