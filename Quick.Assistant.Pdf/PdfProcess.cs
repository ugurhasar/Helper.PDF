using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Assistant.Pdf
{
    public class PdfProcess : BaseManager
    {
        public void CreateTextField(PdfStamper pdfStamper, Rectangle box, string fieldName, string text, int page)
        {
            TextField field = new TextField(pdfStamper.Writer, box, fieldName);
            field.Text = text;
      
            pdfStamper.AddAnnotation(field.GetTextField(), 1);
        }

        public void SetFieldValue(PdfStamper pdfStamper, string fieldName, string text)
        {
           pdfStamper.AcroFields.SetField(fieldName, text);
        }

        public Rectangle GetFieldPostions(PdfStamper pdfStamper, string fieldName)
        {            
            AcroFields.FieldPosition fieldPosition = pdfStamper.AcroFields.GetFieldPositions(fieldName).FirstOrDefault();

            if (fieldPosition == null || fieldPosition.position == null)
                return null;

            return fieldPosition.position;
        }

        //public byte[] Write(DocumentInfo documentInfo) 
        //{
        //    MemoryStream memoryStream = new MemoryStream();

        //    using (Document document = this.CreateDocumentInfo(documentInfo))
        //    {
        //        PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
        //        pdfWriter.CloseStream = false;
        //        pdfWriter.PageEvent = new PageEventHelper();

        //        document.Open(); 
                
        //        PdfPTable pdfPTable = CreateListTable();

        //        document.Add(pdfPTable);
        //        document.Close();
        //    }

        //    return memoryStream.ToArray();
        //}

        public Document CreateDocumentInfo(DocumentInfo documentInfo)
        {
            Document document = new Document(PageSize.A4, 25, 25, 20, 50);

            document.AddCreationDate();

            documentInfo = documentInfo ?? new DocumentInfo();

            document.AddLanguage(documentInfo.Language);
            document.AddAuthor(documentInfo.Author);
            document.AddCreator(documentInfo.Creator);
            document.AddKeywords(documentInfo.Keywords);
            document.AddSubject(documentInfo.Subject);
            document.AddTitle(documentInfo.Title);

            return document;
        }

        //private PdfPTable CreateListTable()
        //{
        //    //PageEventHelper pdfCreator = new PageEventHelper();

        //    Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 12, Font.NORMAL, BaseColor.BLACK);

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
        //}

        public PdfPCell CreateCell(Font font, string text, float[] padding, bool isHeader)
        {
            PdfPCell cell = new PdfPCell();

            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            cell.PaddingBottom = padding[0];
            cell.PaddingLeft = padding[1];
            cell.PaddingTop = padding[2];
            cell.PaddingRight = padding[3];
            if (isHeader)
            {
                font.Size = 12;
                cell.BorderWidthBottom = 1.5f;
            }
            else {
                font.Size = 10;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderColorBottom = BaseColor.GRAY;

            }

            Phrase phrase = new Phrase(text, font);

            cell.AddElement(phrase);

            return cell;
        }
    }
}
