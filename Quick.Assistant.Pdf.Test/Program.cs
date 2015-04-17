using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Assistant.Pdf.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfProcess pdfProcess = new PdfProcess();
            Stream stream = File.Open(@"D:\existingPlusFields-2.pdf", FileMode.Open);

            PdfReader reader = new PdfReader(stream);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(@"D:\existingPlusFields-1.pdf", FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            pdfProcess.SetFieldValue(pdfStamper, "txtScoreCard", "");

            string x =pdfFormFields.GetField("Alternatif__ube_Eikogd1msP2HElxT63ZxJw");
            AcroFields.Item ite = pdfFormFields.GetFieldItem("Alternatif__ube_Eikogd1msP2HElxT63ZxJw");
            PdfDictionary div = ite.GetWidget(0);
            


            //Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 8, Font.NORMAL, BaseColor.BLACK);
            //pdfFormFields.SetFieldProperty("Desc_", "textfont",bold, null);

            Rectangle rec = pdfProcess.GetFieldPostions(pdfStamper, "Alternatif__ube_Eikogd1msP2HElxT63ZxJw");

            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            //table.SetWidths(new float[] { 0.2f, 0.2f, 2f, 2f });
            Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 8, Font.NORMAL, BaseColor.BLACK);

            PdfPCell cell00_0 = pdfProcess.CreateCell(font, "#", new float[] { 8, 5, 0, 5 }, true);
            cell00_0.BorderWidthRight = 0.5f;
            table.AddCell(cell00_0);
            PdfPCell cell0_0 = pdfProcess.CreateCell(font, "Kat", new float[] { 8, 5, 0, 5 }, true);
            table.AddCell(cell0_0);
            PdfPCell cell1_0 = pdfProcess.CreateCell(font, "Kat m²", new float[] { 8, 5, 0, 5 }, true);
            table.AddCell(cell1_0);
            PdfPCell cell2_0 = pdfProcess.CreateCell(font, "Kiriş Altı Yükseklik", new float[] { 8, 5, 0, 5 }, true);
            table.AddCell(cell2_0);
            PdfPCell cell3_0 = pdfProcess.CreateCell(font, "Tavan Yüksekliği", new float[] { 8, 5, 0, 5 }, true);
            table.AddCell(cell3_0);

            PdfPCell cell00_1 = pdfProcess.CreateCell(font, "1", new float[] { 8, 5, 10, 5 }, false);
            cell00_1.BorderWidthRight = 0.5f;
            table.AddCell(cell00_1);
            PdfPCell cell0_1 = pdfProcess.CreateCell(font, "-1", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell0_1);
            PdfPCell cell1_1 = pdfProcess.CreateCell(font, "100", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell1_1);
            PdfPCell cell2_1 = pdfProcess.CreateCell(font, "5", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell2_1);
            PdfPCell cell3_1 = pdfProcess.CreateCell(font, "3", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell3_1);

            PdfPCell cell00_2 = pdfProcess.CreateCell(font, "2", new float[] { 8, 5, 10, 5 }, false);
            cell00_2.BorderWidthRight = 0.5f;
            table.AddCell(cell00_2);
            PdfPCell cell0_2 = pdfProcess.CreateCell(font, "0", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell0_2);
            PdfPCell cell1_2 = pdfProcess.CreateCell(font, "100", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell1_2);
            PdfPCell cell2_2 = pdfProcess.CreateCell(font, "6", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell2_2);
            PdfPCell cell3_2 = pdfProcess.CreateCell(font, "3", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell3_2);

            PdfPCell cell00_3 = pdfProcess.CreateCell(font, "3", new float[] { 8, 5, 10, 5 }, false);
            cell00_3.BorderWidthRight = 0.5f;
            table.AddCell(cell00_3);
            PdfPCell cell0_3 = pdfProcess.CreateCell(font, "Asma Kat", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell0_3);
            PdfPCell cell1_3 = pdfProcess.CreateCell(font, "100", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell1_3);
            PdfPCell cell2_3 = pdfProcess.CreateCell(font, "3", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell2_3);
            PdfPCell cell3_3 = pdfProcess.CreateCell(font, "1,5", new float[] { 8, 5, 10, 5 }, false);
            table.AddCell(cell3_3);

            PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);
            //pdfContentByte.Add(table); 

            table.SetTotalWidth(new float[] { 20, 100, 100, 150, 150 });
            table.WriteSelectedRows(0, -1, 35, rec.Top - 40, pdfContentByte);
            //pdfStamper.Close();
            //BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            //pdfContentByte.SetColorFill(BaseColor.BLUE);
            //pdfContentByte.SetFontAndSize(baseFont, 8);


            //pdfContentByte.BeginText();
            //pdfContentByte.ShowText("Kevin Cheng - A Hong Kong actor");
            //pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Kevin Cheng - A Hong Kong actor", 50, 50, 0);
            //pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Kevin Cheng - A Hong Kong actor", 400, 600, 0);
            //pdfContentByte.EndText();
            //pdfStamper.Close();
            //ct.AddElement(table);
            //PageEventHelper pdfCreator = new PageEventHelper();

            //    

            //    PdfPTable table = new PdfPTable(2);
            //    table.WidthPercentage = 100;
            //    table.SetWidths(new float[] { 0.2f, 0.8f});

            //    PdfPCell cell0 = pdfCreator.CreateCell(font, "#", new float[] { 10, 5, 10, 5 }, true, 0);
            //    table.AddCell(cell0);
            //    PdfPCell cell1 = pdfCreator.CreateCell(font, "Show", new float[] { 10, 5, 10, 5 }, true, 0);
            //    table.AddCell(cell1);
            //    table.DefaultCell.Border = 0;

            //    //for (int i = 0; i < ratingReportItems.Length; i++)
            //    //{
            //    //    if (ratingReportItems[i].TotalValue == 0)
            //    //        continue;

            //    //    table.AddCell(pdfCreator.CreateCell(font, (i + 1).ToString(), new float[] { 0, 5, 0, 5 }, false, i));
            //    //    table.AddCell(pdfCreator.CreateCell(font, ratingReportItems[i].Title, new float[] { 0, 5, 0, 5 }, false, i));
            //    //    table.AddCell(pdfCreator.CellImage(ratingReportItems[i].Channels, i));
            //    //    table.AddCell(pdfCreator.CreateCell(font, string.Concat(ratingReportItems[i].UniquePercentage.ToString("G4", new CultureInfo("en-US")), " %"), new float[] { 0, 5, 0, 0 }, false, i));
            //    //    table.AddCell(pdfCreator.CreateCell(font, ratingReportItems[i].TotalValue.ToString("#,##0", new CultureInfo("en-US")), new float[] { 0, 5, 0, 0 }, false, i));
            //    //}

            //    return table;


            //AcroFields.FieldPosition fieldPosition = pdfStamper.AcroFields.GetFieldPositions("Alternatif__ube_Eikogd1msP2HElxT63ZxJw").FirstOrDefault();

            //float top = 0, right = 0, bottom = 0, left = 0;

            //pdfProcess.GetFieldPostions(pdfStamper, "Alternatif__ube_Eikogd1msP2HElxT63ZxJw", out top, out right, out bottom, out left);

            //Rectangle rec = fieldPosition.position;
            //rec.Top = top + 100;
            //rec.Bottom = bottom - 100;
            //rec.Left = left;
            //rec.Right = right;

            //pdfProcess.CreateTextField(pdfStamper, rec, "test", "test", 1);
            //TextField field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(40, 500, 360, 530), "some_text");

            // add the field here, the second param is the page you want it on         
            //pdfStamper.AddAnnotation(field.GetTextField(), 1);
            //pdfFormFields.SetField("Score_Card_V6XJ8AHF0Wgkcr2GSsQGww", "mobidik canım");
            //IList<iTextSharp.text.pdf.AcroFields.FieldPosition> x = pdfFormFields.GetFieldPositions("Score_Card_V6XJ8AHF0Wgkcr2GSsQGww");
            pdfStamper.Close();

            //byte[] bytes = pdfProcess.Write(null);

            //File.WriteAllBytes("D:\\itextsharp.pdf", bytes);
        }
        public static void Mobidik(PdfReader reader)
        {
            FileStream _out = new FileStream(@"D:\existingPlusFields.pdf", FileMode.Create, FileAccess.Write);

            PdfStamper stamp = new PdfStamper(reader, _out);

            TextField field = new TextField(stamp.Writer, new iTextSharp.text.Rectangle(40, 500, 360, 530), "some_text");
            // add the field here, the second param is the page you want it on         
            stamp.AddAnnotation(field.GetTextField(), 1);

            stamp.FormFlattening = true; // lock fields and prevent further edits.

            stamp.Close();
        }
    }
}
