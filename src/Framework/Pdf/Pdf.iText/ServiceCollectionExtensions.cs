using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.Pdf.iText
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加iText
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static IServiceCollection AddPdf(this IServiceCollection services, string environmentName)
        {
            return services;
        }
    }
}
