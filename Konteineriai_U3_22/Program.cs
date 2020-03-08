using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace U3_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            string[] failai = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");
            int parduotuviuKiekis = failai.Length;
            ParduotuviuKonteineris parduotuves = new ParduotuviuKonteineris();
            p.SkaitytiDuomenis(failai, parduotuves);
            string[] spalvos = p.Spalvos(parduotuves, parduotuviuKiekis);
            for (int i = 0; i < spalvos.Length; i++)
            {
                if(spalvos[i]!=" ")
                {
                    Console.WriteLine(spalvos[i].ToString() + " ");
                }
            }


        }

        void SkaitytiDuomenis(string[] failai, ParduotuviuKonteineris parduotuves )
        {
            foreach(var failas in failai)
            {
                using (StreamReader skaityti = new StreamReader(failas, Encoding.GetEncoding(1257)))
                {
                    string pavadinimas = skaityti.ReadLine();
                    string adresas = skaityti.ReadLine();
                    string telefonas = skaityti.ReadLine();
                    Parduotuve parduotuve = new Parduotuve(pavadinimas, adresas, telefonas);
                    string eilute;
                    while(null !=(eilute = skaityti.ReadLine()))
                    {
                        string[] dalys = eilute.Split(',');
                        char raide = Convert.ToChar(dalys[0]);
                        string gamintojas = dalys[1];
                        string modelis = dalys[2];
                        string energijosKlase = dalys[3];
                        string spalva = dalys[4];
                        int kaina = Convert.ToInt32(dalys[5]);
                        switch (raide)
                        {
                            case 'S':
                                int talpa = Convert.ToInt32(dalys[6]);
                                string montavimoTipas = dalys[7];
                                string turiSaldikli = dalys[8];
                                int aukstis = Convert.ToInt32(dalys[9]);
                                int plotis = Convert.ToInt32(dalys[10]);
                                int gylis = Convert.ToInt32(dalys[11]);
                                Saldytuvas saldytuvas = new Saldytuvas(gamintojas, modelis, energijosKlase, spalva, kaina, talpa, montavimoTipas, turiSaldikli, aukstis, plotis, gylis);
                                break;

                            case 'M':
                                string galingumas = dalys[6];
                                int programuSkaicius = Convert.ToInt32(dalys[7]);
                                MikrobanguKrosnele mikrobanguKrosnele = new MikrobanguKrosnele(gamintojas, modelis, energijosKlase, spalva, kaina, galingumas, programuSkaicius);
                                break;

                            case 'V':
                                string galia = dalys[6];
                                string turis = dalys[7];
                                ElektrinisVirdulys elektrinisVirdulys = new ElektrinisVirdulys(gamintojas, modelis, energijosKlase, spalva, kaina, galia, turis);
                                break;                   
                        }
                    }
                    parduotuves.PridetiParduotuve(parduotuve);
                }
            }
        }
        string[] Spalvos(ParduotuviuKonteineris parduotuves, int parduotuviuKiekis)
        {
            string[] spalvos = new string[GetNumber()];
            int count = 0;
            for (int i = 0; i < parduotuviuKiekis;i++)
            {
                for (int j = 0; j < parduotuves.GrazintiParduotuve(i).SaldytuvuKiekis; j++)
                {
                    Saldytuvas saldytuvas = parduotuves.GrazintiParduotuve(i).GrazintiSaldytuva(j);
                    if(!Contains(saldytuvas.Spalva, spalvos))
                    {
                        spalvos[count] = saldytuvas.Spalva;
                        count++;
                    }
                }
            }
            return spalvos;
        }
        public bool Contains(ParduotuviuKonteineris parduotuves, string[] spalvos)
        {
            for (int i = 0; i < spalvos.Length; i++)
            {
                if (spalvos[i] == parduotuves)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetFileCount()
        {
            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "Duomenys*.txt");
            return filePaths.Length;
        }
        public int GetNumber()
        {
            int num = 1;
            int totalNum = 0;
            while (num <= GetFileCount())
            {
                string filePath = "Duomenys" + num + "txt";
                string[] lines = File.ReadAllLines(filePath);
                //lines.Length - 3 nes pirmos 3 eilutes turi parduotuves informacija
                totalNum += lines.Length - 3;
                num++;
            }
            return totalNum;
        }
    }
}
