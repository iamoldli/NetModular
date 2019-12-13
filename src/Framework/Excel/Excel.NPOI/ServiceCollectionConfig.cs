using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Excel.Abstractions;

namespace NetModular.Lib.Excel.NPOI
{
    public class ServiceCollectionConfig : IServiceCollectionConfig
    {
        public IServiceCollection Config(IServiceCollection services, ExcelOptions options)
        {
            services.AddSingleton<IExcelExportHandler, NPOIExcelExportHandler>();
            return services;
        }
    }
}
