namespace _22
{
    class ParduotuviuKonteineris
    {
        string pavadinimas;
        string adresas;
        string numeris;
        Saldytuvas[] saldytuvas;
        public int count {get; set;}

        public ParduotuviuKonteineris(int size, string pav, string adresas, string num)
        {
            saldytuvas = new Saldytuvas[size];
            pavadinimas = pav;
            this.adresas = adresas;
            numeris = num;
            count = 0;
        }

        public Saldytuvas GetSaldytuvas(int index)
        {
            return saldytuvas[index];
        }

        public void AddSaldytuvas(Saldytuvas saldytuvas)
        {
            this.saldytuvas[count] = saldytuvas;
            count++;
        }

        public string GetPavadinimas()
        {
            return pavadinimas;
        }

        public string GetAdresas()
        {
            return adresas;
        }

        public string GetNumeris()
        {
            return numeris;
        }
    }
}
