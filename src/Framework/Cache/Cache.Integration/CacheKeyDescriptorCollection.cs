using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Abstracts;

namespace NetModular.Lib.Cache.Integration
{
    public class CacheKeyDescriptorCollection : CollectionAbstract<CacheKeyDescriptor>, ICacheKeyDescriptorCollection
    {
        private readonly IModuleCollection _moduleCollection;

        public CacheKeyDescriptorCollection(IModuleCollection moduleCollection)
        {
            _moduleCollection = moduleCollection;
            Init();
        }

        public IEnumerable<CacheKeyDescriptor> GetByModule(string moduleCode)
        {
            if (moduleCode.IsNull())
                return new List<CacheKeyDescriptor>();

            return Collection.Where(m => m.ModuleCode.EqualsIgnoreCase(moduleCode));
        }

        private void Init()
        {
            foreach (var module in _moduleCollection)
            {
                var cacheKeys = module.AssemblyDescriptor.Infrastructure.GetTypes()
                    .FirstOrDefault(m => m.Name.EqualsIgnoreCase("CacheKeys"));
                if (cacheKeys != null)
                {
                    var fields = cacheKeys.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    foreach (var field in fields)
                    {
                        var descriptor = new CacheKeyDescriptor
                        {
                            ModuleCode = module.Id,
                            Name = field.GetRawConstantValue().ToString(),
                            Desc = field.Name
                        };

                        var descAttr = field.GetCustomAttributes().FirstOrDefault(m =>
                            m.GetType().IsAssignableFrom(typeof(DescriptionAttribute)));
                        if (descAttr != null)
                        {
                            descriptor.Desc = ((DescriptionAttribute)descAttr).Description;
                        }

                        Collection.Add(descriptor);
                    }
                }
            }
        }
    }
}
