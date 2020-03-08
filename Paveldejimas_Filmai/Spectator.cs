using System;

namespace _21
{
    class Spectator
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Address { get; set; }
        public int CruisKidmanCount { get; set; }

        public FilmContainer Films;

        public Spectator(string name, int year, string address, int cruisKidmanCount)
        {
            Films = new FilmContainer();
            Name = name;
            Year = year;
            Address = address;
            CruisKidmanCount = cruisKidmanCount;
        }
        public override string ToString()
        {
            string line;
            line = string.Format("{0} {1} {2}", Name, Year, Address);
            return line;
        }
    }
}
