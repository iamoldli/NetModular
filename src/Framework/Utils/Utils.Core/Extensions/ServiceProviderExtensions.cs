using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// IServiceProvider扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 根据指定名称开头获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetStartWith<T>(this IServiceProvider sp, string name) where T : class
        {
            var services = sp.GetServices<T>();
            return services.FirstOrDefault(m => m.GetType().Name.ToString().StartsWith(name));
        }

        /// <summary>
        /// 根据指定名称结尾获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetEndsWith<T>(this IServiceProvider sp, string name) where T : class
        {
            var services = sp.GetServices<T>();
            return services.FirstOrDefault(m => m.GetType().Name.ToString().EndsWith(name));
        }
    }
}
