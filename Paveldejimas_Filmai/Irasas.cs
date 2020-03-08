using System;

namespace _21
{
    public abstract class Irasas
    {

        public string Name { get; set; }
        public string Genre { get; set; }
        public string Studio { get; set; }
        public string First_actor { get; set; }
        public string Second_actor { get; set; }
        public Irasas(string line)
        {
            SetData(line);
        }
        public Irasas(string name, string genre, string studio, string firstActor, string secondActor)
        {
            Name = name;
            Genre = genre;
            Studio = studio;
            First_actor = firstActor;
            Second_actor = secondActor;

        }

        public virtual void SetData(string line)
        {
            string[] reiksmes = line.Split(';');
            Name = reiksmes[0];
            Genre = reiksmes[1];
            Studio = reiksmes[2];
            First_actor = reiksmes[3];
            Second_actor = reiksmes[4];
        }

        public static bool operator <=(Irasas i1, Irasas i2)
        {
            int num;
            num = String.Compare(i1.Genre, i2.Genre);
            if (num < 0)
                return true;
            if (num > 0)
                return false;
            if (num == 0)
            {
                num = String.Compare(i1.Name, i2.Name);
                if (num < 0)
                    return true;
            }
           return false;
       }

        public static bool operator >=(Irasas i1, Irasas i2)
        {
            int num;
            num = String.Compare(i1.Genre, i2.Genre);
            if (num > 0)
                return true;
            if (num < 0)
                return false;
            if (num == 0)
            {
                num = String.Compare(i1.Name, i2.Name);
                if (num > 0)
                    return true;
            }
            return false;
        }


    }
}
