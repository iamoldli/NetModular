using NetModular.Lib.Utils.Core.Attributes;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Utils.Core.Abstracts;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Cache.Abstractions
{
    [Singleton(true)]
    public class CacheKeyDescriptorCollection : CollectionAbstract<CacheKeyDescriptor>
    {
        public CacheKeyDescriptorCollection()
        {
            Init();
        }

        private void Init()
        {
            var assemblies = AssemblyHelper.Load();
            foreach (var assembly in assemblies)
            {
                var cacheKeysType = assembly.GetTypes().Where(m => m.FullName.Contains(".Infrastructure.CacheKeys"));
                foreach (var type in cacheKeysType)
                {
                    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    var moduleCode = type.FullName.Split('.')[2];

                    foreach (var field in fields)
                    {
                        var descriptor = new CacheKeyDescriptor
                        {
                            ModuleCode = moduleCode,
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
