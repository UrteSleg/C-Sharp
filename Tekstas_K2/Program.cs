using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace K___2_vienas
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fv = "..\\..\\Duomenys.txt";
            const string fr = "..\\..\\KoreguotasTekstas.txt";
            const string s = " ,.:-_?!()\"/\\+";
            AtliktiVeiksmus(fv, fr, s);
        }
        
        static bool TikSkaitmenys(string e)
        {
            int skKiek = 0;
            string skaitmenys = "0123456789";
            for(int i = 0; i < e.Length; i++)
            {
                if(skaitmenys.IndexOf(e[i])!= -1)
                {
                    skKiek++;
                }
            }
            if(skKiek == e.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int SkaitmenuSuma(string e)
        {
            int suma = 0;
            for(int i = 0; i < e.Length; i++)
            {
                string skaitmuo = "" + e[i];
                suma = suma + int.Parse(skaitmuo);
            }
            return suma;
        }
        
        static void RastiZodiEil(string e, string sk, out string z)
        {
            z = "";
            int kelintas = -1;
            int pr = 0;
            string[] zodziai = e.Split(sk.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < zodziai.Length; i++)
            {
                if(TikSkaitmenys(zodziai[i]) && zodziai[i].Length == 5)
                {
                    z = zodziai[i];
                    kelintas = i;
                    //pr = e.IndexOf(zodziai[i], pr);
                }
            }
            pr = e.LastIndexOf(z);
            if(pr > -1)
            {
                if (kelintas > -1 && kelintas < zodziai.Length - 1)
                {
                    string sekantis = zodziai[kelintas + 1];
                    z = e.Substring(pr, e.IndexOf(sekantis, pr) - pr);
                }
                else if (kelintas == zodziai.Length - 1)
                {
                    z = e.Substring(pr);
                }
            }
        }

        static void Rasti2ZodiEil(string e, string sk, out string z)
        {
            z = "";
            int kelintas = -1;
            int pr = -1;
            string[] zodziai = e.Split(sk.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < zodziai.Length; i++)
            {
                if (TikSkaitmenys(zodziai[i]) && zodziai[i].Length > z.Length)
                {
                    z = zodziai[i];
                    kelintas = i;
                }
            }
            pr = e.IndexOf(z);
            if(pr > -1)
            {
                if (kelintas > -1 && kelintas < zodziai.Length - 1)
                {
                    string sekantis = zodziai[kelintas + 1];
                    z = e.Substring(pr, e.IndexOf(sekantis, pr) - pr);
                }
                else if (kelintas == zodziai.Length - 1)
                {
                    z = e.Substring(pr);
                }
            }
            
        }

        static void KeistiEilute(ref string e, string sk, string z)
        {
            int pr = e.LastIndexOf(z);
            e = e.Remove(pr, z.Length);
        }

        static void AtliktiVeiksmus(string fd, string fr, string sk)
        {
            using (StreamWriter writer = new StreamWriter(fr))
            {
                using (StreamReader reader = new StreamReader(fd, Encoding.GetEncoding(1257)))
                {
                    
                    string e;
                    while((e = reader.ReadLine()) != null)
                    {
                        string zodis;
                        RastiZodiEil(e, sk, out zodis);
                        KeistiEilute(ref e, sk, zodis);
                        writer.WriteLine(e);
                    }
                }
                int suma = 0;
                using (StreamReader reader = new StreamReader(fd, Encoding.GetEncoding(1257)))
                {
                    string e;
                    string skaicius;
                    while ((e = reader.ReadLine()) != null)
                    {
                        Rasti2ZodiEil(e, sk, out skaicius);
                        if(skaicius != "")
                        {
                            skaicius = skaicius.Trim(sk.ToCharArray());
                            suma = suma + SkaitmenuSuma(skaicius);
                            writer.WriteLine(skaicius);
                        }
                    }
                }
                writer.WriteLine(suma);
            }
        }
    }
}
