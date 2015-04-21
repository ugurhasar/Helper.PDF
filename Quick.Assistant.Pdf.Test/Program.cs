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
            Stream stream = File.Open(@"D:\score_card.pdf", FileMode.Open);

            MemoryStream memo = new MemoryStream();

            PdfReader reader = new PdfReader(stream);


            ///PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(@"D:\existingPlusFields-1.pdf", FileMode.Create));
            PdfStamper pdfStamper = new PdfStamper(reader, memo);

            AcroFields pdfFormFields = pdfStamper.AcroFields;
            //pdfStamper.Writer.PageEvent = new PageEventHelper();

            ////pdfProcess.SetFieldValue(pdfStamper, "txtScoreCard", "");

            ////string x =pdfFormFields.GetField("Alternatif__ube_Eikogd1msP2HElxT63ZxJw");
            ////AcroFields.Item ite = pdfFormFields.GetFieldItem("Alternatif__ube_Eikogd1msP2HElxT63ZxJw");
            ////PdfDictionary div = ite.GetWidget(0);

            ////Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 8, Font.NORMAL, BaseColor.BLACK);
            ////pdfFormFields.SetFieldProperty("Desc_", "textfont",bold, null);

            Rectangle rec = pdfProcess.GetFieldPostions(pdfStamper, "txtReference");

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

            Random random = new Random();

            for (int i = 1; i < 100; i++)
            {
                PdfPCell cell00_1 = pdfProcess.CreateCell(font, i.ToString(), new float[] { 8, 5, 10, 5 }, false);
                cell00_1.BorderWidthRight = 0.5f;
                table.AddCell(cell00_1);
                PdfPCell cell0_1 = pdfProcess.CreateCell(font, (i - 10).ToString(), new float[] { 8, 5, 10, 5 }, false);
                table.AddCell(cell0_1);
                PdfPCell cell1_1 = pdfProcess.CreateCell(font, random.Next(80, 200).ToString(), new float[] { 8, 5, 10, 5 }, false);
                table.AddCell(cell1_1);
                PdfPCell cell2_1 = pdfProcess.CreateCell(font, random.Next(2, 10).ToString(), new float[] { 8, 5, 10, 5 }, false);
                table.AddCell(cell2_1);
                PdfPCell cell3_1 = pdfProcess.CreateCell(font, random.Next(2, 10).ToString(), new float[] { 8, 5, 10, 5 }, false);
                table.AddCell(cell3_1);
            }

            table.SetTotalWidth(new float[] { 40, 90, 90, 150, 150 });

            Document document = new Document(PageSize.A4, 25, 25, 50, 50);
            MemoryStream tableMemo = new MemoryStream();

            PdfWriter pdfWriter = PdfWriter.GetInstance(document, tableMemo);
            pdfWriter.CloseStream = false;
            pdfWriter.PageEvent = new PageEventHelper();

            document.Open();

            document.Add(table);

            MemoryStream memil = new MemoryStream();
            PdfCopy writer = new PdfCopy(document, memo);

            int n = reader.NumberOfPages;
            // add content, page-by-page

            PdfImportedPage page;
            for (int p = 0; p < n; )
            {
                ++p;
                page = writer.GetImportedPage(reader, p);

                writer.AddPage(page);
            }

            document.Close();

            //int n = reader.NumberOfPages;
            //PdfContentByte pdfContentByte = null;
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);



            //for (int i = 1; i <= n; i++)
            //{
            //    pdfContentByte = pdfStamper.GetOverContent(i);
            //    pdfContentByte.BeginText();

            //    if (i == 1)
            //    {
            //        pdfContentByte.SetFontAndSize(bf, 18);
            //        pdfContentByte.SetTextMatrix(30, 750);
            //        pdfContentByte.ShowText("Score Card (SCA-1)");
            //    }

            //    pdfContentByte.SetFontAndSize(bf, 10);
            //    pdfContentByte.SetTextMatrix(550, 30);
            //    pdfContentByte.ShowText(i.ToString());
            //    pdfContentByte.EndText();
            //}

            ////pdfStamper.InsertPage(4, reader.GetPageSize(3));
            ////pdfContentByte = pdfStamper.GetOverContent(3);



            //Document document = new Document(PageSize.A4, 25, 25, 20, 50);

            //memo.Position = 0;
            //var copy = new PdfCopy(document, memo);
            //copy.SetMergeFields();
            //document.Open();

            //MemoryStream memot = new MemoryStream();
            ////memot.Write(memo.ToArray(), 0, memo.ToArray().Length);
            ////memot.Position = 0;

            //PdfWriter pdfWriter = PdfWriter.GetInstance(document, memot);
            //pdfWriter.CloseStream = false;
            //pdfWriter.PageEvent = new PageEventHelper();

            //document.Open();

            ////PdfPTable pdfPTable = CreateListTable(ratingReportItems);

            //document.Add(table);
            //document.Close();

            //pdfStamper.Close();

            ////byte[] bytes = pdfProcess.Write(null);

            //File.WriteAllBytes("D:\\itextsharp.pdf", memot.ToArray());

            //Document document = null;
            //PdfCopy writer = null;
            //MemoryStream stre = new MemoryStream();
            //try
            //{
            //    document = new Document();
            //    writer = new PdfCopy(document, stre);

            //    writer.PageEvent = new PageEventHelper();
            //    document.Open();

            //    PdfReader reader = new PdfReader(stream);
            //    int n = reader.NumberOfPages;
            //    // add content, page-by-page

            //    PdfImportedPage page;
            //    for (int p = 0; p < n; )
            //    {
            //        ++p;
            //        page = writer.GetImportedPage(reader, p);

            //        writer.AddPage(page);
            //    }

            //    document.Add(table);
            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (document != null && document.IsOpen()) document.Close();
            //}
            //File.WriteAllBytes("D:\\itextsharp.pdf", stre.ToArray());
        }

        static void CreateMergedPDF(string targetPDF, string sourceDir)
        {
            using (FileStream stream = new FileStream(targetPDF, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfCopy pdf = new PdfCopy(pdfDoc, stream);
                pdfDoc.Open();
                var files = Directory.GetFiles(sourceDir);
                Console.WriteLine("Merging files count: " + files.Length);
                int i = 1;
                foreach (string file in files)
                {
                    Console.WriteLine(i + ". Adding: " + file);
                    pdf.AddDocument(new PdfReader(file));
                    i++;
                }

                if (pdfDoc != null)
                    pdfDoc.Close();

                Console.WriteLine("SpeedPASS PDF merge complete.");
            }
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
