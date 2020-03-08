using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace L2_3_Leidiniai
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private const string DuomenysApieLeidinius = "App_Data/U3a.txt";  //Informacija apie taškus
        private const string DuomenysApiePrenumeratorius = "App_Data/U3b.txt";  //Informacija apie spalvas
        private const string Rezultatai = "App_Data/Rez.txt"; //Rezultatai
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Rezultatai))
            {
                File.Delete(Rezultatai);
            }
            
            LeidiniuSarasas leidiniuSarasas = Skaiciavimai.NuskaitymasA(DuomenysApieLeidinius, Server);
            PrenumeratoriuSarasas prenumeratoriuSarasas = Skaiciavimai.NuskaitymasB(DuomenysApiePrenumeratorius, Server);
            List<string> pajamos = Skaiciavimai.KiekvienoMenesioDidziausiosPajamos(prenumeratoriuSarasas, leidiniuSarasas);
            double bendrosPajamos = Skaiciavimai.BendrosiosLeidiniuPajamos(prenumeratoriuSarasas, leidiniuSarasas);
            LeidiniuSarasas didesniUzVidurki = Skaiciavimai.PajamosMazesnesUzVidutines(bendrosPajamos, leidiniuSarasas, prenumeratoriuSarasas);
            didesniUzVidurki.Rikiuoti();
            PrenumeratoriuSarasas atrinkti = Skaiciavimai.Atrinkti(leidiniuSarasas, prenumeratoriuSarasas, TextBox1.Text, TextBox2.Text);
            IsvestiBendrasPajamas(bendrosPajamos);
            SpausdintiMenesioPajamas(pajamos);
            SpausdintiLeidinius(leidiniuSarasas);
            SpausdintiPrenumeratorius(prenumeratoriuSarasas);
            SpausdintiLeidiniusKuriuPajamosMazesnesUzVidutines(didesniUzVidurki);
            SpausdintiAtrinktusLeidinius(atrinkti);
            Skaiciavimai.SpausdintiFaile(Rezultatai, leidiniuSarasas, Server);
        }
            public void SpausdintiMenesioPajamas(List<string> pajamos)
            {
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                cell1.Text = "Mėnesis";
                cell2.Text = "Pavadinimas";
                row1.Cells.Add(cell1);
                row1.Cells.Add(cell2);
                Table3.Rows.Add(row1);

                for (int i = 0; i < 12; i++)
                {
                    TableRow row = new TableRow();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    cell3.Text = Convert.ToString(i + 1);
                    cell4.Text = pajamos[i];
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    Table3.Rows.Add(row);
                }
            }

            public void IsvestiBendrasPajamas(double bendrosPajamos)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = Convert.ToString(bendrosPajamos);
                row.Cells.Add(cell);
                Table4.Rows.Add(row);
            }

            public void SpausdintiLeidiniusKuriuPajamosMazesnesUzVidutines(LeidiniuSarasas leidiniai)
            {
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                cell1.Text = "Kodas";
                cell2.Text = "Pavadinimas";
                cell3.Text = "Kaina";
                row1.Cells.Add(cell1);
                row1.Cells.Add(cell2);
                row1.Cells.Add(cell3);
                Table5.Rows.Add(row1);

                for (leidiniai.PradziaIsvedimui(); leidiniai.YraIsvedimui(); leidiniai.Kitas())
                {
                    TableRow row = new TableRow();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();
                    cell4.Text = Convert.ToString(leidiniai.ImtiDuomenis().Kodas);
                    cell5.Text = leidiniai.ImtiDuomenis().Pavadinimas;
                    cell6.Text = Convert.ToString(leidiniai.ImtiDuomenis().Kaina);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);
                    row.Cells.Add(cell6);
                    Table5.Rows.Add(row);
                }
            }

            public void SpausdintiLeidinius(LeidiniuSarasas leidiniai)
            {
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                cell1.Text = "Kodas";
                cell2.Text = "Pavadinimas";
                cell3.Text = "Kaina";
                row1.Cells.Add(cell1);
                row1.Cells.Add(cell2);
                row1.Cells.Add(cell3);
                Table7.Rows.Add(row1);

                for (leidiniai.PradziaIsvedimui(); leidiniai.YraIsvedimui(); leidiniai.Kitas())
                {
                    TableRow row = new TableRow();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();
                    cell4.Text = Convert.ToString(leidiniai.ImtiDuomenis().Kodas);
                    cell5.Text = leidiniai.ImtiDuomenis().Pavadinimas;
                    cell6.Text = Convert.ToString(leidiniai.ImtiDuomenis().Kaina);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);
                    row.Cells.Add(cell6);
                    Table7.Rows.Add(row);
                }
            }

            public void SpausdintiPrenumeratorius(PrenumeratoriuSarasas prenumeratoriai)
            {
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                TableCell cell5 = new TableCell();
                TableCell cell6 = new TableCell();

                cell1.Text = "Pavarde";
                cell2.Text = "Adresas";
                cell3.Text = "Laikotarpio pradžia";
                cell4.Text = "Laikotarpio ilgis";
                cell5.Text = "Kodas";
                cell6.Text = "Kiekis";

                row1.Cells.Add(cell1);
                row1.Cells.Add(cell2);
                row1.Cells.Add(cell3);
                row1.Cells.Add(cell4);
                row1.Cells.Add(cell5);
                row1.Cells.Add(cell6);
                Table2.Rows.Add(row1);

                for (prenumeratoriai.PradziaIsvedimui(); prenumeratoriai.YraIsvedimui(); prenumeratoriai.Kitas())
                {
                    TableRow row = new TableRow();
                    TableCell cell11 = new TableCell();
                    TableCell cell12 = new TableCell();
                    TableCell cell13 = new TableCell();
                    TableCell cell14 = new TableCell();
                    TableCell cell15 = new TableCell();
                    TableCell cell16 = new TableCell();
                    cell11.Text = prenumeratoriai.ImtiDuomenis().Pavarde;
                    cell12.Text = prenumeratoriai.ImtiDuomenis().Adresas;
                    cell13.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().LaikotarpioPradzia);
                    cell14.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().LaikotarpioIlgis);
                    cell15.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().Kodas);
                    cell16.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().Kiekis);
                    row.Cells.Add(cell11);
                    row.Cells.Add(cell12);
                    row.Cells.Add(cell13);
                    row.Cells.Add(cell14);
                    row.Cells.Add(cell15);
                    row.Cells.Add(cell16);
                    Table2.Rows.Add(row);
                }
            }

            public void SpausdintiAtrinktusLeidinius(PrenumeratoriuSarasas prenumeratoriai)
            {
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                TableCell cell5 = new TableCell();
                TableCell cell6 = new TableCell();

                cell1.Text = "Pavarde";
                cell2.Text = "Adresas";
                cell3.Text = "Laikotarpio pradžia";
                cell4.Text = "Laikotarpio ilgis";
                cell5.Text = "Kodas";
                cell6.Text = "Kiekis";

                row1.Cells.Add(cell1);
                row1.Cells.Add(cell2);
                row1.Cells.Add(cell3);
                row1.Cells.Add(cell4);
                row1.Cells.Add(cell5);
                row1.Cells.Add(cell6);
                Table6.Rows.Add(row1);

                for (prenumeratoriai.PradziaIsvedimui(); prenumeratoriai.YraIsvedimui(); prenumeratoriai.Kitas())
                {
                    TableRow row = new TableRow();
                    TableCell cell11 = new TableCell();
                    TableCell cell12 = new TableCell();
                    TableCell cell13 = new TableCell();
                    TableCell cell14 = new TableCell();
                    TableCell cell15 = new TableCell();
                    TableCell cell16 = new TableCell();
                    cell11.Text = prenumeratoriai.ImtiDuomenis().Pavarde;
                    cell12.Text = prenumeratoriai.ImtiDuomenis().Adresas;
                    cell13.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().LaikotarpioPradzia);
                    cell14.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().LaikotarpioIlgis);
                    cell15.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().Kodas);
                    cell16.Text = Convert.ToString(prenumeratoriai.ImtiDuomenis().Kiekis);
                    row.Cells.Add(cell11);
                    row.Cells.Add(cell12);
                    row.Cells.Add(cell13);
                    row.Cells.Add(cell14);
                    row.Cells.Add(cell15);
                    row.Cells.Add(cell16);
                    Table6.Rows.Add(row);
                }
            }
        }
}