using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dublisNR2
{
    // Klasės savybės perrašytos, kad prasidėtų iš didžiųjų raidžių ir būtų iš pilnų žodžių, kad būtų lengviau skaitomos
    class Klausimas
    {
        public string Tema { get; set; }
        public int Lygis { get; set; }
        public string Vardas { get; set; }
        public string StudentoKlausimas { get; set; }
        public string PirmasAtsakymas { get; set; }
        public string AntrasAtsakymas { get; set; }
        public string TreciasAtsakymas { get; set; }
        public string KetvirtasAtsakymas { get; set; }
        public string TeisingasAtsakymas { get; set; }
        public int Taskai { get; set; }

        public Klausimas(string tema, int lygis, string vardas, string studentoKlausimas, string pirmasAtsakymas, string antrasAtsakymas, string treciasAtsakymas, string ketvirtasAtsakymas, string teisingasAtsakymas, int taskai)
        {
            Tema = tema;
            Lygis = lygis;
            Vardas = vardas;
            StudentoKlausimas = studentoKlausimas;
            PirmasAtsakymas = pirmasAtsakymas;
            AntrasAtsakymas = antrasAtsakymas;
            TreciasAtsakymas = treciasAtsakymas;
            KetvirtasAtsakymas = ketvirtasAtsakymas;
            TeisingasAtsakymas = teisingasAtsakymas;
            Taskai = taskai;
        }

        public override String ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", Tema, Lygis, Vardas, StudentoKlausimas, PirmasAtsakymas, AntrasAtsakymas, TreciasAtsakymas, KetvirtasAtsakymas, TeisingasAtsakymas, Taskai);
        }
    }

    // Pridėta indvidualių studentų klasė
    class IndvidualusStudentai
    {
        public string StudentoVardas { get; set; }
        public int KlausimuKiekis { get; set; }

        public IndvidualusStudentai(string studentoVardas, int klausimuKiekis)
        {
            StudentoVardas = studentoVardas;
            KlausimuKiekis = klausimuKiekis;
        }
    }

    // Pridėta klausimų konteinerinė klasė
    class KlausimuKonteineris
    {
        public string StudentuAtstovybe { get; set; }
        public int KlausimuKiekis { get; set; }
        private int MasyvoDydis = 100;
        private Klausimas[] Klausimai;
        private IndvidualusStudentai[] IndvidualusStudentai;
        public int IndvidualiuStudentuKiekis { get; set; }
        public int DidziausiasKlausimuSkaicius { get; set; }

        public KlausimuKonteineris(string studentuAtstovybe)
        {
            KlausimuKiekis = 0;
            Klausimai = new Klausimas[MasyvoDydis];
            StudentuAtstovybe = studentuAtstovybe;
            IndvidualusStudentai = new IndvidualusStudentai[MasyvoDydis];
            IndvidualiuStudentuKiekis = 0;
            DidziausiasKlausimuSkaicius = 0;
        }

        public void PridetiKlausima(Klausimas klausimas)
        {
            Klausimai[KlausimuKiekis++] = klausimas;
        }

        public Klausimas GrazintiKlausima(int indeksas)
        {
            return Klausimai[indeksas];
        }

        public void PridetiIndvidualuStudenta(IndvidualusStudentai indvidualusStudentas)
        {
            IndvidualusStudentai[IndvidualiuStudentuKiekis++] = indvidualusStudentas;
        }

        public IndvidualusStudentai GrazintiIndvidualuStudenta(int indeksas)
        {
            return IndvidualusStudentai[indeksas];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Program programa = new Program();                                                           // Sukuriamas pagrindinis programos objektas
            string[] failuKeliai = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");        // Randami visi tekstiniai failai esantys Debug faile
            int atstovybiuKiekis = failuKeliai.Length;                                                  // Tekstinių failų skaičius

            KlausimuKonteineris[] klausimuKonteineris = new KlausimuKonteineris[atstovybiuKiekis];
            programa.DuomenuSkaitymas(failuKeliai, klausimuKonteineris);

            programa.SpausdintiPradiniusDuomenis(klausimuKonteineris, atstovybiuKiekis);

            int[] sudetingumoLygiai = new int[3];
            programa.SudetingumoLygioRadimas(sudetingumoLygiai, klausimuKonteineris, atstovybiuKiekis);
            programa.SudetingumoLygiuSpausdinimas(sudetingumoLygiai);

            programa.RastiVisusIndvidualiusStudentus(klausimuKonteineris, atstovybiuKiekis);

            programa.RastiDidziausiusKlausimuKiekius(klausimuKonteineris, atstovybiuKiekis);

            programa.SpausdintiAktyviausiusStudentus(klausimuKonteineris, atstovybiuKiekis);

            List<string> visiKlausimai = new List<string>();
            programa.RastiVisusKlausimus(klausimuKonteineris, atstovybiuKiekis, visiKlausimai);
            List<string> pasikartojantysKlausimai = new List<string>();
            programa.RastiVisusPasikartojanciusKlausimus(visiKlausimai, pasikartojantysKlausimai);
            programa.DuomenuRasymasVienodiKlausimai(pasikartojantysKlausimai);

            List<string> visosTemos = new List<string>();
            programa.VisuTemuPaieska(klausimuKonteineris, atstovybiuKiekis, visosTemos);
            programa.DuomenuRasymasVisosTemos(visosTemos);

        }

        /// <summary>
        /// Pakeistas duomenų skaitymo metodo pavadinimas ir nuskaitymo kodas, dėl pasikeitusių pradinių duomenų
        /// </summary>
        /// <param name="failuKeliai">Nurodo kiekvieno tekstinio failo kelią</param>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        void DuomenuSkaitymas(string[] failuKeliai, KlausimuKonteineris[] klausimuKonteineris)
        {
            int indeksas = 0;

            foreach (var kelias in failuKeliai)
            {
                using (StreamReader skaityti = new StreamReader(@kelias, Encoding.GetEncoding(1257)))
                {
                    string eilute = skaityti.ReadLine();
                    KlausimuKonteineris naujasKlausimuKonteineris = new KlausimuKonteineris(eilute);

                    while (null != (eilute = skaityti.ReadLine()))
                    {
                        string[] dalys = eilute.Split(',');
                        Klausimas klausimas = new Klausimas(dalys[0], Convert.ToInt32(dalys[1]), dalys[2], dalys[3], dalys[4], dalys[5], dalys[6], dalys[7], dalys[8], Convert.ToInt32(dalys[9]));
                        naujasKlausimuKonteineris.PridetiKlausima(klausimas);
                        klausimuKonteineris[indeksas] = naujasKlausimuKonteineris;
                    }
                }

                indeksas++;
            }
        }

        /// <summary>
        /// Pakeistas klausimų sudėtingumo paieškos metodas dėl pakeistų užduoties reikalavimų
        /// </summary>
        /// <param name="sudetingumoLygiai">Klausimų sudėtingumo lygio masyvas</param>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        /// <param name="atstovybiuKiekis">Studentų atstovybių skaičius</param>
        void SudetingumoLygioRadimas(int[] sudetingumoLygiai, KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int j = 0; j < klausimuKonteineris[i].KlausimuKiekis; j++)
                {
                    if (klausimuKonteineris[i].GrazintiKlausima(j).Lygis == 1)
                    {
                        sudetingumoLygiai[0]++;
                    }
                    else if (klausimuKonteineris[i].GrazintiKlausima(j).Lygis == 2)
                    {
                        sudetingumoLygiai[1]++;
                    }
                    else if (klausimuKonteineris[i].GrazintiKlausima(j).Lygis == 3)
                    {
                        sudetingumoLygiai[2]++;
                    }
                }
            }
        }


        /// <summary>
        /// Pridėtas klausimų sudėtingumo lygių spausdinimo metodas
        /// </summary>
        /// <param name="sudetingumoLygiai">Klausimų sudėtingumo lygio masyvas</param>
        void SudetingumoLygiuSpausdinimas(int[] sudetingumoLygiai)
        {
            Console.WriteLine("I lygio klausimai: {0}", sudetingumoLygiai[0]);
            Console.WriteLine("II lygio klausimai: {0}", sudetingumoLygiai[1]);
            Console.WriteLine("III lygio klausimai: {0}", sudetingumoLygiai[2]);
        }

        /// <summary>
        /// Pridėtas metodas, kuris randa visus skirtingus studentus
        /// </summary>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        /// <param name="atstovybiuKiekis">Studentų atstovybių skaičius</param>
        void RastiVisusIndvidualiusStudentus(KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int j = 0; j < klausimuKonteineris[i].KlausimuKiekis; j++)
                {
                    bool yraToksStudentas = false;

                    for (int k = 0; k < klausimuKonteineris[i].IndvidualiuStudentuKiekis; k++)
                    {
                        if (klausimuKonteineris[i].GrazintiKlausima(j).Vardas == klausimuKonteineris[i].GrazintiIndvidualuStudenta(k).StudentoVardas)
                        {
                            klausimuKonteineris[i].GrazintiIndvidualuStudenta(k).KlausimuKiekis++;
                            yraToksStudentas = true;
                        }
                    }

                    if (yraToksStudentas == false)
                    {
                        IndvidualusStudentai indvidualusStudentas = new IndvidualusStudentai(klausimuKonteineris[i].GrazintiKlausima(j).Vardas, 1);
                        klausimuKonteineris[i].PridetiIndvidualuStudenta(indvidualusStudentas);
                    }
                }
            }
        }


        /// <summary>
        /// Pridėtas metodas, kuris randa daugiausiai paklaususio studento klausimų skaičių kiekvienoje SA
        /// </summary>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        /// <param name="atstovybiuKiekis">Studentų atstovybių skaičius</param>
        void RastiDidziausiusKlausimuKiekius(KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int j = 0; j < klausimuKonteineris[i].IndvidualiuStudentuKiekis; j++)
                {
                    if (klausimuKonteineris[i].GrazintiIndvidualuStudenta(j).KlausimuKiekis > klausimuKonteineris[i].DidziausiasKlausimuSkaicius)
                    {
                        klausimuKonteineris[i].DidziausiasKlausimuSkaicius = klausimuKonteineris[i].GrazintiIndvidualuStudenta(j).KlausimuKiekis;
                    }
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris spausdina studentus, kurie uždavė daugiausiai klausimų savo SA
        /// </summary>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        /// <param name="atstovybiuKiekis">Studentų atstovybių skaičius</param>
        void SpausdintiAktyviausiusStudentus(KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                Console.WriteLine("{0} daugiausiai klausimų sugalvojusių studentų vardai ir jų klausimų kiekiai:", klausimuKonteineris[i].StudentuAtstovybe);
                for (int j = 0; j < klausimuKonteineris[i].IndvidualiuStudentuKiekis; j++)
                {
                    if (klausimuKonteineris[i].GrazintiIndvidualuStudenta(j).KlausimuKiekis == klausimuKonteineris[i].DidziausiasKlausimuSkaicius)
                    {
                        Console.WriteLine("{0} {1}", klausimuKonteineris[i].GrazintiIndvidualuStudenta(j).StudentoVardas, klausimuKonteineris[i].GrazintiIndvidualuStudenta(j).KlausimuKiekis);
                    }
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris randa visus klausimus
        /// </summary>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        /// <param name="atstovybiuKiekis">Studentų atstovybių skaičius</param>
        /// <param name="visiKlausimai">Visi klausimai</param>
        void RastiVisusKlausimus(KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis, List<string> visiKlausimai)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int j = 0; j < klausimuKonteineris[i].KlausimuKiekis; j++)
                {
                    visiKlausimai.Add(klausimuKonteineris[i].GrazintiKlausima(j).StudentoKlausimas);
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris randa visus pasikartojančius klausimus
        /// </summary>
        /// <param name="visiKlausimai">Visi klausimai</param>
        /// <param name="pasikartojantysKlausimai">Visi pasikartojantys klausimai</param>
        void RastiVisusPasikartojanciusKlausimus(List<string> visiKlausimai, List<string> pasikartojantysKlausimai)
        {
            for (int i = 0; i < visiKlausimai.Count - 1; i++)
            {
                bool kartojasiKlausimas = false;

                for (int j = i + 1; j < visiKlausimai.Count; j++)
                {
                    if (visiKlausimai[i] == visiKlausimai[j])
                    {
                        kartojasiKlausimas = true;
                        break;
                    }
                }

                if (kartojasiKlausimas == true && pasikartojantysKlausimai.Contains(visiKlausimai[i]) == false)
                {
                    pasikartojantysKlausimai.Add(visiKlausimai[i]);
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris spausdina pasikartojančius klausimus į VienodiKlausimai.csv failą
        /// </summary>
        /// <param name="pasikartojantysKlausimai">Visi pasikartojantys klausimai</param>
        void DuomenuRasymasVienodiKlausimai(List<string> pasikartojantysKlausimai)
        {
            using (StreamWriter rasyti = new StreamWriter("VienodiKlausimai.csv", false, Encoding.GetEncoding(1257)))
            {
                for (int i = 0; i < pasikartojantysKlausimai.Count; i++)
                {
                    rasyti.WriteLine(pasikartojantysKlausimai[i]);
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris randa visas skirtingas temas
        /// </summary>
        /// <param name="klausimuKonteineris">Klausimų konteineris</param>
        /// <param name="atstovybiuKiekis">Studentų atstovybių skaičius</param>
        /// <param name="visosTemos">Visos temos</param>
        void VisuTemuPaieska(KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis, List<string> visosTemos)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int j = 0; j < klausimuKonteineris[i].KlausimuKiekis; j++)
                {
                    if (visosTemos.Contains(klausimuKonteineris[i].GrazintiKlausima(j).Tema) == false)
                    {
                        visosTemos.Add(klausimuKonteineris[i].GrazintiKlausima(j).Tema);
                    }
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris spausdina visas skirtingas temas į Temos.csv failą
        /// </summary>
        /// <param name="visosTemos">Visos temos</param>
        void DuomenuRasymasVisosTemos(List<string> visosTemos)
        {
            using (StreamWriter rasyti = new StreamWriter("Temos.csv", false, Encoding.GetEncoding(1257)))
            {
                for (int i = 0; i < visosTemos.Count; i++)
                {
                    rasyti.WriteLine(visosTemos[i]);
                }
            }
        }

        /// <summary>
        /// Pridėtas metodas, kuris spausdina pradinius duomenis į PradiniaiDuomenys.csv failą
        /// </summary>
        /// <param name="klausimuKonteineris"></param>
        /// <param name="atstovybiuKiekis"></param>
        void SpausdintiPradiniusDuomenis(KlausimuKonteineris[] klausimuKonteineris, int atstovybiuKiekis)
        {
            using (StreamWriter rasyti = new StreamWriter("PradiniaiDuomenys.csv", false, Encoding.GetEncoding(1257)))
            {
                for (int i = 0; i < atstovybiuKiekis; i++)
                {
                    rasyti.WriteLine(klausimuKonteineris[i].StudentuAtstovybe);

                    for (int j = 0; j < klausimuKonteineris[i].KlausimuKiekis; j++)
                    {
                        rasyti.WriteLine(klausimuKonteineris[i].GrazintiKlausima(j));
                    }
                }
            }
        }
    }
}
