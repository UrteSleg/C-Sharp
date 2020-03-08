using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboratorinis_darbas_1
{
    class Program
    {
        public const int MiestuSkaicius = 5;
        static void Main(string[] args)
        {
            Program P = new Program();
            Miestas[] Miestai = new Miestas[MiestuSkaicius];

            Miestai[0] = new Miestas("Vilnius");
            Miestai[1] = new Miestas("Kaunas");
            Miestai[2] = new Miestas("Klaipeda");
            Miestai[3] = new Miestas("Siauliai");
            Miestai[4] = new Miestas("Luoke");

            string[] failai = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");
            foreach(string failas in failai)
            {
                P.NuskaitytiFailus(failas, Miestai);
            }
            P.SpausdintiArNemokamasSuGidu(Miestai);
            P.SpausdintiMuziejuTipuSarasa(P.MuziejuTipuSarasoRusiavimas(P.MuziejuTipuSarasas(Miestai)));
            P.SudarytiSutampaCSV(P.VlnKlpMuziejai(Miestai));
            P.SpausdintiDailesCSV(P.DailesMuziejuSarasas(Miestai),Miestai);
        }

        Miestas ImtiMiesta(Miestas[] miestai, string pavadinimas)
        {
            for(int i = 0; i < MiestuSkaicius; i++)
            {
                if(miestai[i].Pavadinimas == pavadinimas)
                {
                    return miestai[i];
                }
            }
            return null;
        }
        void NuskaitytiFailus(string failas, Miestas[] miestai)
        {
            string[] lines = File.ReadAllLines(failas);
 
            string Pavadinimas = lines[0];
            string Atsakingas = lines[1];
            Miestas miestas = ImtiMiesta(miestai, Pavadinimas);
            miestas.Atsakingas = Atsakingas;

            foreach(string line in lines.Skip(2))
            {
                string[] parts = line.Split(';');
                   Muziejus m = new Muziejus(parts[0], parts[1], Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]),
                   Convert.ToInt32(parts[4]), Convert.ToInt32(parts[5]), Convert.ToInt32(parts[6]), Convert.ToInt32(parts[7]),
                   Convert.ToInt32(parts[8]), Convert.ToDouble(parts[9]), parts[10]);
                   miestas.Muziejai.Deti(m);
            }
        }

        bool ArNemokamasSuGidu(Miestas miestas)
        {
            bool TenkinaSalyga = false;
            for (int i = 0; i < miestas.Muziejai.kiek; i++)
            {
                if (miestas.Muziejai.Imti(i).Gidas == "TRUE" &&
                    miestas.Muziejai.Imti(i).Kaina == 0)
                {
                    TenkinaSalyga = true;
                    break;
                }
                else TenkinaSalyga = false;
            }
            return TenkinaSalyga;
        }

        void SpausdintiArNemokamasSuGidu(Miestas[] miestai)
        {
            foreach(Miestas miestas in miestai)
            {
                if(ArNemokamasSuGidu(miestas) == true)
                {
                    Console.WriteLine("{0} - {1}", miestas.Pavadinimas, "Taip;");
                }
                else Console.WriteLine("{0} - {1}", miestas.Pavadinimas, "Ne;");
            }
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }

        List<string> MuziejuTipuSarasas(Miestas[] miestai)
        {
            List<string> tipai = new List<string>();
            foreach(Miestas miestas in miestai)
            {
                for(int i = 0; i < miestas.Muziejai.kiek; i++)
                {
                    tipai.Add(miestas.Muziejai.Imti(i).Tipas);
                }
            }
            return tipai;
        }

        List<string> MuziejuTipuSarasoRusiavimas(List<string> tipai)
        {
            for(int i = 0; i < tipai.Count(); i++)
            {
                for(int j = i + 1; j < tipai.Count(); j++)
                {
                    if(tipai[j] == tipai[i])
                    {
                        tipai.RemoveAt(j);
                    }
                }
            }
            return tipai;
        }

        void SpausdintiMuziejuTipuSarasa(List<string> tipai)
        {
            foreach(string tipas in tipai)
            {
                Console.WriteLine(tipas);
            }
        }

        List<Muziejus> VlnKlpMuziejai(Miestas[] miestai)
        {
            List<Muziejus> Muziejai = new List<Muziejus>();
            string[] VlnMuzPav = new string[ImtiMiesta(miestai, "Vilnius").Muziejai.kiek];
            string[] KlpMuzPav = new string[ImtiMiesta(miestai, "Klaipeda").Muziejai.kiek];

            for(int i = 0; i < ImtiMiesta(miestai,"Vilnius").Muziejai.kiek; i++)
            {
                VlnMuzPav[i] = ImtiMiesta(miestai, "Vilnius").Muziejai.Imti(i).MuzPavadinimas;
                VlnMuzPav[i] = VlnMuzPav[i].Replace("Vilniaus ", string.Empty); 
                for (int j = 0; j < ImtiMiesta(miestai, "Klaipeda").Muziejai.kiek; j++)
                {
                    KlpMuzPav[j] = ImtiMiesta(miestai, "Klaipeda").Muziejai.Imti(j).MuzPavadinimas;
                    KlpMuzPav[j] = KlpMuzPav[j].Replace("Klaipedos ", string.Empty);
                    if (VlnMuzPav[i].CompareTo(KlpMuzPav[j]) == 0)
                    {
                        Muziejai.Add(ImtiMiesta(miestai, "Vilnius").Muziejai.Imti(i));
                        Muziejai.Add(ImtiMiesta(miestai, "Klaipeda").Muziejai.Imti(j));
                    }
                }
            }
            return Muziejai;
        }

        void SudarytiSutampaCSV(List<Muziejus> sarasas)
        {
            using(StreamWriter failas = new StreamWriter("..//..//Sutampa.csv"))
            {
                foreach(Muziejus muziejus in sarasas)
                {
                    failas.WriteLine(muziejus);
                }
            }
        }

        List<Muziejus> DailesMuziejuSarasas(Miestas[] miestai)
        {
            List<Muziejus> muziejai = new List<Muziejus>();
            foreach(Miestas miestas in miestai)
            {
                for(int i = 0; i < miestas.Muziejai.kiek; i++)
                {
                    if((miestas.Muziejai.Imti(i).Tipas) == "Dailes")
                    {
                        muziejai.Add(miestas.Muziejai.Imti(i));
                    }
                }
            }
            return muziejai;
        }

        string GautiMiestaIsDailesMuziejuSaraso(Muziejus muziejus, Miestas[] miestai)
        {
            string pavad = null;
            foreach(Miestas miestas in miestai)
            {
                for(int i = 0; i < miestas.Muziejai.kiek; i++)
                {
                    if(miestas.Muziejai.Imti(i).Equals(muziejus))
                    {
                        pavad = miestas.Pavadinimas;
                    }
                }
            }
            return pavad;
        }
        void SpausdintiDailesCSV(List<Muziejus> sarasas, Miestas[] miestai)
        {
            using(StreamWriter failas = new StreamWriter("..//..//Dailes.csv"))
            {
                foreach(Muziejus muziejus in sarasas)
                {
                    failas.WriteLine("{0}; {1}", GautiMiestaIsDailesMuziejuSaraso(muziejus, miestai), muziejus);
                }
            }
        }
    }

    class Miestas
    {
        public string Pavadinimas { get; private set; }
        public string Atsakingas { get; set; }
        public MuziejuKonteineris Muziejai { get; private set; }
        public const int KontDydis = 20;

        public Miestas(string pavadinimas)
        {
            Pavadinimas = pavadinimas;
            Muziejai = new MuziejuKonteineris(KontDydis);
        }
    }

    class MuziejuKonteineris
    {
        Muziejus[] M;
        public int kiek { get; private set; }

        public MuziejuKonteineris(int dydis)
        {
            kiek = 0;
            M = new Muziejus[dydis];
        }

        public Muziejus Imti(int kuris)
        {
            return M[kuris];
        }

        public void Deti(Muziejus m)
        {
            M[kiek++] = m;
        }

        public void Deti(Muziejus m, int kuris)
        {
            M[kuris] = m;
        }
    }
   
    class Muziejus
    {
        public string MuzPavadinimas { get; private set; }
        public string Tipas { get; private set; }
        public int Pirmadienis { get; private set; }
        public int Antradienis { get; private set; }
        public int Treciadienis { get; private set; }
        public int Ketvirtadienis { get; private set; }
        public int Penktadienis { get; private set; }
        public int Sestadienis { get; private set; }
        public int Sekmadienis { get; private set; }
        public double Kaina { get; private set; }
        public string Gidas { get; private set; }

        public Muziejus(string muzpavadinimas, string tipas, int pirmadienis, int antradienis, int treciadienis, int ketvirtadienis, int penktadienis, int sestadienis, int sekmadienis, double kaina, string gidas)
        {
            MuzPavadinimas = muzpavadinimas;
            Tipas = tipas;
            Pirmadienis = pirmadienis;
            Antradienis = antradienis;
            Treciadienis = treciadienis;
            Ketvirtadienis = ketvirtadienis;
            Penktadienis = penktadienis;
            Sestadienis = sestadienis;
            Sekmadienis = sekmadienis;
            Kaina = kaina;
            Gidas = gidas;
        }

        public override String ToString()
        {
            return String.Format("{0,30}; {1,15}; {2}; {3}; {4}; {5}; {6}; {7}; {8}; {9}; {10}",
                MuzPavadinimas, Tipas, Pirmadienis, Antradienis, Treciadienis, Ketvirtadienis, Penktadienis,
                Sestadienis, Sekmadienis, Kaina, Gidas); 
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Muziejus);
        }

        public bool Equals(Muziejus muz)
        {
            return (muz.MuzPavadinimas == MuzPavadinimas) && (muz.Tipas == Tipas) && (muz.Pirmadienis == Pirmadienis) &&
               (muz.Antradienis == Antradienis) && (muz.Treciadienis == Treciadienis) && (muz.Ketvirtadienis == Ketvirtadienis) &&
               (muz.Penktadienis == Penktadienis) && (muz.Sestadienis == Sestadienis) && (muz.Sekmadienis == Sekmadienis) &&
               (muz.Gidas == Gidas);
        }

        public override int GetHashCode()
        {
            return MuzPavadinimas.GetHashCode() ^ Tipas.GetHashCode() ^ Pirmadienis.GetHashCode() ^ Antradienis.GetHashCode() ^
                Treciadienis.GetHashCode() ^ Ketvirtadienis.GetHashCode() ^ Penktadienis.GetHashCode() ^ Sestadienis.GetHashCode() ^
                Sekmadienis.GetHashCode() ^ Kaina.GetHashCode() ^ Gidas.GetHashCode();
        }
    }

}
