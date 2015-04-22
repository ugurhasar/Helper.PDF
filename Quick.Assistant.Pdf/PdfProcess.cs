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

        public PdfPCell CreateCell(string text, float paddingTop, float paddingRight, float paddingBottom, float paddingLeft, bool isHeader = false)
        {
            Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 8, Font.NORMAL, BaseColor.BLACK);

            PdfPCell cell = new PdfPCell(new Phrase(text, font));

            cell.Border = 0;

            cell.PaddingTop = paddingTop;
            cell.PaddingRight = paddingRight;
            cell.PaddingBottom = paddingBottom;
            cell.PaddingLeft = paddingLeft;

            if (isHeader)
            {
                font.Size = 12;
                cell.BorderWidthBottom = 1.5f;
            }
            else
            {
                font.Size = 10;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderColorBottom = BaseColor.GRAY;
            }

            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            return cell;
        }
    }
}
