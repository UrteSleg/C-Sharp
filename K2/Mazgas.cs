using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontras
{
    public sealed class Mazgas<tipas> where tipas: Auto, IComparable
    {
        public tipas Duomenis { get; set; }
        public Mazgas<tipas> Kitas { get; set; }
        public Mazgas(tipas duom, Mazgas<tipas> adr)
        {
            this.Duomenis = duom;
            this.Kitas = adr;
        }
    }
}
