using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace L2_3_Leidiniai
{
    public class Skaiciavimai
    {
            public static LeidiniuSarasas NuskaitymasA(string failas, HttpServerUtility Server)
            {
                LeidiniuSarasas sarasas = new LeidiniuSarasas();
                string[] eilutes = File.ReadAllLines(Server.MapPath(failas));
                foreach (string eilute in eilutes)
                {
                    string[] dalys = eilute.Split(',');
                    int kodas = int.Parse(dalys[0]);
                    string pavadinimas = dalys[1];
                    double kaina = double.Parse(dalys[2]);
                    Leidinys leidinys = new Leidinys(kodas, pavadinimas, kaina);
                    sarasas.DetiDuomenisA(leidinys);
                }
                return sarasas;
            }
            public static PrenumeratoriuSarasas NuskaitymasB(string failas, HttpServerUtility Server)
            {
                PrenumeratoriuSarasas sarasas = new PrenumeratoriuSarasas();
                string[] eilutes = File.ReadAllLines(Server.MapPath(failas));
                foreach (string eilute in eilutes)
                {
                    string[] dalys = eilute.Split(',');
                    string pavarde = dalys[0];
                    string adresas = dalys[1];
                    int laikotarpioPradzia = int.Parse(dalys[2]);
                    int laikotarpioIlgis = int.Parse(dalys[3]);
                    int kodas = int.Parse(dalys[4]);
                    int kiekis = int.Parse(dalys[5]);
                    Prenumeratorius prenumeratorius = new Prenumeratorius(pavarde, adresas, laikotarpioPradzia, laikotarpioIlgis, kodas, kiekis);
                    sarasas.DetiDuomenisA(prenumeratorius);
                }
                return sarasas;
            }

            public static List<string> KiekvienoMenesioDidziausiosPajamos(PrenumeratoriuSarasas prenumeratoriai, LeidiniuSarasas leidiniai)
            {
                List<string> pavadinimai = new List<string>();

                for (int i = 1; i <= 12; i++)
                {
                    double suma = 0;
                    double maxSuma = 0;
                    string pav = null;
                    for (leidiniai.Pradzia(); leidiniai.Yra(); leidiniai.Kitas())
                    {
                        int kod = leidiniai.ImtiDuomenis().Kodas;
                        for (prenumeratoriai.Pradzia(); prenumeratoriai.Yra(); prenumeratoriai.Kitas())
                        {
                            var imti = prenumeratoriai.ImtiDuomenis();
                            if (i >= imti.LaikotarpioPradzia && kod == imti.Kodas && i <= imti.LaikotarpioPradzia + imti.LaikotarpioIlgis)
                            {
                                for (int j = 0; j < imti.Kiekis; j++)
                                {
                                    suma += leidiniai.ImtiDuomenis().Kaina;
                                }
                            }
                        }
                        if (suma > maxSuma)
                        {
                            maxSuma = suma;
                            pav = leidiniai.ImtiDuomenis().Pavadinimas;
                        }
                        suma = 0;
                    }
                    if (maxSuma == 0)
                    {
                        pav = "-";
                    }
                    pavadinimai.Add(pav);
                }
                return pavadinimai;
            }

            public static double BendrosiosLeidiniuPajamos(PrenumeratoriuSarasas prenumeratoriai, LeidiniuSarasas leidiniai)
            {
                double pajamos = 0;
                for (prenumeratoriai.Pradzia(); prenumeratoriai.Yra(); prenumeratoriai.Kitas())
                {
                    int kod = prenumeratoriai.ImtiDuomenis().Kodas;
                    for (leidiniai.Pradzia(); leidiniai.Yra(); leidiniai.Kitas())
                    {
                        if (kod == leidiniai.ImtiDuomenis().Kodas)
                        {
                            for (int i = 0; i < prenumeratoriai.ImtiDuomenis().Kiekis; i++)
                            {
                                pajamos += leidiniai.ImtiDuomenis().Kaina;
                            }

                            break;
                        }
                    }
                }
                return pajamos;
            }

            public static LeidiniuSarasas PajamosMazesnesUzVidutines(double bendrosPajamos, LeidiniuSarasas leidiniai, PrenumeratoriuSarasas prenumeratoriai)
            {
                double vidurkis = bendrosPajamos / ElementuKiekis(leidiniai);
                LeidiniuSarasas sarasas = new LeidiniuSarasas();

                for (leidiniai.Pradzia(); leidiniai.Yra(); leidiniai.Kitas())
                {
                    double suma = 0;
                    int kod = leidiniai.ImtiDuomenis().Kodas;
                    for (prenumeratoriai.Pradzia(); prenumeratoriai.Yra(); prenumeratoriai.Kitas())
                    {
                        var imti = prenumeratoriai.ImtiDuomenis();
                        if (kod == imti.Kodas)
                        {
                            suma += leidiniai.ImtiDuomenis().Kaina;
                        }
                    }
                    if (suma < vidurkis && suma != 0)
                    {
                        sarasas.DetiDuomenisA(leidiniai.ImtiDuomenis());
                    }

                }

                return sarasas;
            }

            public static int ElementuKiekis(LeidiniuSarasas leidiniai)
            {
                int k = 0;
                for (leidiniai.Pradzia(); leidiniai.Yra(); leidiniai.Kitas())
                {
                    k++;
                }
                return k - 2;
            }
        
            public static PrenumeratoriuSarasas Atrinkti(LeidiniuSarasas leidiniai, PrenumeratoriuSarasas prenumeratoriai, string pav, string men)
            {
                string pavadinimas = pav;
                int menesis = int.Parse(men);
                PrenumeratoriuSarasas atrinkti = new PrenumeratoriuSarasas();

                for (leidiniai.Pradzia(); leidiniai.Yra(); leidiniai.Kitas())
                {
                    if (leidiniai.ImtiDuomenis().Pavadinimas == pavadinimas)
                    {
                        for (prenumeratoriai.Pradzia(); prenumeratoriai.Yra(); prenumeratoriai.Kitas())
                        {
                            var imti = prenumeratoriai.ImtiDuomenis();
                            for (int i = imti.LaikotarpioPradzia; i < imti.LaikotarpioPradzia + imti.LaikotarpioIlgis; i++)
                            {
                                if (menesis == i)
                                {
                                    atrinkti.DetiDuomenisA(prenumeratoriai.ImtiDuomenis());
                                }
                            }
                        }
                    }
                }
                return atrinkti;
            }
            public static void SpausdintiFaile(string failas, LeidiniuSarasas leidiniai, HttpServerUtility Server)
            {
                using (StreamWriter rasyti = new StreamWriter(Server.MapPath(failas)))
                {
                    rasyti.WriteLine("Pradiniai duomenys:");
                    rasyti.WriteLine("Kodas|Pavadinimas|Kaina");
                    for (leidiniai.PradziaIsvedimui(); leidiniai.YraIsvedimui(); leidiniai.Kitas())
                    {
                        rasyti.WriteLine(leidiniai.ImtiDuomenis().ToString());
                    }
                    rasyti.WriteLine();
                    rasyti.WriteLine("Pavarde     |Adresas        |Laikotarpio Pradžia|Laikotarpio Ilgis|Kodas|Kaina");

                }
            }
        }
    }