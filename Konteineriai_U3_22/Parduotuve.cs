using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_22
{
    class Parduotuve
    {
        public string Pavadinimas { get; set; }
        public string Adresas { get; set; }
        public string Telefonas { get; set; }
        public int ElektriniuVirduliuKiekis { get; set; }
        private ElektrinisVirdulys[] ElektriniaiVirduliai;
        public int MikrobanguKrosneliuKiekis { get; set; }
        private MikrobanguKrosnele[] MikrobanguKrosneles;
        public int SaldytuvuKiekis { get; set; }
        private Saldytuvas[] Saldytuvai;

        public Parduotuve( string pavadinimas, string adresas, string telefonas)
        {

            Pavadinimas = pavadinimas;
            Adresas = adresas;
            Telefonas = telefonas;
            ElektriniuVirduliuKiekis = 0;
            ElektriniaiVirduliai = new ElektrinisVirdulys[100];
            MikrobanguKrosneliuKiekis = 0;
            MikrobanguKrosneles = new MikrobanguKrosnele[100];
            SaldytuvuKiekis = 0;
            Saldytuvai = new Saldytuvas[100];

        }

        public void PridetiElektriniVirduli(ElektrinisVirdulys elektrinisVirdulys)
        {
            ElektriniaiVirduliai[ElektriniuVirduliuKiekis++] = elektrinisVirdulys;
        }

        public ElektrinisVirdulys GrazintiElektriniVirduli(int indeksas)
        {
            return ElektriniaiVirduliai[indeksas];
        }

        public void PridetiMikrobanguKrosnele( MikrobanguKrosnele mikrobanguKrosnele)
        {
            MikrobanguKrosneles[MikrobanguKrosneliuKiekis++] = mikrobanguKrosnele;
        }

        public MikrobanguKrosnele GrazintiMikrobanguKrosnele(int indeksas)
        {
            return MikrobanguKrosneles[indeksas];
        }

        public void PridetiSaldytuva(Saldytuvas saldytuvas)
        {
            Saldytuvai[SaldytuvuKiekis++] = saldytuvas;
        }

        public Saldytuvas GrazintiSaldytuva(int indeksas)
        {
            return Saldytuvai[indeksas];
        }

    }
}
