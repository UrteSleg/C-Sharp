using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace _1.Unikalūs_žodžiai
{
    class Program
    {
        static Dictionary<string, int> UniqueWords(string words)
        {
            return Regex.Matches(words, @"\w+").
                   OfType<Match>().
                   GroupBy(x => x.Value).
                   ToDictionary(x => x.Key, x => x.Count());
        }

        static Dictionary<string, int> OnlyInFile1(Dictionary<string, int> file1, Dictionary<string, int> file2)
        {
            Dictionary<string, int> unique = new Dictionary<string, int>();

            foreach (var pair in file1)
                if (!file2.ContainsKey(pair.Key))
                    unique.Add(pair.Key, pair.Value);

            return unique;
        }

        static Dictionary<string, int> InBothFiles(Dictionary<string, int> file1, Dictionary<string, int> file2)
        {
            Dictionary<string, int> unique = new Dictionary<string, int>();

            foreach (var pair in file1)
                if (file2.ContainsKey(pair.Key))
                    unique.Add(pair.Key, pair.Value + file2[pair.Key]);

            return unique;
        }

        static Dictionary<string, int> SortByValueDescending(Dictionary<string, int> data)
        {
            return data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        static string CreateManoKnyga(string file1, string file2)
        {
            string[] firstFile = Regex.Matches(file1, @"(\w+|\W+)").OfType<Match>().Select(x => x.Value).ToArray();
            string[] secondFile = Regex.Matches(file2, @"(\w+|\W+)").OfType<Match>().Select(x => x.Value).ToArray();

            int fileIndex = 0;

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < firstFile.Length; i += 2)
            {
                if (firstFile[i] != secondFile[fileIndex])
                    output.Append(firstFile[i] + firstFile[i + 1]);
                else
                {
                    int tmp1 = i + 2;
                    i = fileIndex - 2;
                    fileIndex = tmp1;

                    string[] tmp2 = firstFile;
                    firstFile = secondFile;
                    secondFile = tmp2;
                }
            }

            if(fileIndex < secondFile.Length)
                for (int i = fileIndex; i < secondFile.Length; i++)
                    output.Append(secondFile[i]);

            return output.ToString();
        }

        static void PrintUniqueWordsInFile1(Dictionary<string, int> words)
        {
            using (StreamWriter writer = new StreamWriter("Analizė.txt"))
            {
                writer.WriteLine("Unikalių žodžių esančių tik pirmame faile kiekis: {0}", words.Count);
                writer.WriteLine("10 daugiausiai paskiartojančių unikalių žodžių esančių tik pirmame faile");
                for (int i = 0; i < 10 && i < words.Count; i++)
                    writer.WriteLine("{0}, {1}", words.ElementAt(i).Key, words.ElementAt(i).Value);
                writer.WriteLine();
            }
        }

        static void PrintUniqueWordsInFile1ToConsole(Dictionary<string, int> words)
        {
            Console.WriteLine("Unikalių žodžių esančių tik pirmame faile kiekis: {0}", words.Count);
            Console.WriteLine("10 daugiausiai paskiartojančių unikalių žodžių esančių tik pirmame faile");
            for (int i = 0; i < 10 && i < words.Count; i++)
                Console.WriteLine("{0}, {1}", words.ElementAt(i).Key, words.ElementAt(i).Value);
            Console.WriteLine();
        }

        static void PrintUniqueWordsInBothFiles(Dictionary<string, int> words)
        {
            using (StreamWriter writer = new StreamWriter("Analizė.txt", true))
            {
                writer.WriteLine("Unikalių žodžių esančių abiejuose failuose kiekis: {0}", words.Count);
                writer.WriteLine("10 daugiausiai paskiartojančių unikalių žodžių esančių abiejuose failuose");
                for (int i = 0; i < 10  && i < words.Count; i++)
                    writer.WriteLine("{0}, {1}", words.ElementAt(i).Key, words.ElementAt(i).Value);
                writer.WriteLine();
            }
        }

        static void PrintManoKnyga(string contents)
        {
            File.WriteAllText("ManoKnyga.txt", contents);
        }

        static void Main(string[] args)
        {
            //string separators = ".,!?:;()\t'\r\n";

            string file1 = File.ReadAllText("Knyga1.txt", Encoding.GetEncoding(1257));
            string file2 = File.ReadAllText("Knyga2.txt", Encoding.GetEncoding(1257));

            PrintUniqueWordsInFile1(SortByValueDescending(OnlyInFile1(UniqueWords(file1), UniqueWords(file2))));
            PrintUniqueWordsInBothFiles(SortByValueDescending(InBothFiles(UniqueWords(file1), UniqueWords(file2))));

            PrintManoKnyga(CreateManoKnyga(file1, file2));

            Console.ReadKey();
        }
    }
}
