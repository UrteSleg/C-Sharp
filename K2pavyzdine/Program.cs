using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace K2pavyzdine
{
        class Vanduo : IComparable<Vanduo>
        {
            public string Pavadinimas { get; set; }
            public string Valstybe { get; set; }
            public double Gylis { get; set; }

            public Vanduo(string eil)
            {
                string[] dalys = eil.Split(';');
                Pavadinimas = dalys[0];
                Valstybe = dalys[1];
                Gylis = double.Parse(dalys[2]);
            }

            public Vanduo() {}

            public int CompareTo(Vanduo kita)
            {
                if (Gylis == kita.Gylis)
                    return kita.Pavadinimas.CompareTo(Pavadinimas);
                if (Gylis < kita.Gylis)
                    return -1;
                return 1;
            }

            public static bool operator >(Vanduo viena, Vanduo kita)
            {
                if (viena.Gylis > kita.Gylis) return true;
                return false;
            }

            public static bool operator <(Vanduo viena, Vanduo kita)
            {
                if (viena.Gylis < kita.Gylis) return true;
                return false;
            }

            public override string ToString()
            {
                return string.Format("| {0,15} | {1,15} | {2,15}|",Pavadinimas, Valstybe, Gylis);
            }
        }

        class Mazgas<Tipas>
            where Tipas : IComparable<Tipas>
        {
            public Tipas Duom { get; set; }
            public Mazgas<Tipas> Kitas { get; set; }

            public Mazgas(Tipas duom, Mazgas<Tipas> kitas)
            {
                Duom = duom;
                Kitas = kitas;
            }
        }
        class Telkiniai<Tipas>
            where Tipas : IComparable<Tipas>
        {
            private Mazgas<Tipas> Pr { get; set; }
            private Mazgas<Tipas> Pb { get; set; }
            private Mazgas<Tipas> d { get; set; }

            public Telkiniai()
            {
                Pr = null;
                Pb = null;
                d = null;
            }

            public void Pradzia()
            {
                d = Pr;
            }

            public void Kitas()
            {
                d = d.Kitas;
            }

            public Tipas ImtiDuom()
            {
                return d.Duom;
            }

            public bool Yra()
            {
                return d != null;
            }

            public void DetiT(Tipas obj)
            {
                var dd = new Mazgas<Tipas>(obj, null);
                if(Pr!=null)
                {
                    Pb.Kitas = dd;
                    Pb = dd;
                }
                else
                {
                    Pr = dd;
                    Pb = dd;
                }
            }

            public void RikiuotiBurbulu()
            {
                if(Pr==null) { return; }
                bool keista = true;
                while(keista)
                {
                    keista = false;
                    var pra = Pr;
                    while(pra.Kitas !=null)
                    {
                        if(pra.Duom.CompareTo(pra.Kitas.Duom)== -1)
                        {
                            keista = true;
                            Tipas obj = pra.Duom;
                            pra.Duom = pra.Kitas.Duom;
                            pra.Kitas.Duom = obj;
                        }
                        pra = pra.Kitas;
                    }
                }
            }
        }
    class Program
    {
        const string FailasDuomenu = "Duomenys.txt";
        const string FailasRez = "Rezultatai.txt";
        static void Main(string[] args)
        {
            Telkiniai<Vanduo> A = new Telkiniai<Vanduo>();
            Skaitymas(FailasDuomenu, A);
            Telkiniai<Vanduo> B = Atrinkti(A);
            if (File.Exists(FailasRez))
                File.Delete(FailasRez);

            IFailaLentele(FailasRez, A);
            IFailaLentele(FailasRez, B);
            B.RikiuotiBurbulu();
            IFailaLentele(FailasRez, B);
        }

        public static void Skaitymas(string fd, Telkiniai<Vanduo> A)
        {
            using (StreamReader reader = new StreamReader(fd, Encoding.GetEncoding(1257)))
                {
                string eil;
                while((eil = reader.ReadLine()) != null)
                {
                    A.DetiT(new Vanduo(eil));
                }
            }
        }

        public static Vanduo Giliausia(Telkiniai<Vanduo> A)
        {
            Vanduo giliausia = new Vanduo(";;0");
            for(A.Pradzia(); A.Yra(); A.Kitas())
            {
                if(A.ImtiDuom() > giliausia)
                {
                    giliausia = A.ImtiDuom();
                }
            }
            return giliausia;
        }

        public static Telkiniai<Vanduo> Atrinkti(Telkiniai<Vanduo> A)
        {
            Telkiniai<Vanduo> B = new Telkiniai<Vanduo>();
            Vanduo giliausia = Giliausia(A);
            for(A.Pradzia(); A.Yra(); A.Kitas())
            {
                if((giliausia.Gylis - A.ImtiDuom().Gylis)/ giliausia.Gylis <= 0.1)
                {
                    B.DetiT(A.ImtiDuom() as Vanduo);
                }
            }
            return B;
        }

        public static void IFailaLentele(string fr, Telkiniai<Vanduo> A)
        {
            using (StreamWriter writer = File.AppendText(fr))
            {
                string bruksnys = new string('-', 55);
                writer.WriteLine(bruksnys);
                writer.WriteLine("| {0,15} | {1,15} | {2,15} |", "Pavadinimas", "Valstybe", "Gylis");
                writer.WriteLine(bruksnys);
                for(A.Pradzia(); A.Yra(); A.Kitas())
                {
                    writer.WriteLine(A.ImtiDuom().ToString());
                }
                writer.WriteLine(bruksnys);
                writer.WriteLine();
            }
        }
    }
}
