using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontras
{
    public class Auto : IComparable<Auto>, IComparable
    {
        public string Gamintojas { get; private set; }
        public string Modelis { get; private set; }
        public double Kaina { get; private set; }
        
        public Auto(string gamintojas, string modelis, double kaina)
        {
            this.Gamintojas = gamintojas;
            this.Modelis = modelis;
            this.Kaina = kaina;
        }
        public Auto()
        {
        }
        public int CompareTo(Auto kitas)
        {
            if(kitas.Kaina > Kaina)
            {
                return 1;
            }
            if(kitas.Kaina == Kaina)
            {
                return -kitas.Modelis.CompareTo(Modelis);
            }
            return 0;
        }
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Auto);
        }
        public override string ToString()
        {
            return String.Format("|{0,-15}|{1,-25}|{2,10}|", Gamintojas, Modelis, Kaina);
        }
    }
}
