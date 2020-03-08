using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_22
{
    class Prietaisas
    {
        public string Gamintojas { get; set; }
        public string Modelis { get; set; }
        public string EnergijosKlase { get; set; }
        public string Spalva { get; set; }
        public int Kaina { get; set; }

        public Prietaisas()
        {
        }
        public Prietaisas(string gamintojas, string modelis, string energijosKlase, string spalva, int kaina)
        {
            Gamintojas = gamintojas;
            Modelis = modelis;
            EnergijosKlase = energijosKlase;
            Spalva = spalva;
            Kaina = kaina;
        }
       }
    }
