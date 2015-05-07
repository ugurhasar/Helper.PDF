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
        public BaseFont baseFont = null;
        public Font font = null;
        //public Font font = FontFactory.GetFont("C:\\WINDOWS\\Fonts\\arial.ttf", "CP1254", true, 8, Font.NORMAL, BaseColor.BLACK);

        public float DocumentTopMargin = 50f;
        public float DocumentBottomMargin = 50f;

        public PdfProcess()
        {
            this.baseFont = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\arial.ttf", BaseFont.CP1252, true);
            this.font = new Font(baseFont, 8, Font.NORMAL, BaseColor.BLACK);
        }

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

        public void AddPageNumaber(PdfReader pdfReader, PdfStamper pdfStamper)
        {
            int totalPage = pdfReader.NumberOfPages;
            int counter = 1;

            PdfContentByte pdfContentByte = null;

            while (counter <= pdfReader.NumberOfPages)
            {
                pdfContentByte = pdfStamper.GetOverContent(counter);
                pdfContentByte.BeginText();
                pdfContentByte.SetFontAndSize(baseFont, 10);
                pdfContentByte.SetTextMatrix(575, 15);
                pdfContentByte.ShowText(counter.ToString());
                pdfContentByte.EndText();

                counter++;
            }
        }

        public void AddHeader(PdfReader pdfReader, PdfStamper pdfStamper, string text, int pageNumber = 1)
        {
            PdfContentByte pdfContentByte = pdfStamper.GetOverContent(pageNumber);
            pdfContentByte.BeginText();

            pdfContentByte.SetFontAndSize(baseFont, 18);
            pdfContentByte.SetTextMatrix(35, 755);
            pdfContentByte.ShowText(text);
            pdfContentByte.EndText();
        }

        public void AddTableExistingDocument(PdfPTable pdfPTable, PdfReader pdfReader, PdfStamper pdfStamper, int currentpage, Rectangle referenceRec = null) 
        {
            int currentPageNumber = currentpage;
            float pageHeight = pdfReader.GetPageSize(1).Height;

            float rowHeight = pdfPTable.Rows.OrderByDescending(x => x.GetMaxRowHeightsWithoutCalculating()).FirstOrDefault().GetMaxRowHeightsWithoutCalculating();
            float avaliableTotalArea = pageHeight - (this.DocumentTopMargin + this.DocumentBottomMargin);
            int selectRowCount = (int)(avaliableTotalArea / rowHeight);

            PdfContentByte pdfContentByte = null;
            int firstSelectRowCount = 0;

            if (referenceRec != null)
            {
                float avaliableFirstArea = referenceRec.Top - (this.DocumentTopMargin + this.DocumentBottomMargin);
                firstSelectRowCount = (int)(avaliableFirstArea / rowHeight);

                pdfContentByte = pdfStamper.GetOverContent(currentPageNumber);

                pdfPTable.WriteSelectedRows(0, firstSelectRowCount, 40, referenceRec.Top - 40, pdfContentByte);
            }

            decimal totalPagePercentage = (decimal)(pdfPTable.Rows.Count - firstSelectRowCount) / selectRowCount;

            for (int i = 0; i < totalPagePercentage; i++)
            {
                int startRowIndex = i * selectRowCount + firstSelectRowCount;
                int endRowIndex = startRowIndex + selectRowCount;

                currentPageNumber++;
                pdfStamper.InsertPage(currentPageNumber, pdfReader.GetPageSize(1));

                pdfContentByte = pdfStamper.GetOverContent(currentPageNumber);

                pdfPTable.WriteSelectedRows(startRowIndex, endRowIndex, 40, pageHeight - this.DocumentBottomMargin, pdfContentByte);
            }
        }

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
            PdfPCell cell = new PdfPCell(new Phrase(text, this.font));

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
