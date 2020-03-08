using System;

namespace _21
{
    class Film : Irasas
    {
        public int Year { get; set; }
        public string Director { get; set; }
        public int Profit { get; set; }
        public Film(string line) : base(line)
        {
            SetData(line);
        }

        public Film(string name, int year, string studio, string genre, string director, string first_actor, string second_actor, int profit) : base(name,studio,genre,first_actor,second_actor)
        {
            Name = name;
            Year = year;
            Genre = genre;
            Studio = studio;
            Director = director;
            First_actor = first_actor;
            Second_actor = second_actor;
            Profit = profit;

        }

        public override void SetData(string line)
        {
            base.SetData(line);
            string[] reiksmes = line.Split(';');
            Year = int.Parse(reiksmes[5]);
            Director = reiksmes[6];
            Profit = int.Parse(reiksmes[7]);
        }


        public override string ToString()
        {
            string line;
            line = string.Format("{0} {1} {2} {3} {4} {5} {6}, Profit: {7}", Name, Genre, Studio, First_actor, Second_actor, Year, Director, Profit);
            return line;
        }
        public bool Equals(Film another)
        {
            return Name == another.Name && Year == another.Year && Genre == another.Genre && Studio == another.Studio && Director == another.Director && First_actor == another.First_actor && Second_actor == another.Second_actor && Profit == another.Profit;
        }
    }
}
