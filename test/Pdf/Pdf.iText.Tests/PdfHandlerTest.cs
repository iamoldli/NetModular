using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Pdf.Abstractions;
using Xunit;

namespace Pdf.iText.Tests
{
    public class PdfHandlerTest : BaseTest
    {
        private readonly IPdfHandler _handler;
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\pdfs\\test.pdf");

        public PdfHandlerTest()
        {
            _handler = SP.GetService<IPdfHandler>();
        }

        [Fact]
        public void GetPagesTest()
        {
           var s= Type.GetTypeCode(typeof(Guid)) == TypeCode.Object;

            var pages = _handler.GetPages(_filePath);
            Assert.Equal(21, pages);
        }
    }
}
