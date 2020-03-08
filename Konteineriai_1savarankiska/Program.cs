using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace _1_savarankiska
{
    class Program
    {
        public const int NumberOfBranches = 3;
        public const int MaxNumberOfBreeds = 10;
        public const int MaxNumberOfAnimals = 50;

        static void Main(string[] args)
        {
            Branch[] branches = new Branch[NumberOfBranches];

            branches[0] = new Branch("Kaunas");
            branches[1] = new Branch("Vilnius");
            branches[2] = new Branch("Šiauliai");

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");

            foreach (string path in filePaths)
            {
                ReadAnimalData(path, branches);
            }

            Console.WriteLine("Kaune užregistruoti šunys:");
            PrintAnimalsToConsole(branches[0].Dogs);
            Console.WriteLine();

            Console.WriteLine("Kaune užregistruotos katės:");
            PrintAnimalsToConsole(branches[0].Cats);

            Console.WriteLine("Kaune užregistruotos jūrų kiaulytės:");
            PrintAnimalsToConsole(branches[0].GuineaPigs);

            Console.WriteLine();
            Console.WriteLine("Agresyvus Kauno šunys: {0}", CountAggressive(branches[0].Dogs));
            Console.WriteLine("Agresyvus Vilniaus šunys: {0}", CountAggressive(branches[1].Dogs));

            AnimalsContainer kaunasDogs = branches[0].Dogs;
            AnimalsContainer vilniusCats = branches[1].Cats;

            Console.Out.WriteLine("Populiariausia šunų veislė Kaune: {0}", GetMostPopularBreed(kaunasDogs));
            Console.Out.WriteLine("Populiariausia kačių veislė Vilniuje: {0}", GetMostPopularBreed(vilniusCats));
            Console.WriteLine();

            Console.WriteLine("Surūšiuotas visų filialų šunų sąrašas:");
            Console.WriteLine();
            AnimalsContainer allDogs = new AnimalsContainer(Program.MaxNumberOfAnimals * Program.NumberOfBranches);
            for (int i = 0; i < NumberOfBranches; i++)
            {
                for (int j = 0; j < branches[i].Dogs.Count; j++)
                {
                    allDogs.AddAnimal(branches[i].Dogs.GetAnimal(j));
                }
            }
            allDogs.SortAnimals();
            PrintAnimalsToConsole(allDogs);
            Console.WriteLine();
            Console.WriteLine("Surūšiuotas visų filialų kačių sąrašas:");
            Console.WriteLine();
            AnimalsContainer allCats = new AnimalsContainer(Program.MaxNumberOfAnimals * Program.NumberOfBranches);
            for (int i = 0; i < NumberOfBranches; i++)
            {
                for (int j = 0; j < branches[i].Cats.Count; j++)
                {
                    allCats.AddAnimal(branches[i].Cats.GetAnimal(j));
                }
            }
            allCats.SortAnimals();
            PrintAnimalsToConsole(allCats);
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Surūšiuotas visų filialų jūros kiaulyčių sąrašas:");
            Console.WriteLine();
            AnimalsContainer allGuineaPigs = new AnimalsContainer(Program.MaxNumberOfAnimals * Program.NumberOfBranches);
            for (int i = 0; i < NumberOfBranches; i++)
            {
                for (int j = 0; j < branches[i].GuineaPigs.Count; j++)
                {
                    allGuineaPigs.AddAnimal(branches[i].GuineaPigs.GetAnimal(j));
                }
            }
            allGuineaPigs.SortAnimals();
            PrintAnimalsToConsole(allGuineaPigs);
            Console.ReadKey();


        }

        static void PrintAnimalsToConsole(AnimalsContainer animals)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine("Nr {0,-2}: {1}", (i + 1), animals.GetAnimal(i).ToString());
            }
        }

        private static Branch GetBranchByTown(Branch[] branches, string town)
        {
            for (int i = 0; i < NumberOfBranches; i++)
            {
                if (branches[i].Town == town)
                {
                    return branches[i];
                }
            }
            return null;
        }

        private static void ReadAnimalData(string file, Branch[] branches)
        {

            string town = null;

            using (StreamReader reader = new StreamReader(@file, Encoding.GetEncoding(1257)))
            {
                string line = null;
                line = reader.ReadLine();
                if (line != null)
                {
                    town = line;
                }
                Branch branch = GetBranchByTown(branches, town);
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(',');
                    char type = Convert.ToChar(line[0]);
                    string name = values[1];
                    string breed = values[2];
                    string owner = values[3];
                    string phone = values[4];
                    DateTime vd = DateTime.Parse(values[5]);

                    switch (type)
                    {
                        case 'D':
                            int chipId = int.Parse(values[6]);
                            bool aggressive = bool.Parse(values[7]);
                            Dog dog = new Dog(name, breed, owner, phone, vd, chipId, aggressive);
                            if (!branch.Dogs.Contains(dog))
                            {
                                branch.Dogs.AddAnimal(dog);
                            }
                            break;
                        case 'C':
                            chipId = int.Parse(values[6]);
                            Cat cat = new Cat(name, breed, owner, phone, vd, chipId);
                            if (!branch.Cats.Contains(cat))
                            {
                                branch.Cats.AddAnimal(cat);
                            }
                            break;
                        case 'P':
                            GuineaPig guineaPig = new GuineaPig(name, breed, owner, phone, vd);
                            if (!branch.GuineaPigs.Contains(guineaPig))
                            {
                                branch.GuineaPigs.AddAnimal(guineaPig);
                            }
                            break;
                    }
                }
            }

        }

        private static void GetBreeds(AnimalsContainer animals, out string[] breeds, out int breedCount)
        {
            breeds = new string[MaxNumberOfBreeds];
            breedCount = 0;

            for (int i = 0; i < animals.Count; i++)
            {
                string breed = animals.GetAnimal(i).Breed;
                if (!breeds.Contains(breed))
                {
                    breeds[breedCount++] = breed;
                }
            }
        }


        private static AnimalsContainer FilterByBreed(AnimalsContainer animals, string breed)
        {
            AnimalsContainer filteredAnimals = new AnimalsContainer(Program.MaxNumberOfAnimals);
            for (int i = 0; i < animals.Count; i++)
            {
                if (animals.GetAnimal(i).Breed == breed)
                {
                    filteredAnimals.AddAnimal(animals.GetAnimal(i));
                }
            }

            return filteredAnimals;
        }

        private static int CountAggressive(AnimalsContainer animals)
        {
            int counter = 0;
            for (int i = 0; i < animals.Count; i++)
            {
                Dog dog = animals.GetAnimal(i) as Dog;
                if (dog != null && dog.Aggressive)
                {
                    counter++;
                }
            }

            return counter;
        }

        private static string GetMostPopularBreed(AnimalsContainer animals)
        {
            String popular = "not found";
            int count = 0;

            int breedCount = 0;
            string[] breeds;

            GetBreeds(animals, out breeds, out breedCount);

            for (int i = 0; i < breedCount; i++)
            {
                AnimalsContainer filteredAnimals = FilterByBreed(animals, breeds[i]);
                if (filteredAnimals.Count > count)
                {
                    popular = breeds[i];
                    count = filteredAnimals.Count;
                }
            }

            return popular;
        }
    }
}
