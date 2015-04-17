using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Assistant.Pdf
{
    public class PageEventHelper : PdfPageEventHelper
    {
        //public PdfPCell CreateCell(Font font, string text, float[] padding, bool isHeader, int ratingIndex)
        //{
        //    PdfPCell cell = new PdfPCell();

        //    cell.HorizontalAlignment = 0;
        //    cell.Border = 0;
        //    cell.PaddingBottom = padding[0];
        //    cell.PaddingLeft = padding[1];
        //    cell.PaddingTop = padding[2];
        //    cell.PaddingRight = padding[3];
        //    if (isHeader)
        //    {
        //        font.Size = 16;
        //        cell.BorderWidthBottom = 1.5f;
        //    }
        //    else
        //        font.Size = 14;

        //    //if (IsOdd(ratingIndex))
        //    //    cell.BackgroundColor = new BaseColor(230, 230, 230);

        //    Phrase phrase = new Phrase(text, font);

        //    cell.AddElement(phrase);

        //    return cell;
        //}

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);

            Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 18, Font.BOLD, BaseColor.BLACK);
            Font font1 = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 14, Font.NORMAL, BaseColor.BLACK);
            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(headerLogoUrl);

            //image.ScaleAbsoluteHeight(75f);
            //image.ScaleAbsoluteWidth(75f);

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.WidthPercentage = 100;
            headerTable.SetWidths(new float[] { 0.7f, 1f });
            headerTable.DefaultCell.Border = 0;

            PdfPTable headerTextTable = new PdfPTable(1);
            //PdfPCell dateCell = new PdfPCell(new Phrase(headerDateText, font));
            //dateCell.HorizontalAlignment = 0;
            //dateCell.Border = 0;
            //dateCell.PaddingTop = 30;
            //dateCell.PaddingBottom = 10;
            //headerTextTable.AddCell(dateCell);

            //PdfPCell reportTypeCell = new PdfPCell(new Phrase(headerReportTypeText, font1));
            //reportTypeCell.HorizontalAlignment = 0;
            //reportTypeCell.Border = 0;
            //headerTextTable.AddCell(reportTypeCell);

            headerTable.AddCell(headerTextTable);

            //PdfPCell imageCell = new PdfPCell(image);
            //imageCell.HorizontalAlignment = 2;
            //imageCell.Border = 0;
            //imageCell.PaddingRight = 10;
            //imageCell.PaddingTop = 30;
            //headerTable.AddCell(imageCell);
            //document.Add(headerTable);
            document.Add(new Phrase(Environment.NewLine));
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int currentPageNumber = writer.CurrentPageNumber;
            Font numberFont = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 9, Font.NORMAL, BaseColor.BLACK);
            Font textFont = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 9, Font.NORMAL, BaseColor.GRAY);

            PdfPTable pdfPTable = new PdfPTable(2);
            pdfPTable.TotalWidth = 540;
            pdfPTable.SetWidths(new float[] { 1f, 0.2f });

            //PdfPCell textCell = new PdfPCell(new Phrase(footerText, textFont));
            //textCell.Border = 0;
            //textCell.HorizontalAlignment = 0;
            //pdfPTable.AddCell(textCell);

            PdfPCell numberCell = new PdfPCell(new Phrase(currentPageNumber.ToString(), numberFont));
            numberCell.Border = 0;
            numberCell.HorizontalAlignment = 2;
            pdfPTable.AddCell(numberCell);

            pdfPTable.WriteSelectedRows(0, -1, 25, document.Bottom, writer.DirectContent);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
