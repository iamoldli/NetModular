using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Nm.Lib.Utils.Core.Attributes;

namespace Nm.Lib.Utils.Core.Helpers
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
        public List<Assembly> Load(Func<CompilationLibrary, bool> predicate = null)
        {
            if (predicate == null)
                return DependencyContext.Default.CompileLibraries.Select(m => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(m.Name))).ToList();

            return DependencyContext.Default.CompileLibraries.Where(predicate).Select(m => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(m.Name))).ToList();
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
