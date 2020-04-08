using System.Collections.Generic;
using System.Linq;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Module.AspNetCore;

namespace NetModular.Lib.Swagger.Core.Extensions
{
    internal static class ModuleDescriptorExtensions
    {
        /// <summary>
        /// 获取分组
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetGroups(this IModuleDescriptor descriptor)
        {
            var groups = new Dictionary<string, string> { { descriptor.Code, descriptor.Name } };

            var assemblies = (ModuleAssemblyDescriptor)descriptor.AssemblyDescriptor;
            var controllers = (assemblies.Web ?? assemblies.Api).GetTypes().Where(m => m.Name.EndsWith("Controller"));
            foreach (var controller in controllers)
            {
                var arr = controller.FullName.Split('.');
                if (arr.Length == 7)
                {
                    var dic = arr[5];
                    if (dic != "Controllers" && !groups.ContainsKey(dic))
                    {
                        groups.Add(descriptor.Code + "_" + dic, descriptor.Name + "_" + dic);
                    }
                }
            }

            return groups;
        }
    }
}
