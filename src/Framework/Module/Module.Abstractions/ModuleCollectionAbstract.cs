using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Utils.Core;

namespace NetModular.Lib.Module.Abstractions
{
    public abstract class ModuleCollectionAbstract : CollectionAbstract<IModuleDescriptor>, IModuleCollection
    {
        /// <summary>
        /// 加载数据库脚本信息
        /// </summary>
        protected void LoadInitDataScript(IModuleDescriptor moduleDescriptor, DirectoryInfo moduleDir)
        {
            var descriptor = new ModuleInitDataScriptDescriptor();
            var sqlServerPath = Path.Combine(moduleDir.FullName, "InitData\\SqlServer.sql");
            if (File.Exists(sqlServerPath))
            {
                descriptor.SqlServer = sqlServerPath;
            }
            var mySqlPath = Path.Combine(moduleDir.FullName, "InitData\\MySql.sql");
            if (File.Exists(mySqlPath))
            {
                descriptor.MySql = mySqlPath;
            }
            var sqlitePath = Path.Combine(moduleDir.FullName, "InitData\\SQLite.sql");
            if (File.Exists(sqlitePath))
            {
                descriptor.SQLite = sqlitePath;
            }
            var pgPath = Path.Combine(moduleDir.FullName, "InitData\\PostgreSQL.sql");
            if (File.Exists(pgPath))
            {
                descriptor.PostgreSQL = pgPath;
            }
            var oraclePath = Path.Combine(moduleDir.FullName, "InitData\\Oracle.sql");
            if (File.Exists(oraclePath))
            {
                descriptor.Oracle = oraclePath;
            }

            moduleDescriptor.InitDataScriptDescriptor = descriptor;
        }

        /// <summary>
        /// 加载服务配置器
        /// </summary>
        /// <param name="moduleDescriptor"></param>
        protected void LoadServicesConfigurator(IModuleDescriptor moduleDescriptor)
        {
            var servicesConfiguratorType = moduleDescriptor.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(t => typeof(IModuleServicesConfigurator).IsAssignableFrom(t));
            if (servicesConfiguratorType != null && (servicesConfiguratorType != typeof(IModuleServicesConfigurator)))
            {
                moduleDescriptor.ServicesConfigurator = (IModuleServicesConfigurator)Activator.CreateInstance(servicesConfiguratorType);
            }
        }

        /// <summary>
        /// 加载枚举信息
        /// </summary>
        /// <param name="moduleDescriptor"></param>
        protected void LoadEnumDescriptors(IModuleDescriptor moduleDescriptor)
        {
            if (moduleDescriptor.AssemblyDescriptor.Domain != null)
            {
                LoadEnumDescriptors(moduleDescriptor, moduleDescriptor.AssemblyDescriptor.Domain, "Domain");
            }
            if (moduleDescriptor.AssemblyDescriptor.Infrastructure != null)
            {
                LoadEnumDescriptors(moduleDescriptor, moduleDescriptor.AssemblyDescriptor.Infrastructure, "Infrastructure");
            }
            if (moduleDescriptor.AssemblyDescriptor.Application != null)
            {
                LoadEnumDescriptors(moduleDescriptor, moduleDescriptor.AssemblyDescriptor.Application, "Application");
            }
        }

        /// <summary>
        /// 加载枚举信息
        /// </summary>
        /// <param name="moduleDescriptor"></param>
        /// <param name="assembly"></param>
        /// <param name="libraryName"></param>
        private void LoadEnumDescriptors(IModuleDescriptor moduleDescriptor, Assembly assembly, string libraryName)
        {
            var enumTypes = assembly.GetTypes().Where(m => m.IsEnum);
            foreach (var enumType in enumTypes)
            {
                var enumDescriptor = new ModuleEnumDescriptor
                {
                    LibraryName = libraryName,
                    Name = enumType.Name,
                    Type = enumType,
                    Options = Enum.GetValues(enumType).Cast<Enum>().Where(m => !m.ToString().EqualsIgnoreCase("UnKnown")).Select(x => new OptionResultModel
                    {
                        Label = x.ToDescription(),
                        Value = x
                    }).ToList()
                };

                moduleDescriptor.EnumDescriptors.Add(enumDescriptor);
            }
        }

        /// <summary>
        /// 检测程序集
        /// </summary>
        /// <param name="moduleDescriptor"></param>
        /// <param name="assemblyDescriptor"></param>
        protected void CheckAssemblyDescriptor(IModuleDescriptor moduleDescriptor, IModuleAssemblyDescriptor assemblyDescriptor)
        {
            Check.NotNull(assemblyDescriptor.Domain, $"{moduleDescriptor.Name}({moduleDescriptor.Id})模块的Domain程序集未发现");
            Check.NotNull(assemblyDescriptor.Infrastructure, $"{moduleDescriptor.Name}({moduleDescriptor.Id})模块的Infrastructure程序集未发现");
            Check.NotNull(assemblyDescriptor.Application, $"{moduleDescriptor.Name}({moduleDescriptor.Id})模块的Application程序集未发现");
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="moduleDir"></param>
        /// <param name="jsonReader"></param>
        protected abstract void LoadDescriptor(DirectoryInfo moduleDir, StreamReader jsonReader);

        public void Load()
        {
            Collection.Clear();

            var modulesRootDirPath = Path.Combine(AppContext.BaseDirectory, "_modules");
            if (!Directory.Exists(modulesRootDirPath))
                return;

            var modulesRootDir = new DirectoryInfo(modulesRootDirPath);
            var moduleDirs = modulesRootDir.GetDirectories();
            if (!moduleDirs.Any())
                return;

            foreach (var moduleDir in moduleDirs)
            {
                //从_module.json文件中读取模块信息
                var jsonPath = Path.Combine(moduleDir.FullName, "_module.json");
                if (!File.Exists(jsonPath))
                    continue;

                using var jsonReader = new StreamReader(jsonPath);
                LoadDescriptor(moduleDir, jsonReader);
            }
        }
    }
}
