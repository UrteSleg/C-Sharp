using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontras
{
    public sealed class Modeliai<tipas> where tipas: Auto, IComparable
    {
        private Mazgas<tipas> pr;
        private Mazgas<tipas> esamas;
        private Mazgas<tipas> pb;
        public Modeliai()
        {
            pr = null;
            pb = null;
            esamas = null;
        }
        public void Kitas()
        {
            esamas = esamas.Kitas;
        }
        public void Pradzia()
        {
            esamas = pr;
        }
        public bool Yra()
        {
            return esamas != null;
        }
        public tipas GautiD()
        {
            return esamas.Duomenis;
        }
        public void DetiA(tipas duom)
        {
            pr = new Mazgas<tipas>(duom, pr);
        }
        public void DetiT(tipas duom)
        {
            var dd = new Mazgas<tipas>(duom, null);
            if(pr != null)
            {
                pb.Kitas = dd;
                pb = dd;
            }
            else
            {
                pr = dd;
                pb = dd;
            }
        }
        public void Rikiuoti()
        {
            bool surikiuota = false;
            while(!surikiuota)
            {
                surikiuota = !surikiuota;
                for(Pradzia();Yra();Kitas())
                {
                    if(esamas.Kitas != null)
                    {
                        if(esamas.Duomenis.CompareTo(esamas.Kitas.Duomenis) > 0)
                        {
                            tipas kazkas = esamas.Duomenis;
                            esamas.Duomenis = esamas.Kitas.Duomenis;
                            esamas.Kitas.Duomenis = kazkas;
                            surikiuota = false;
                        }
                    }
                }
            }
        }
    }
}
