namespace Gible.Tech.PDF
{
    public interface IPdfExporter
    {
        /// <summary>
        /// Export a set of images to a chosen directory.
        /// </summary>
        /// <param name="baseDirectory">The directory to export a PDF to.</param>
        /// <param name="fileName">The name for the desired file without an extension.</param>
        /// <param name="filePaths">The file paths for all images being exported.</param>
        void ExportPdf(string baseDirectory, string fileName, IEnumerable<string> filePaths);
    }
}