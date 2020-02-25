using System.IO;
using System.Linq;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Helpers;
using Newtonsoft.Json;

namespace NetModular.Lib.Module.GenericHost
{
    public class ModuleCollection : ModuleCollectionAbstract
    {
        /// <summary>
        /// 加载程序集信息
        /// </summary>
        private void LoadAssemblyDescriptor(ModuleDescriptor moduleDescriptor)
        {
            //此处默认模块命名空间前缀与当前项目相同
            var assemblyDescriptor = new ModuleAssemblyDescriptor
            {
                Domain = AssemblyHelper.LoadByNameEndString($"Module.{moduleDescriptor.Id}.Domain"),
                Infrastructure = AssemblyHelper.LoadByNameEndString($"Module.{moduleDescriptor.Id}.Infrastructure"),
                Application = AssemblyHelper.LoadByNameEndString($"Module.{moduleDescriptor.Id}.Application"),
                Quartz = AssemblyHelper.LoadByNameEndString($"Module.{moduleDescriptor.Id}.Quartz")
            };

            CheckAssemblyDescriptor(moduleDescriptor, assemblyDescriptor);

            moduleDescriptor.AssemblyDescriptor = assemblyDescriptor;
        }

        protected override void LoadDescriptor(DirectoryInfo moduleDir, StreamReader jsonReader)
        {
            var moduleDescriptor = JsonConvert.DeserializeObject<ModuleDescriptor>(jsonReader.ReadToEnd());
            if (moduleDescriptor != null)
            {
                //判断是否已存在
                if (!Collection.Any(m => m.Name.Equals(moduleDescriptor.Name)))
                {
                    //加载程序集信息
                    LoadAssemblyDescriptor(moduleDescriptor);

                    //加枚举信息
                    LoadEnumDescriptors(moduleDescriptor);

                    //加载服务配置器
                    LoadServicesConfigurator(moduleDescriptor);

                    //加载初始化数据脚本信息
                    LoadInitDataScript(moduleDescriptor, moduleDir);

                    Add(moduleDescriptor);
                }
            }
        }
    }
}
