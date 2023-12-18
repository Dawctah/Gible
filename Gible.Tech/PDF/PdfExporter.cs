using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Text;

namespace Gible.Tech.PDF
{
    public class PdfExporter : IPdfExporter
    {
        /// <inheritdoc>
        public void ExportPdf(string baseDirectory, string fileName, IEnumerable<string> filePaths)
        {
            var files = new List<FileStream>();
            foreach (var file in filePaths)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                files.Add(File.OpenRead(file));
            }

            var document = new PdfDocument();
            foreach (var file in files)
            {
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var image = XImage.FromStream(file);

                page.Width = image.PointWidth;
                page.Height = image.PointHeight;

                gfx.DrawImage(image, 0, 0, page.Width, page.Height);
            }

            document.Save($"{baseDirectory}\\{fileName}.pdf");
        }
    }
}