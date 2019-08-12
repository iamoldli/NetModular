using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Tm.Lib.Utils.Core.Attributes;

namespace Tm.Lib.Utils.Core.Helpers
{
    /// <summary>
    /// 程序集操作帮助类
    /// </summary>
    [Singleton]
    public class AssemblyHelper
    {
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <returns></returns>
        public List<Assembly> Load(Func<RuntimeLibrary, bool> predicate = null)
        {
            var list = DependencyContext.Default.RuntimeLibraries.ToList();
            if (predicate != null)
                list = DependencyContext.Default.RuntimeLibraries.Where(predicate).ToList();

            return list.Select(m =>
            {
                try
                {
                    return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(m.Name));
                }
                catch
                {
                    return null;
                }
            }).Where(m => m != null).ToList();
        }

        /// <summary>
        /// 获取当前程序集的名称
        /// </summary>
        /// <returns></returns>
        public string GetCurrentAssemblyName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }
    }
}
