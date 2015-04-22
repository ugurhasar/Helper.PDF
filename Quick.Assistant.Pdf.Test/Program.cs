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
            Stream stream = File.Open(@"D:\score_card-1.pdf", FileMode.Open);

            MemoryStream memoryStream = new MemoryStream();

            PdfReader reader = new PdfReader(stream);

            PdfStamper pdfStamper = new PdfStamper(reader, memoryStream);

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            Rectangle rec = pdfProcess.GetFieldPostions(pdfStamper, "txtReference");

            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;


            PdfPCell cell00_0 = pdfProcess.CreateCell("#", 0, 5, 8, 5, true);
            cell00_0.BorderWidthRight = 0.5f;
            table.AddCell(cell00_0);
            PdfPCell cell0_0 = pdfProcess.CreateCell("Kat", 0, 5, 8, 10, true);
            table.AddCell(cell0_0);
            PdfPCell cell1_0 = pdfProcess.CreateCell("Kat m²", 0, 5, 8, 5, true);
            table.AddCell(cell1_0);
            PdfPCell cell2_0 = pdfProcess.CreateCell("Kiriş Altı Yükseklik", 0, 5, 8, 5, true);
            table.AddCell(cell2_0);
            PdfPCell cell3_0 = pdfProcess.CreateCell("Tavan Yüksekliği", 0, 5, 8, 5, true);
            table.AddCell(cell3_0);

            Random random = new Random();

            for (int i = 1; i < 100; i++)
            {
                PdfPCell cell00_1 = pdfProcess.CreateCell(i.ToString(), 10, 5, 8, 5);
                cell00_1.BorderWidthRight = 0.5f;
                table.AddCell(cell00_1);
                PdfPCell cell0_1 = pdfProcess.CreateCell((i - 10).ToString(), 10, 5, 8, 10);
                table.AddCell(cell0_1);
                PdfPCell cell1_1 = pdfProcess.CreateCell(random.Next(80, 200).ToString(), 10, 5, 8, 5);
                table.AddCell(cell1_1);
                PdfPCell cell2_1 = pdfProcess.CreateCell(random.Next(2, 10).ToString(), 10, 5, 8, 5);
                table.AddCell(cell2_1);
                PdfPCell cell3_1 = pdfProcess.CreateCell(random.Next(2, 10).ToString(), 10, 5, 8, 5);
                table.AddCell(cell3_1);
            }

            table.SetTotalWidth(new float[] { 30, 95, 95, 150, 150 });

            int currentPageNumber = 3;
            float pageHeight = reader.GetPageSize(1).Height;
            float yMargin = 50;

            float rowHeight = table.Rows.OrderByDescending(x => x.GetMaxRowHeightsWithoutCalculating()).FirstOrDefault().GetMaxRowHeightsWithoutCalculating();
            float avaliableTotalArea = pageHeight - (2 * yMargin);

            float avaliableFirstArea = rec.Top - (2 * yMargin);

            int selectRowCount = (int)(avaliableTotalArea / rowHeight);

            int firstSelectRowCount = (int)(avaliableFirstArea / rowHeight);

            PdfContentByte pdfContentByte = pdfStamper.GetOverContent(currentPageNumber);

            table.WriteSelectedRows(0, firstSelectRowCount, 40, rec.Top - 40, pdfContentByte);

            decimal totalPagePercentage = (decimal)(table.Rows.Count - 10) / 20;

            for (int i = 0; i < totalPagePercentage; i++)
            {
                int startRowIndex = i * selectRowCount + firstSelectRowCount;
                int endRowIndex = startRowIndex + selectRowCount;

                currentPageNumber++;
                pdfStamper.InsertPage(currentPageNumber, reader.GetPageSize(1));

                pdfContentByte = pdfStamper.GetOverContent(currentPageNumber);

                table.WriteSelectedRows(startRowIndex, endRowIndex, 40, pageHeight - yMargin, pdfContentByte);
            }

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);

            int totalPage = reader.NumberOfPages;
            int counter = 1;
            while (counter <= reader.NumberOfPages)
            {
                pdfContentByte = pdfStamper.GetOverContent(counter);
                pdfContentByte.BeginText();

                if (counter == 1)
                {
                    pdfContentByte.SetFontAndSize(bf, 18);
                    pdfContentByte.SetTextMatrix(35, 760);
                    pdfContentByte.ShowText("Score Card (SCA-1)");
                }

                pdfContentByte.SetFontAndSize(bf, 10);
                pdfContentByte.SetTextMatrix(550, 20);
                pdfContentByte.ShowText(counter.ToString());
                pdfContentByte.EndText();

                counter++;
            }

            pdfStamper.Close();

            File.WriteAllBytes("D:\\itextsharp.pdf", memoryStream.ToArray());
        }
    }
}
