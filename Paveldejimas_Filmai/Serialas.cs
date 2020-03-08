using System;

namespace _21
{
    class Serialas : Irasas
    {

        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool isGoing { get; set; }
        public int episodesCount { get; set; }
        public Serialas(string line) : base(line)
        {
            SetData(line);
        }

        public Serialas(string name, int startYear, string studio, string genre, string first_actor, string second_actor, int endYear, bool IsGoing, int epCount) : base(name,studio,genre,first_actor,second_actor)
        {
            Name = name;
            Genre = genre;
            Studio = studio;
            First_actor = first_actor;
            Second_actor = second_actor;
            StartYear = startYear;
            EndYear = endYear;
            isGoing = IsGoing;
            episodesCount = epCount;
        }

        public override void SetData(string line)
        {
            base.SetData(line);
            string[] reiksmes = line.Split(';');
            StartYear = int.Parse(reiksmes[5]);
            EndYear = int.Parse(reiksmes[6]);
            isGoing = bool.Parse(reiksmes[7]);
            episodesCount = int.Parse(reiksmes[8]);
        }

        public override string ToString()
        {
            string line;
            if (isGoing)
                line = string.Format("{0} {1} {2} {3} {4} {5}, Number of episodes: {6}", Name,  Genre, Studio, First_actor, Second_actor, StartYear, episodesCount);
            else
                line = string.Format("{0} {1}-{2} {3} {4} {5} {6}, Number of episodes: {7}", Name, Genre, Studio, First_actor, Second_actor, StartYear, EndYear, episodesCount);
            return line;
        }

    }
}
