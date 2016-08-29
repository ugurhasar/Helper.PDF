using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.IO;
using System.Text;

namespace Quick.Assistant.Pdf
{
    public static class PDFHelper
    {
        public static byte[] Write(string template, string style, float marginLeft = 0, float marginRight = 0, float marginTop = 0, float marginBottom = 0)
        {
            return Write(template, style, Encoding.UTF8, marginLeft, marginRight, marginTop, marginBottom);
        }

        public static byte[] Write(string template, string style, Encoding encoding, float marginLeft = 0, float marginRight = 0, float marginTop = 0, float marginBottom = 0)
        {
            byte[] bytes = null;

            if (string.IsNullOrWhiteSpace(template))
            {
                throw new Exception("template is null.");
            }

            using (var memoryStream = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4, marginLeft, marginRight, marginTop, marginBottom))
                {
                    using (var pdfWriter = PdfWriter.GetInstance(document, memoryStream))
                    {
                        document.Open();

                        using (var css = new MemoryStream(encoding.GetBytes(style)))
                        {
                            using (var html = new MemoryStream(encoding.GetBytes(template)))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, document, html, css);
                            }
                        }
                        document.Close();

                    }
                }
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
