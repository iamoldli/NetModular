using System;
using System.Text;
using iText.IO.Font;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
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

        public string GetFullText(string filePath)
        {
            var sb = new StringBuilder();
            using var doc = _helper.OpenRead(filePath);
            var font= doc.GetDefaultFont();
            var number = doc.GetNumberOfPages();
            for (int i = 1; i <= number; i++)
            {
                var page = doc.GetPage(i);

                sb.Append(PdfTextExtractor.GetTextFromPage(page));
            }

            return sb.ToString();
        }
    }
}
