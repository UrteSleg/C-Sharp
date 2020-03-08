using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2_3_Leidiniai
{
    // Icomparer, icomperable, iequatable
    public class Prenumeratorius : IComparer<Prenumeratorius>, IComparable<Prenumeratorius>, IEquatable<Prenumeratorius>
    {
        public string Pavarde { get; set; }
        public string Adresas { get; set; }
        public int LaikotarpioPradzia { get; set; }
        public int LaikotarpioIlgis { get; set; }
        public int Kodas { get; set; }
        public int Kiekis { get; set; }

        public Prenumeratorius()
        {

        }

        public Prenumeratorius(string pavarde, string adresas, int laikotarpioPradzia, int laikotarpioIlgis, int kodas, int kiekis)
        {
            Pavarde = pavarde;
            Adresas = adresas;
            LaikotarpioPradzia = laikotarpioPradzia;
            LaikotarpioIlgis = laikotarpioIlgis;
            Kodas = kodas;
            Kiekis = kiekis;
        }

        public override string ToString()
        {
            return string.Format("{0,-13}|{1,-15}|{2,-2}|{3,-2}|{4,-4}|{5,-2}", Pavarde, Adresas, LaikotarpioPradzia, LaikotarpioIlgis, Kodas, Kiekis);
        }
        
    }
}
