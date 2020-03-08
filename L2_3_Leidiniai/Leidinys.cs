using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2_3_Leidiniai
{
    public class Leidinys : IComparable<Leidinys>, IEquatable<Leidinys>,
        IComparer<Leidinys>
    {
        public int Kodas { get; set; }
        public string Pavadinimas { get; set; }
        public double Kaina { get; set; }

        public Leidinys()
        {

        }

        public Leidinys(int kodas, string pavadinimas, double kaina)
        {
            Kodas = kodas;
            Pavadinimas = pavadinimas;
            Kaina = kaina;
        }

        static public bool operator >(Leidinys pirmas, Leidinys antras)
        {
            return pirmas.Kaina > antras.Kaina || pirmas.Kaina == antras.Kaina && pirmas.Pavadinimas.CompareTo(antras.Pavadinimas) > 0;
        }
        static public bool operator <(Leidinys pirmas, Leidinys antras)
        {
            return pirmas.Kaina < antras.Kaina || pirmas.Kaina == antras.Kaina && pirmas.Pavadinimas.CompareTo(antras.Pavadinimas) < 0;
        }

        public override string ToString()
        {
            return string.Format("{0,-5}|{1,11}|{2,-6}", Kodas, Pavadinimas, Kaina);
        }

    }
}
