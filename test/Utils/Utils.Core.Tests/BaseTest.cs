using System;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core;

namespace Utils.Core.Tests
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider SP;

        protected BaseTest()
        {
            var service = new ServiceCollection();
            service.AddNetModularServices();
            SP = service.BuildServiceProvider();
        }
    }
}
