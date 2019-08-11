using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Module.AspNetCore
{
    public class ModuleCollection : IModuleCollection
    {
        private readonly List<IModuleDescriptor> _list = new List<IModuleDescriptor>();

        public ModuleCollection()
        {
            var assemblyHelper = new AssemblyHelper();
            var allAssembly = assemblyHelper.Load();
            foreach (var assembly in allAssembly)
            {
                var resources = assembly.GetManifestResourceNames();
                var name = resources.FirstOrDefault(m => m.EndsWith("_module.json"));
                if (name.IsNull())
                    continue;

                var stream = assembly.GetManifestResourceStream(name);
                if (stream == null)
                    continue;

                //读取流并解析配置信息
                var moduleDescriptor = JsonConvert.DeserializeObject<ModuleDescriptor>(new StreamReader(stream).ReadToEnd());
                if (moduleDescriptor != null)
                {
                    //判断是否已存在
                    if (!_list.Any(m => m.Name.Equals(moduleDescriptor.Name)))
                    {
                        //此处默认模块命名空间前缀与当前项目相同
                        var assemblyDescriptor = new ModuleAssemblyDescriptor
                        {
                            Domain = assemblyHelper.Load(m => m.Name.EndsWith("Module." + moduleDescriptor.Id + ".Domain")).FirstOrDefault(),
                            Infrastructure = assemblyHelper.Load(m => m.Name.EndsWith("Module." + moduleDescriptor.Id + ".Infrastructure")).FirstOrDefault(),
                            Application = assemblyHelper.Load(m => m.Name.EndsWith("Module." + moduleDescriptor.Id + ".Application")).FirstOrDefault(),
                            Web = assemblyHelper.Load(m => m.Name.EndsWith("Module." + moduleDescriptor.Id + ".Web")).FirstOrDefault(),
                            Quartz = assemblyHelper.Load(m => m.Name.EndsWith("Module." + moduleDescriptor.Id + ".Quartz")).FirstOrDefault()
                        };

                        Check.NotNull(assemblyDescriptor.Domain, moduleDescriptor.Id + "模块的Domain程序集未发现");
                        Check.NotNull(assemblyDescriptor.Infrastructure, moduleDescriptor.Id + "模块的Infrastructure程序集未发现");
                        Check.NotNull(assemblyDescriptor.Application, moduleDescriptor.Id + "模块的Application程序集未发现");
                        Check.NotNull(assemblyDescriptor.Web, moduleDescriptor.Id + "模块的Web程序集未发现");

                        var initializerType = assemblyDescriptor.Web.GetTypes().FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));
                        if (initializerType != null && (initializerType != typeof(IModuleInitializer)))
                        {
                            moduleDescriptor.Initializer = (IModuleInitializer)Activator.CreateInstance(initializerType);
                        }

                        moduleDescriptor.AssemblyDescriptor = assemblyDescriptor;

                        Add(moduleDescriptor);
                    }
                }
            }
        }

        public IEnumerator<IModuleDescriptor> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IModuleDescriptor item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(IModuleDescriptor item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(IModuleDescriptor[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(IModuleDescriptor item)
        {
            return _list.Remove(item);
        }

        public int Count => _list.Count;
        public bool IsReadOnly => true;

        public int IndexOf(IModuleDescriptor item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, IModuleDescriptor item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public IModuleDescriptor this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }
}
