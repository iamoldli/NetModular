using System;
using System.IO;
using NetModular.Lib.Pdf.Abstractions;
using NetModular.Lib.Pdf.iText;
using Xunit;

namespace Pdf.iText.Tests
{
    public class PdfHandlerTest
    {
        private readonly IPdfHandler _handler = new PdfHandler(new iTextHelper());
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "pdfs\\再生铜、铝、铅、锌工业污染物排放标准.pdf");

        [Fact]
        public void GetPagesTest()
        {
            var pages = _handler.GetPages(_filePath);
            Assert.Equal(21, pages);
        }

        [Fact]
        public void GetFullTextTest()
        {
            var s = _handler.GetFullText(_filePath);
        }
    }
}
