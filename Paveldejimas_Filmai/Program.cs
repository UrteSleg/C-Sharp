using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace _21
{
    class Program
    {
        bool FilmCheck(FilmContainer filmContainer, Irasas filmToBeChecked)
        {
            for (int i = 0; i < filmContainer.Count; i++)
            {
                if (filmContainer.GetFilm(i).Equals(filmToBeChecked))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Suranda failiukus
        /// </summary>
        /// <param name="Spectators"></param>
        void FileReading(SpectatorBranch Spectators)
        {
            DirectoryInfo d = new DirectoryInfo(@"Duomenys");
            FileInfo[] Files = d.GetFiles("*.txt");
            Program p = new Program();
            foreach (FileInfo file in Files)
            {
                p.ReadFileData("Duomenys/" + file.Name, Spectators);
            }
        }
        /// <summary>
        /// Nuskaito faila
        /// </summary>
        /// <param name="failas"></param>
        /// <param name="Spectators"></param>
        void ReadFileData(string failas, SpectatorBranch Spectators)
        {
            using (StreamReader reader = new StreamReader(failas))
            {
                string name = reader.ReadLine();
                int years = int.Parse(reader.ReadLine());
                string address = reader.ReadLine();

                Spectator spectator = new Spectator(name, years, address, 0);
                FilmContainer filmas = new FilmContainer();

                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] segment = line.Split(';');
                    Film film;
                    Serialas serialas;

                
                    if (segment.Length == 8) {
                        film = new Film(line);
                        spectator.Films.AddFilm(film);
                    }
                    else {
                        serialas = new Serialas(line);
                        spectator.Films.AddFilm(serialas);
                    }
                        
                    
                }
                Spectators.AddSpectator(spectator);
            }
        }
        /// <summary>
        /// Sukuria WarnerBros sukurtu filmu konteineri
        /// </summary>
        /// <param name="Spectators"></param>
        /// <returns></returns>
        FilmContainer WarnerBrosContainerCreate (SpectatorBranch Spectators)
        {
            string studio = "Warner Bros";
            Program p = new Program();
            FilmContainer WarnerBrosContainer = new FilmContainer();
            for (int i = 0; i < Spectators.Count; i++)
            {
                for (int j = 0; j < Spectators.GetSpectator(i).Films.Count; j++)
                {
                    if (Spectators.GetSpectator(i).Films.GetFilm(j).Studio == studio)
                    {
                        if (p.FilmCheck(WarnerBrosContainer, Spectators.GetSpectator(i).Films.GetFilm(j)))
                        {
                            WarnerBrosContainer.AddFilm(Spectators.GetSpectator(i).Films.GetFilm(j));
                        }
                    }
                }
            }
            return WarnerBrosContainer;
        }
        /// <summary>
        /// Atspausdina WarnerBros sukurtus filmus ir serialus
        /// </summary>
        /// <param name="WarnerBrosContainer"></param>
        void WarnerBrosPrint(FilmContainer WarnerBrosContainer)
        {
            Console.WriteLine("WarnerBros sukurti filmai ir serialai: ");
            for (int i = 0; i < WarnerBrosContainer.Count; i++)
            {
                Console.WriteLine(WarnerBrosContainer.GetFilm(i));
            }
        }
        /// <summary>
        /// Suskaiciuoja kiek kuris ziurovas mate filmu kuriuose vaidina N.Kidman.
        /// </summary>
        /// <param name="Spectators"></param>
        void CruisAndKidmanCounter (ref SpectatorBranch Spectators)
        {
            Program p = new Program();
            for (int i = 0; i < Spectators.Count; i++)
            {
                for (int j = 0; j < Spectators.GetSpectator(i).Films.Count; j++)
                {
                    if (p.CruisAndKidmanIsOrNo(Spectators.GetSpectator(i).Films.GetFilm(j)) == true)
                    {
                        Spectators.GetSpectator(i).CruisKidmanCount++;
                    }
                }
            }
        }



        /// <summary>
        /// Metodas patikrina ar filme vaidino T. Cruise ir N.Kidman.
        /// </summary>
        /// <param name="film"></param>
        /// <returns></returns>
        bool CruisAndKidmanIsOrNo(Irasas film)
        {
            string actorOne = "T. Cruise";
            if (film.First_actor == actorOne || film.Second_actor == actorOne)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Suranda kiek daugiausiai vienas ziurovas mate filmu kuriuose vaidino T. Cruise ir N.Kidman.
        /// </summary>
        /// <param name="Spectators"></param>
        /// <returns></returns>
        int CruisAndKidmanMax(SpectatorBranch Spectators)
        {
            int max = 0;
            for (int i = 0; i < Spectators.Count; i++)
            {
                if (Spectators.GetSpectator(i).CruisKidmanCount > max)
                {
                    max = Spectators.GetSpectator(i).CruisKidmanCount;
                }
            }
            return max;
        }
        /// <summary>
        /// Atspausdinama i konsole kas mate daugiausiai filmu, kuriuose vaidina T. Cruise ir N.Kidman.
        /// </summary>
        /// <param name="Spectators"></param>
        /// <param name="max"></param>
        void CruisAndKidmanSpausdinimas(SpectatorBranch Spectators,int max)
        {
            Console.WriteLine("Daugiausiai filmu, kuriose vaidino N. Kidman mate: ");
            for (int i = 0; i < Spectators.Count; i++)
            {
                if (Spectators.GetSpectator(i).CruisKidmanCount == max)
                    Console.WriteLine("{0} {1}", Spectators.GetSpectator(i).Name, Spectators.GetSpectator(i).CruisKidmanCount);
            }
        }
        /// <summary>
        /// Sukuriamas visu filmu konteineris
        /// </summary>
        /// <param name="Spectators"></param>
        /// <returns></returns>
        FilmContainer AllFilmContainerCreate(SpectatorBranch Spectators)
        {
            Program p = new Program();
            FilmContainer AllFilms = new FilmContainer();
            for (int i = 0; i < Spectators.Count; i++)
            {
                for (int j = 0; j < Spectators.GetSpectator(i).Films.Count; j++)
                {
                    if (p.FilmCheck(AllFilms, Spectators.GetSpectator(i).Films.GetFilm(j)))
                    {
                        AllFilms.AddFilm(Spectators.GetSpectator(i).Films.GetFilm(j));
                    }
                }
            }
            return AllFilms;
        }
        /// <summary>
        /// Surandamas nematytu filmu sarasas, kuris iskart siunciamas atspausdinti
        /// </summary>
        /// <param name="Spectators"></param>
        /// <param name="AllFilms"></param>
        void Recomendations(SpectatorBranch Spectators, FilmContainer AllFilms)
        {
            Program p = new Program();
            for (int i = 0; i < Spectators.Count; i++)
            {
                FilmContainer UnseenedFilms = new FilmContainer();
                string[] unseendedFilmsString = new string[50];
                for (int j = 0; j < AllFilms.Count; j++)
                {
                    if (p.FilmCheck(Spectators.GetSpectator(i).Films, AllFilms.GetFilm(j)))
                    {
                        unseendedFilmsString[UnseenedFilms.Count] = string.Format(AllFilms.GetFilm(j).ToString());
                        UnseenedFilms.AddFilm(AllFilms.GetFilm(j));
                    }
                }
                p.RecomendationPrint(unseendedFilmsString, Spectators.GetSpectator(i).Name);
            }
        }
        /// <summary>
        /// Atspausdinami - sukuriami failai (rekomenduomu filmu sarasas)
        /// </summary>
        /// <param name="unseenedFilmsString"></param>
        /// <param name="spectatorName"></param>
        void RecomendationPrint(string[] unseenedFilmsString, string spectatorName)
        {
            string[] segment = spectatorName.Split(' ');
            string name = segment[0];
            string surname = segment[1];
            string fileName = "Rekomendacija_" + name + "_" + surname + ".csv";
            File.WriteAllLines(fileName, unseenedFilmsString);
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            SpectatorBranch Spectators = new SpectatorBranch();
            p.FileReading(Spectators);

            // 1 Uzduotis:  Rasti, kuris kino mėgėjas matė daugiausiai filmų, kuriuose vaidino N.Kidman
            p.CruisAndKidmanCounter(ref Spectators);
            int max = p.CruisAndKidmanMax(Spectators);
            p.CruisAndKidmanSpausdinimas(Spectators, max);

            // 2 Uzduotis:  Rasti, kiek sąraše esančių filmų sukurti kino studijos „Warner Bros“
            FilmContainer WarnerBrosContainer = new FilmContainer();
            WarnerBrosContainer = p.WarnerBrosContainerCreate(Spectators);
            p.WarnerBrosPrint(WarnerBrosContainer);

            // 3 Uzduotis:  Kiekvienam kino mėgėjui sudarykite rekomenduojamų peržiūrėti filmų sąrašą 
            FilmContainer AllFilms = new FilmContainer();
            AllFilms = p.AllFilmContainerCreate(Spectators);
            p.Recomendations(Spectators, AllFilms);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
