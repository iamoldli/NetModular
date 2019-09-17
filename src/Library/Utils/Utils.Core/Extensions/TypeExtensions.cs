using System;
using System.Linq;
using System.Reflection;

namespace Nm.Lib.Utils.Core.Extensions
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断属性是否是静态的
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsStatic(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod).IsStatic;

        /// <summary>
        /// 判断指定类型是否实现于该类型
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementType"></param>
        /// <returns></returns>
        public static bool IsImplementType(this Type serviceType, Type implementType)
        {
            //泛型
            if (serviceType.IsGenericType)
            {
                if (serviceType.IsInterface)
                {
                    var interfaces = implementType.GetInterfaces();
                    if (interfaces.Any(m => m.IsGenericType && m.GetGenericTypeDefinition() == serviceType))
                    {
                        return true;
                    }
                }
                else
                {
                    if (implementType.BaseType != null && implementType.BaseType.IsGenericType && implementType.BaseType.GetGenericTypeDefinition() == serviceType)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (serviceType.IsInterface)
                {
                    var interfaces = implementType.GetInterfaces();
                    if (interfaces.Any(m => m == serviceType))
                        return true;
                }
                else
                {
                    if (implementType.BaseType != null && implementType.BaseType == serviceType)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否继承自指定的泛型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="generic"></param>
        /// <returns></returns>
        public static bool IsSubclassOfGeneric(this Type type, Type generic)
        {
            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (generic == cur)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        /// <summary>
        /// 判断是否可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
