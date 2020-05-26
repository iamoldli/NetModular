using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Pdf.Abstractions;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Pdf.Integration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Excel功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection AddPdf(this IServiceCollection services, IConfiguration cfg)
        {
            var config = new PdfConfig();
            var section = cfg.GetSection("Pdf");
            if (section != null)
            {
                section.Bind(config);
            }

            services.AddSingleton(config);

            var assembly = AssemblyHelper.LoadByNameEndString($".Lib.Pdf.{config.Provider.ToString()}");

            Check.NotNull(assembly, $"Pdf实现程序集({config.Provider.ToString()})未找到，请安装 NetModular.Lib.Pdf.{config.Provider.ToString()} 包");

            var handlerType = assembly.GetTypes().FirstOrDefault(m => m.Name.EndsWith("PdfHandler"));
            if (handlerType != null)
            {
                services.AddSingleton(typeof(IPdfHandler), handlerType);
            }

            return services;
        }
    }
}