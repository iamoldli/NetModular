using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Excel.Abstractions;

namespace NetModular.Lib.Excel.EPPlus
{
    public class ServiceCollectionConfig : IServiceCollectionConfig
    {
        public IServiceCollection Config(IServiceCollection services, ExcelOptions options)
        {
            services.AddSingleton<IExcelExportHandler, EPPlusExcelExportHandler>();
            services.AddSingleton<IExcelHandler, EPPlusExcelHandler>();
            return services;
        }
    }
}
