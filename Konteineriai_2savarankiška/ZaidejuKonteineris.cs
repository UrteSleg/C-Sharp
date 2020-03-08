using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_savarankiska
{
    class ZaidejuKonteineris
    {
        public Zaidejas[] Zaidejai { get; set; }
        public int Skaicius { get; private set; }

        public ZaidejuKonteineris(int dydis)
        {
            Zaidejai = new Zaidejas[dydis];
        }

        public void PridetiZaideja(Zaidejas zaidejas)
        {
            Zaidejai[Skaicius++] = zaidejas;
        }

        public Zaidejas ImtiZaideja(int kuris)
        {
            return Zaidejai[kuris];
        }

        public void PasalintiZaideja(int kuris)
        {
            for(int i = kuris; i < Skaicius - 1; i++)
            {
                Zaidejai[i] = Zaidejai[i + 1];
            }
            Skaicius--;
        }

        
    }
}
