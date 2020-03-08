using System;

namespace _21
{
    class FilmContainer
    {
        public Irasas[] Films;
        public int Count { get; private set; }
        private const int MAXCOUNT = 100;

        public FilmContainer()
        {
            Films = new Irasas[MAXCOUNT];
        }
        public void AddFilm(Irasas film)
        {
            Films[Count++] = film;
        }
        public void AddFilm(Irasas film, int index)
        {
            Films[index] = film;
        }
        public Irasas GetFilm(int index)
        {
            return Films[index];
        }

        /// <summary>
        /// Pagal reikalavimus surikiuojamas rekomenduomu filmu/serialu sarasas
        /// </summary>
        public void SortRecomendations()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                Irasas minValue = Films[i];
                int minValueIndex = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (Films[j] <= minValue)
                    {
                        minValue = Films[j];
                        minValueIndex = j;
                    }
                }
                Films[minValueIndex] = Films[i];
                Films[i] = minValue;
            }
        }
    }
}
