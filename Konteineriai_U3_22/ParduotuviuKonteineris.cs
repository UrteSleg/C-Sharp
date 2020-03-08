using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_22
{
    class ParduotuviuKonteineris
    {
        public int Kiekis { get; set; }
        Parduotuve[] Parduotuves;

        public ParduotuviuKonteineris()
        {
            Kiekis = 0;
            Parduotuves = new Parduotuve[100];
        }

        public void PridetiParduotuve(Parduotuve parduotuve)
        {
            Parduotuves[Kiekis++] = parduotuve;
        }

        public Parduotuve GrazintiParduotuve(int indeksas)
        {
            return Parduotuves[indeksas];
        }


    }
}
