using System;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Pdf.Abstractions;
using NetModular.Lib.Pdf.iText;
using NetModular.Lib.Utils.Core;

namespace Pdf.iText.Tests
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider SP;

        protected BaseTest()
        {
            var service = new ServiceCollection();
            service.AddNetModularServices();
            service.AddSingleton<IPdfHandler, PdfHandler>();

            SP = service.BuildServiceProvider();
        }
    }
}
