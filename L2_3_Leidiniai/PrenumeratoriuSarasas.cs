using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2_3_Leidiniai
{
        public class PrenumeratoriuSarasas
        {
            private sealed class Mazgas
            {
                public Prenumeratorius Duom { get; private set; }
                public Mazgas Kitas { get; set; }
                public Mazgas() { }
                public Mazgas(Prenumeratorius reikme, Mazgas adr)
                {
                    Duom = reikme;
                    Kitas = adr;
                }
            }
            private Mazgas pr;  //Pradžia
            private Mazgas pb;  //Pabaiga
            private Mazgas pre; //Sąrašui formuoti
            private Mazgas d;   //Sąsaja
                                //Pradinės sąrašo reikšmės
            public PrenumeratoriuSarasas()
            {
                this.pb = new Mazgas(new Prenumeratorius(), null);
                this.pr = new Mazgas(new Prenumeratorius(), pb);
                pre = pr;
                this.d = null;
            }

            //Deda taškus į sąrašą
            public void DetiDuomenisA(Prenumeratorius naujas)
            {
                pr.Kitas = new Mazgas(naujas, pr.Kitas);
            }

            /// <summary>
            /// Pradžia
            /// </summary>
            public void Pradzia()
            {
                d = pr;
            }

            /// <summary>
            /// Kitas elementas
            /// </summary>
            public void Kitas()
            {
                d = d.Kitas;
            }
            /// <summary>
            /// Ar yra 
            /// </summary>
            /// <returns>Ar yra</returns>
            public bool Yra()
            {
                return d != null;
            }
            public Prenumeratorius ImtiDuomenis()
            {
                return d.Duom;
            }
            /// <summary>
            /// Pradžia išvedimui
            /// </summary>
            public void PradziaIsvedimui()
            {
                d = pr.Kitas;
            }

            /// <summary>
            /// Ar yra išvedimui
            /// </summary>
            /// <returns>Ar yra</returns>
            public bool YraIsvedimui()
            {
                return d.Kitas != null;
            }
        }
}