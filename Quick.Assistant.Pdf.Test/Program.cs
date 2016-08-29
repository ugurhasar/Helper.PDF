using System.IO;
using System.Reflection;
using System.Text;

namespace Quick.Assistant.Pdf.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string html = File.ReadAllText(string.Concat(currentLocation, "\\template\\index.html"));
            string style = File.ReadAllText(string.Concat(currentLocation, "/template/style.css"));

            byte[] bytes = PDFHelper.Write(html, style, Encoding.UTF8, 20, 20, 20, 20);

            string fileName = string.Concat(currentLocation, "\\pdf_test.pdf");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllBytes(fileName, bytes);
        }
    }
}
