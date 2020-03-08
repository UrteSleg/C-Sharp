namespace _22
{
    class Saldytuvas
    {
        public string Gamintojas;
        public string Modelis;
        public int Talpa;
        public string EnergijosKlase;
        public string MontavimoTipas;
        public string Spalva;
        public string TuriSaldikli;
        public int Kaina;

        public Saldytuvas ()
        {
        }
        public Saldytuvas (string gamintojas, string modelis, int talpa, string energijosklase, string montavimotipas, string spalva, string turisaldikli, int kaina)
        {
            Gamintojas = gamintojas;
            Modelis = modelis;
            Talpa = talpa;
            EnergijosKlase = energijosklase;
            MontavimoTipas = montavimotipas;
            Spalva = spalva;
            TuriSaldikli = turisaldikli;
            Kaina = kaina;
        }
    }
}
