using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Text;

namespace KP1
{
    class Knyga
    {
        public string Platintojas { get; private set; }
        public string Pavadinimas { get; private set; }
        public int Kiekis { get; private set; }
        public double Kaina { get; private set; }

        public Knyga( string platintojas,string pavadinimas, int kiekis, double kaina)
        {
            Platintojas = platintojas;
            Pavadinimas = pavadinimas;
            Kiekis = kiekis;
            Kaina = kaina;
        }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,-20} {2,10} {3,10}", Platintojas, Pavadinimas, Kiekis, Kaina);
        }

        public static bool operator >=(Knyga pirmaknyga, Knyga antraknyga)
        {
            if(pirmaknyga.Pavadinimas.CompareTo(antraknyga.Pavadinimas)==0)
            {
                return pirmaknyga.Kaina >= antraknyga.Kaina;
            }
            return false;
        }
        public static bool operator <=(Knyga pirmaknyga, Knyga antraknyga)
        {
            if(pirmaknyga.Pavadinimas.CompareTo(antraknyga.Pavadinimas)==0)
            {
                return pirmaknyga.Kaina <= antraknyga.Kaina;
            }
            return false;
        }

    }

    class Knygynas
    {
        private Knyga[] Knygos;
        public int Kiekis { get; private set; }

        public Knygynas()
        {
            Knygos = new Knyga[100];
            Kiekis = 0;
        }

        public void PridetiKnyga(Knyga knyga)
        {
            Knygos[Kiekis++] = knyga;
        }

        public Knyga GrazintiKnyga(int indeksas)
        {
            return Knygos[indeksas];
        }

        public double PiniguSuma()
        {
            double pinigusuma = 0;
            for (int i = 0; i < Kiekis; i++)
            {
                pinigusuma += Knygos[i].Kiekis * Knygos[i].Kaina;
            }
            return pinigusuma;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string rezultatuFailas = @"Rezultatas.txt";
            Program p = new Program();
            Knygynas knygos = new Knygynas();
            Knygynas parduotos = new Knygynas();
            p.SkaitytiKnyga(knygos);
            p.SkaitytiParduota(parduotos);

            if(File.Exists(rezultatuFailas))
            {
                File.Delete(rezultatuFailas);
            }

            p.SpausdintiFaila(knygos, rezultatuFailas, "Knygynas");
            p.SpausdintiFaila(parduotos, rezultatuFailas, "Pardavimas");
            p.PridetiPardavimoKaina(parduotos, knygos);
            p.SpausdintiFaila(parduotos, rezultatuFailas, "Parduota");
            double likusiSuma = knygos.PiniguSuma() - parduotos.PiniguSuma();
            p.SpausdintiAtsiskaitymoSuma(likusiSuma, rezultatuFailas);

        }

        void SkaitytiKnyga(Knygynas knygos)
        {
            string[] eilutes = File.ReadAllLines(@"..\..\Knyga.txt", Encoding.GetEncoding(1257));

            foreach(var eilute in eilutes)
            {
                string[] dalys = eilute.Split(',');
                Knyga knyga = new Knyga(dalys[0], dalys[1], Convert.ToInt32(dalys[2]), Convert.ToDouble(dalys[3]));
                knygos.PridetiKnyga(knyga);
            }

        }

        void SkaitytiParduota(Knygynas parduota)
        {
            string[] eilutes = File.ReadAllLines(@"..\..\Parduota.txt", Encoding.GetEncoding(1257));

            foreach(var eilute in eilutes)
            {
                Knyga knyga = new Knyga(" ", eilute, 1, 0);
                parduota.PridetiKnyga(knyga);
            }
        }

        void SpausdintiFaila(Knygynas knygos, string failoVardas, string lentelesAntraste)
        {
            using (StreamWriter rasyti = new StreamWriter(failoVardas, true, Encoding.GetEncoding(1257)))
            {
                rasyti.WriteLine(lentelesAntraste);
                rasyti.WriteLine(new string('-', 85));
                rasyti.WriteLine("{0,-12} {1,-20} {2,23} {3,18}", "Platintojas", "Pavadinimas", "Kiekis", "Egz. kaina");
                rasyti.WriteLine(new string('-', 85));
                for (int i = 0; i < knygos.Kiekis;i++)
                {
                    rasyti.WriteLine(knygos.GrazintiKnyga(i));
                    rasyti.WriteLine(new string('-', 85));
                }
            }

        }
        double DidziausiaKaina( Knyga knyga, Knygynas knygos)
        {
            for (int i = 0; i < knygos.Kiekis;i++)
            {
                if(knygos.GrazintiKnyga(i)>=knyga)
                {
                    knyga.Kaina = knygos.GrazintiKnyga(i).Kaina;
                }
            }
        }
        void PridetiPardavimoKaina(Knygynas parduotos, Knygynas knygos)
        {
            for (int i = 0; i < parduotos.Kiekis; i++)
            {
                double kaina = DidziausiaKaina(parduotos.GrazintiKnyga(i), knygos);
                parduotos.GrazintiKnyga(i).Kaina = kaina;
            }
        }

        void SpausdintiAtsiskaitymoSuma(double suma, string failoVardas)
        {
            using (StreamWriter rasyti = new StreamWriter(failoVardas, true, Encoding.GetEncoding(1257)))

            {
                rasyti.WriteLine("Turi dar surinkti {0} pinigu", suma);
            }
        }
    }


}
