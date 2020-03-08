using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kontras
{
    class Program
    {
        static void Main(string[] args)
        {
            double didz;
            string file = @"../../Masinos.txt";
            string file2 = @"../../Rezultatai.txt";
            Modeliai<Auto> A = Skaitymas(file, out didz);
            Modeliai<Auto> B = Panasus(A, didz);
            if(File.Exists(file2))
            {
                File.Delete(file2);
            }
            Spausdinimas(B, "Pries", file2);
            B.Rikiuoti();
            Spausdinimas(B, "Po", file2);
            Spausdinimas(A, "Pradiniai duomenys(ne rikiuoti)", file2);
            A.Rikiuoti();
            Spausdinimas(A, "Pradiniai duomenys(rikiuoti)", file2);
        }
        private static Modeliai<Auto> Skaitymas(string filePath, out double didz)
        {
            didz = 0;
            Modeliai<Auto> Masinos = new Modeliai<Auto>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    if(didz < double.Parse(values[2]))
                    {
                        didz = double.Parse(values[2]);
                    }
                    Auto temp = new Auto(values[0], values[1], double.Parse(values[2]));
                    Masinos.DetiT(temp);
                }
            }
            return Masinos;
        }
        private static Modeliai<Auto> Panasus(Modeliai<Auto> masinos, double didz)
        {
            Modeliai<Auto> atsakymas = new Modeliai<Auto>();
            for(masinos.Pradzia(); masinos.Yra(); masinos.Kitas())
            {
                Auto masina = masinos.GautiD();
                if(didz - masina.Kaina <= didz * 0.25)
                {
                    atsakymas.DetiT(masina);
                }
            }
            return atsakymas;
        }
        private static void Spausdinimas(Modeliai<Auto> masinos, string title, string path)
        {
            using (StreamWriter writer = new StreamWriter(path,true))
            {
                writer.WriteLine("<---*{0}*--->", title);
                string line = new string('-', 54);
                writer.WriteLine(line);
                for(masinos.Pradzia(); masinos.Yra(); masinos.Kitas())
                {
                    writer.WriteLine(masinos.GautiD().ToString());
                    writer.WriteLine(line);
                }
            }
        }
    }
}
