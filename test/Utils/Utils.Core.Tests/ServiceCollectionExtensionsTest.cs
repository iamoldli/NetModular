using Microsoft.Extensions.DependencyInjection;
using Utils.Core.Tests.Log;
using Xunit;

namespace Utils.Core.Tests
{
    public class ServiceCollectionExtensionsTest : BaseTest
    {
        private readonly ILog _log;

        public ServiceCollectionExtensionsTest()
        {
            _log = SP.GetService<ILog>();
        }

        [Fact]
        public void AddSingletonServicesTest()
        {
            var txt = _log.Debug();

            Assert.Equal("TextLog", txt);
        }
    }
}
