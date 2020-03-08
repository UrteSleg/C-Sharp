using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_savarankiska
{
    class Komanda
    {
        public const int MaxZaidejuSkaicius = 100;

        public string Pavadinimas { get; set; }
        public string Miestas { get; set; }
        public string Treneris { get; set; }
        public int RungtyniuSk { get; set; }
        public ZaidejuKonteineris Zaidejai { get; set; }

        public Komanda(string pavadinimas, string miestas, string treneris, int rungtyniuSk)
        {
            Pavadinimas = pavadinimas;
            Miestas = miestas;
            Treneris = treneris;
            RungtyniuSk = rungtyniuSk;
            Zaidejai = new ZaidejuKonteineris(MaxZaidejuSkaicius);
        }

        public double VidutinisTaskuSkaicius()
        {
            int sk = 0;
            for (int i = 0; i < Zaidejai.Skaicius; i++)
            {
                sk = sk + Zaidejai.ImtiZaideja(i).Taskai;
            }
            return sk / Zaidejai.Skaicius;
        }

        public double VidutinisRezultatyviu()
        {
            int sk = 0;
            for (int i = 0; i < Zaidejai.Skaicius; i++)
            {
                Krepsininkas k = Zaidejai.ImtiZaideja(i) as Krepsininkas;
                sk = sk + k.RezultatyvusPerdavimai;
            }
            return sk / Zaidejai.Skaicius;
        }

        public double VidutinisAtkovotu()
        {
            int sk = 0;
            for (int i = 0; i < Zaidejai.Skaicius; i++)
            {
                Krepsininkas k = Zaidejai.ImtiZaideja(i) as Krepsininkas;
                sk = sk + k.AtkovotiKamuoliai;
            }
            return sk / Zaidejai.Skaicius;
        }

        public double VidutinisGeltonu()
        {
            int sk = 0;
            for (int i = 0; i < Zaidejai.Skaicius; i++)
            {
                Futbolininkas k = Zaidejai.ImtiZaideja(i) as Futbolininkas;
                sk = sk + k.GeltonosKorteles;
            }
            return sk / Zaidejai.Skaicius;
        }

    }
}
