using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2_savarankiska
{
    class Program
    {
        
        public const int MaxKomanduSkaicius = 10;
        static void Main(string[] args)
        {
            Komanda[] komandos = new Komanda[MaxKomanduSkaicius];
            int komanduSk = 0;

            const string komanduFailas = "..//..//Komandos.txt"; 
            const string zaidejuFailas = "..//..//Zaidejai.txt";//Console.WriteLine(komanda.Zaidejai.ImtiZaideja(0).GetType());

            SkaitytiKomanduFaila(komanduFailas, ref komanduSk, komandos);
            SkaitytiZaidejuFaila(zaidejuFailas, komanduSk, komandos);
            ZaidejuKonteineris geri = AtrinktiGerus(komandos, komanduSk);
            for (int i = 0; i < geri.Skaicius; i++)
            {
                Console.WriteLine(geri.ImtiZaideja(i).ToString());
            }

            for (int i = 0; i < komanduSk; i++)
            {
                Console.WriteLine(komandos[i].Pavadinimas);
                Console.WriteLine(komandos[i].RungtyniuSk);
               Console.WriteLine(komandos[i].VidutinisTaskuSkaicius());
                if (komandos[i].Zaidejai.ImtiZaideja(0) is Krepsininkas)
               {
                    Console.WriteLine(komandos[i].VidutinisAtkovotu());
                    Console.WriteLine(komandos[i].VidutinisRezultatyviu());
                }
                else
               {
                    Console.WriteLine(komandos[i].VidutinisGeltonu());
                  }
            }


        }
        
        private static void SkaitytiKomanduFaila(string file, ref int komanduSk, Komanda[] komandos)
        {
            string[] lines = File.ReadAllLines(file, Encoding.GetEncoding(1257));
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                string pavadinimas = values[0];
                string miestas = values[1];
                string treneris = values[2];
                int rungSk = int.Parse(values[3]);
                komandos[komanduSk++] = new Komanda(pavadinimas, miestas, treneris, rungSk);
            }
        }
        private static void SkaitytiZaidejuFaila(string file, int komanduSk, Komanda[] komandos)
        {
            using (StreamReader reader = new StreamReader(file, Encoding.GetEncoding(1257)))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    char raide = Convert.ToChar(values[0]);
                    string kompav = values[1]; 
                    string pav = values[2];
                    string vard = values[3];
                    int rungsk = Convert.ToInt32(values[4]);
                    int taskai = Convert.ToInt32(values[5]);

                    Komanda komanda = ImtiKomandaPagalPavadinima(komandos, komanduSk, kompav);
                    
                    switch (raide)
                    {
                        case 'K':
                            int atkovoti = Convert.ToInt32(values[6]); 
                            int perdavimai = Convert.ToInt32(values[7]);
                            Krepsininkas k = new Krepsininkas(kompav, pav, vard, rungsk, taskai, atkovoti, perdavimai);
                            komanda.Zaidejai.PridetiZaideja(k); 
                            break;
                        case 'F':
                            int geltonos = Convert.ToInt32(values[6]);
                            Futbolininkas f = new Futbolininkas(kompav, pav, vard, rungsk, taskai, geltonos);
                            komanda.Zaidejai.PridetiZaideja(f);
                            break;
                    }
                }
            }
        }
        private static Komanda ImtiKomandaPagalPavadinima(Komanda[] komandos, int komanduSk, string pav)
        {
            for (int i = 0; i < komanduSk; i++)
            {
                if (komandos[i].Pavadinimas == pav)
                {
                    return komandos[i];
                }
            }
            return null;
        }

        private static ZaidejuKonteineris AtrinktiGerus(Komanda[] komandos, int komanduSk)
        {
            int MaxGeruZaidejuSkaicius = 100;
            ZaidejuKonteineris geri = new ZaidejuKonteineris(MaxGeruZaidejuSkaicius);
            for (int i = 0; i < komanduSk; i++)
            {
                for(int j = 0; j < komandos[i].Zaidejai.Skaicius; j++)
                {
                    if (ArAtitinkaReikalavimus(komandos[i].Zaidejai.ImtiZaideja(j), komandos, komanduSk))
                    {
                        geri.PridetiZaideja(komandos[i].Zaidejai.ImtiZaideja(j));
                    }
                }
            }
            return geri;
        }

        private static bool ArAtitinkaReikalavimus(Zaidejas zaidejas, Komanda[] komandos, int komanduSk)
        {
            if (zaidejas is Futbolininkas)
            {
                Futbolininkas zaid = zaidejas as Futbolininkas;
                Komanda joKom = ImtiKomandaPagalPavadinima(komandos, komanduSk, zaidejas.KomandosPavadinimas);

                if (zaid.RungtyniuSk  == joKom.RungtyniuSk && zaid.Taskai >= Math.Round(joKom.VidutinisTaskuSkaicius()) && zaid.GeltonosKorteles >= joKom.VidutinisGeltonu())
                {
                    return true;
                }
            }
            else if (zaidejas is Krepsininkas)
            {
                Krepsininkas zaid = zaidejas as Krepsininkas;
                Komanda joKom = ImtiKomandaPagalPavadinima(komandos, komanduSk, zaidejas.KomandosPavadinimas);

                if (zaid.RungtyniuSk == joKom.RungtyniuSk && zaid.Taskai >= Math.Round(joKom.VidutinisTaskuSkaicius()) &&  zaid.RezultatyvusPerdavimai >= joKom.VidutinisRezultatyviu())
                {
                    return true;
                }
            }
            return false;
        }
    }
}