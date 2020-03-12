using NetModular.Lib.Pdf.Abstractions;

namespace NetModular.Lib.Pdf.iText
{
    public class PdfHandler : IPdfHandler
    {
        private readonly iTextHelper _helper;

        public PdfHandler(iTextHelper helper)
        {
            _helper = helper;
        }

        public int GetPages(string filePath)
        {
            using var doc = _helper.OpenRead(filePath);
            return doc.GetNumberOfPages();
        }
    }
}
