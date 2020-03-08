using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L2_3_Leidiniai
{
    public class LeidiniuSarasas
    {
    
            private sealed class Mazgas
            {
                public Leidinys Duom { get; set; }
                public Mazgas Kitas { get; set; }
                public Mazgas() { }
                public Mazgas(Leidinys reikme, Mazgas adr)
                {
                    Duom = reikme;
                    Kitas = adr;
                }
            }
            private Mazgas pr;  //Pradžia
            private Mazgas pb;  //Pabaiga
            private Mazgas pre; //Eilei formuoti
            private Mazgas d;   //Sąsaja
                                //Pradinės sąrašo reikšmės
            public LeidiniuSarasas()
            {
                this.pb = new Mazgas(new Leidinys(), null);
                this.pr = new Mazgas(new Leidinys(), pb);
                pre = pr;
                this.d = null;
            }
            /// <summary>
            /// Įdeda duomenis į sarašą
            /// </summary>
            /// <param name="naujas">Spalva</param>
            public void DetiDuomenisA(Leidinys naujas)
            {
                pr.Kitas = new Mazgas(naujas, pr.Kitas);
            }

            /// <summary>
            /// Pradžios sąsaja
            /// </summary>
            public void Pradzia()
            {
                d = pr;
            }

            /// <summary>
            /// Išvedimo pradžios sąsaja
            /// </summary>
            public void PradziaIsvedimui()
            {
                d = pr.Kitas;
            }

            /// <summary>
            /// Kitas elementas
            /// </summary>
            public void Kitas()
            {
                d = d.Kitas;
            }

            /// <summary>
            /// Ar nėra tuščias sąrašas
            /// </summary>
            /// <returns>Ar nėra tuščias</returns>
            public bool Yra()
            {
                return d != null;
            }

            /// <summary>
            /// Paima duomenis
            /// </summary>
            /// <returns>Duomenis</returns>
            public Leidinys ImtiDuomenis()
            {
                return d.Duom;
            }

            /// <summary>
            /// Ar yra išvedimui
            /// </summary>
            /// <returns>Ar yra</returns>
            public bool YraIsvedimui()
            {
                return d.Kitas != null;
            }
            public void Rikiuoti()
            {
                for (Mazgas d1 = pr.Kitas; d1.Kitas != null; d1 = d1.Kitas)
                {
                    Mazgas minv = d1;
                    for (Mazgas d2 = d1.Kitas; d2.Kitas != null;
                    d2 = d2.Kitas)
                        if (d2.Duom < minv.Duom)
                        {
                            minv = d2;
                        }
                    Leidinys St = d1.Duom;
                    d1.Duom = minv.Duom;
                    minv.Duom = St;
                }
            }
    }
}