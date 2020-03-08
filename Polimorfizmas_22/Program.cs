using System;
using System.Collections.Generic;
using System.IO;

namespace _22
{
    class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            int[] talpos = p.Talpos(p.Skaitymas());
            for (int i = 0; i < talpos.Length; i++)
            {
                if (talpos[i] != 0)
                {
                    Console.Write(talpos[i].ToString() + " ");
                }
            }
            Console.WriteLine();
            p.PigiausiSaldytuvai();
            File.WriteAllLines(@"Baltieji.csv", p.Baltieji(p.Skaitymas()));
            File.WriteAllLines(@"Bosch.csv", p.Bosch(p.Skaitymas()));
            File.WriteAllLines(@"Visur.csv", p.Visur(p.Skaitymas()));
            Console.ReadKey();
        }

        /// <summary>
        /// nuskaitomas failas „duomenys.csv“
        /// </summary>
        /// <returns> nuskaityti duomenys </returns>
        ParduotuviuKonteineris[] Skaitymas()
        {
            int num = 1;
            ParduotuviuKonteineris[] parduotuviuKonteineris = new ParduotuviuKonteineris[GetFileCount()];
            while (num <= GetFileCount())
            {

                string filePath = "duomenys" + num + ".csv";
                string[] lines = File.ReadAllLines(filePath);
                int lineCount = 0;
                parduotuviuKonteineris[num - 1] = new ParduotuviuKonteineris(lines.Length - 3, lines[0], lines[1], lines[2]);
                foreach (string line in lines)
                {

                    if (lineCount > 2)
                    {
                        string[] values = line.Split(' ');
                        string Gamintojas = values[0];
                        string Modelis = values[1];
                        int Talpa = int.Parse(values[2]);
                        string EnergijosKlase = values[3];
                        string Montavimotipas = values[4];
                        string Spalva = values[5];
                        string TuriSaldikli = values[6];
                        int Kaina = int.Parse(values[7]);
                        Saldytuvas saldytuvas = new Saldytuvas(Gamintojas, Modelis, Talpa, EnergijosKlase, Montavimotipas, Spalva, TuriSaldikli, Kaina);
                        parduotuviuKonteineris[num - 1].AddSaldytuvas(saldytuvas);
                    }
                    lineCount++;
                }
                num++;
            }
            return parduotuviuKonteineris;
        }

        /// <summary>
        /// randama kokios skirtingos talpos saldytuvai yra
        /// </summary>
        /// <returns> skirtingu talpu sarasas </returns>
        int[] Talpos(ParduotuviuKonteineris[] parduotuves)
        {
            int[] talpos = new int[GetNumber()];
            int count = 0;
            for (int i = 0; i < parduotuves.Length; i++)
            {
                for (int j = 0; j < parduotuves[i].count; j++)
                {
                    Saldytuvas saldytuvas = parduotuves[i].GetSaldytuvas(j);
                    if (!Contains(saldytuvas.Talpa, talpos))
                    {
                        talpos[count] = saldytuvas.Talpa;
                        count++;
                    }
                }
            }
            return talpos;
        }
        /// <summary>
        /// randama kuriuos saldytuvus galima rasti visose parduotuvese
        /// </summary>
        /// <returns> grazinamas tekstas su saldytuvu duomenimis </returns>
        string[] Visur(ParduotuviuKonteineris[] parduotuves)
        {
            string[] lines = new string[GetNumber()];
            int count = 0;
            string name = "";
            int detected = 0;

            for (int i = 0; i < parduotuves[0].count; i++)
            {
                Saldytuvas saldytuvas = parduotuves[0].GetSaldytuvas(i);
                name = saldytuvas.Gamintojas + saldytuvas.Modelis;
                for (int a = 1; a < parduotuves.Length; a++)
                {
                    for (int b = 0; b < parduotuves[a].count; b++)
                    {
                        Saldytuvas saldytuvas1 = parduotuves[a].GetSaldytuvas(b);
                        //tikrinama ar saldytuvo gamintojai ir modeliai sutampa
                        if (saldytuvas1.Gamintojas + saldytuvas1.Modelis == name)
                        {
                            detected++;
                            break;
                        }
                    }
                }
                //ar saldytuvas rastas likusiose parduotuvese
                if (detected == GetFileCount()-1)
                {
                    lines[count] = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} ", saldytuvas.Gamintojas, saldytuvas.Modelis, saldytuvas.Talpa, saldytuvas.EnergijosKlase, saldytuvas.MontavimoTipas, saldytuvas.Spalva, saldytuvas.TuriSaldikli, saldytuvas.Kaina); ;
                    count++;
                }
                detected = 0;
            }
            return lines;
        }
        /// <summary>
        /// patikrinama ar talpos nesikartoja
        /// </summary>
        /// <returns> grazinama true/false reiksme </returns>
        public bool Contains(int value, int[] talpos)
        {
            for (int i = 0; i < talpos.Length; i++)
            {
                if (talpos[i] == value)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// randama pigiausio saldytuvo kaina
        /// </summary>
        /// <returns> pigiausio saldytuvo kaina (kelintas duomenu faile) </returns>
        int PigiausioSaldytuvoKaina()
        {
          ParduotuviuKonteineris[] parduotuviuKonteineris = Skaitymas();
            int maziausiaKaina = parduotuviuKonteineris[0].GetSaldytuvas(0).Kaina;

            for (int a = 0; a < parduotuviuKonteineris.Length; a++)
            {
                for (int i = 0; i < parduotuviuKonteineris[a].count; i++)
                {
                    Saldytuvas saldytuvai = parduotuviuKonteineris[a].GetSaldytuvas(i);

                    if (maziausiaKaina > saldytuvai.Kaina && saldytuvai.MontavimoTipas == "Pastatomas" && saldytuvai.TuriSaldikli == "TuriSaldikli")
                    {
                        maziausiaKaina = saldytuvai.Kaina;
                    }
                }
            }
            return maziausiaKaina;
        }
        /// <summary>
        /// Pagal maziausia kaina randamas pigiausias saldytuvas
        /// </summary>
        void PigiausiSaldytuvai()
        {
            int kaina = PigiausioSaldytuvoKaina();
            ParduotuviuKonteineris[] parduotuviuKonteineris = Skaitymas();
            List<string> panaudoti = new List<string>();
            for (int a = 0; a < parduotuviuKonteineris.Length; a++)
            {
                for (int i = 0; i < parduotuviuKonteineris[a].count; i++)
                {
                    Saldytuvas saldytuvai = parduotuviuKonteineris[a].GetSaldytuvas(i);
                    if (kaina == saldytuvai.Kaina && !panaudoti.Contains(saldytuvai.Gamintojas + saldytuvai.Modelis))
                    {
                        Console.WriteLine("Pigiausias pastatomas saldytuvas: {0} {1} {2} {3} galima isigyti: {4}", saldytuvai.Gamintojas, saldytuvai.Modelis, saldytuvai.Talpa, saldytuvai.Kaina, RastiVisasParduotuves(saldytuvai));
                        panaudoti.Add(saldytuvai.Gamintojas + saldytuvai.Modelis);
                    }
                }
            }
        }
        /// <summary>
        /// randama kuriose parduotuvese pigiausi saldytuvai kartojasi
        /// </summary>
        /// <returns> tekstas su visomis parduotuvemis, kuriose randasi pigiausi saldytuvai</returns>
        string RastiVisasParduotuves(Saldytuvas saldytuvas)
        {
            //kintamasis skirtas visoms parduotuvems suvesti
            string parduotuves = "";
            //nuskaitomi VISI duomenys
            ParduotuviuKonteineris[] parduotuviuKonteineris = Skaitymas();
            for (int a = 0; a < parduotuviuKonteineris.Length; a++)
            {
                for (int i = 0; i < parduotuviuKonteineris[a].count; i++)
                {
                    Saldytuvas saldytuvai = parduotuviuKonteineris[a].GetSaldytuvas(i);
                    //tikrinama ar saldytuvas atitinka kriterijus
                    if (saldytuvai.Gamintojas == saldytuvas.Gamintojas && saldytuvai.Modelis == saldytuvas.Modelis)
                    { 
                        if (parduotuves == "")
                        {
                            parduotuves += parduotuviuKonteineris[a].GetPavadinimas();
                        }
                        //jeigu jau yra reiksmiu, pridedamas kablelis
                        else
                        {
                            parduotuves += "," + " " + parduotuviuKonteineris[a].GetPavadinimas();
                        }
                    }
                }
            }
            return parduotuves;
        }
        /// <summary>
        /// randami baltos spalvos saldytuvai su A++ klase
        /// </summary>
        /// <returns> tekstas su baltaisiais saldytuvais </returns>
        string[] Baltieji(ParduotuviuKonteineris[] parduotuves)
        {
            int numeris = 0;
            int count = 0;
            string[] lines = new string[GetNumber() + (parduotuves.Length * 2)];
            for (int a = 0; a < parduotuves.Length; a++)
            {
                count = 0;
                for (int i = 0; i < parduotuves[a].count; i++)
                {
                    Saldytuvas saldytuvai = parduotuves[a].GetSaldytuvas(i);
                    if (saldytuvai.Spalva == "Balta" && saldytuvai.EnergijosKlase == "A++")
                    {
                        if (count == 0)
                        {
                            if (numeris != 0)
                            {
                                lines[numeris] = "";
                                numeris++;
                            }
                            lines[numeris] = string.Format("{0} {1} {2}", parduotuves[a].GetPavadinimas(), parduotuves[a].GetAdresas(), parduotuves[a].GetNumeris());
                            numeris++;

                        }
                        lines[numeris] = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} ", saldytuvai.Gamintojas, saldytuvai.Modelis, saldytuvai.Talpa, saldytuvai.EnergijosKlase, saldytuvai.MontavimoTipas, saldytuvai.Spalva, saldytuvai.TuriSaldikli, saldytuvai.Kaina);
                        numeris++;
                        count++;
                    }
                }
            }
            return lines;
        }
        /// <summary>
        /// randami bosch gamintoju saldytuvai
        /// </summary>
        /// <returns> bosch saldytuvu sarasas</returns>
        string[] Bosch(ParduotuviuKonteineris[] parduotuves)
        {
            string[] lines = new string[GetNumber()];
            int numeris = 0;
            string contains = "";
            for (int i = 0; i < parduotuves.Length; i++)
            {
                for (int a = 0; a < parduotuves[i].count; a++)
                {
                    Saldytuvas saldytuvai = parduotuves[i].GetSaldytuvas(a);
                    if (saldytuvai.Gamintojas == "BOSCH")
                    {
                        if (!contains.Contains(saldytuvai.Gamintojas + saldytuvai.Modelis))
                        {
                            lines[numeris] = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} ", saldytuvai.Gamintojas, saldytuvai.Modelis, saldytuvai.Talpa, saldytuvai.EnergijosKlase, saldytuvai.MontavimoTipas, saldytuvai.Spalva, saldytuvai.TuriSaldikli, saldytuvai.Kaina);
                            contains += saldytuvai.Gamintojas + saldytuvai.Modelis;
                            numeris++;
                        }
                    }
                }
            }
            return lines;
        }
        /// <summary>
        /// randama kiek yra duomenu failu
        /// </summary>
        /// <returns> grazinamas failu skaicius</returns>
        public int GetFileCount()
        {
            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "duomenys*.csv");
            return filePaths.Length;
        }
        /// <summary>
        /// randami kiek is viso, visose parduotuvese yra saldytuvu
        /// </summary>
        /// <returns> grazinamas saldytuvu skaicius</returns>
        public int GetNumber()
        {
            int num = 1;
            int totalNum = 0;
            while (num <= GetFileCount())
            {
                string filePath = "duomenys" + num + ".csv";
                string[] lines = File.ReadAllLines(filePath);
                //lines.Length - 3 nes pirmos 3 eilutes turi parduotuves informacija
                totalNum += lines.Length - 3;
                num++;
            }
            return totalNum;
        }
    }
}
