using System.Linq;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core.Extensions
{
    internal static class EntityDescriptorExtension
    {
        /// <summary>
        /// 获取是否删除属性对应字段名称
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static string GetDeletedColumnName(this IEntityDescriptor descriptor)
        {
            return GetColumnNameForSoftDelete(descriptor, "Deleted");
        }

        /// <summary>
        /// 获取删除时间属性对应字段名称
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static string GetDeletedTimeColumnName(this IEntityDescriptor descriptor)
        {
            return GetColumnNameForSoftDelete(descriptor, "DeletedTime");
        }

        /// <summary>
        /// 获取删除人属性对应字段名称
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static string GetDeletedByColumnName(this IEntityDescriptor descriptor)
        {
            return GetColumnNameForSoftDelete(descriptor, "DeletedBy");
        }

        /// <summary>
        /// 获取租户属性对应字段名称
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static string GetTenantByColumnName(this IEntityDescriptor descriptor)
        {
            return descriptor.Columns.First(m => m.PropertyInfo.Name.Equals("TenantId")).Name;
        }
        
        /// <summary>
        /// 获取修改人属性对应字段名称
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static string GetModifiedByColumnName(this IEntityDescriptor descriptor)
        {
            return descriptor.Columns.First(m => m.PropertyInfo.Name.Equals("ModifiedBy")).Name;
        }

        /// <summary>
        /// 获取修改时间属性对应字段名称
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static string GetModifiedTimeColumnName(this IEntityDescriptor descriptor)
        {
            return descriptor.Columns.First(m => m.PropertyInfo.Name.Equals("ModifiedTime")).Name;
        }

        private static string GetColumnNameForSoftDelete(IEntityDescriptor descriptor, string propertyName)
        {
            if (descriptor == null || !descriptor.IsSoftDelete)
                return string.Empty;

            return descriptor.Columns.First(m => m.PropertyInfo.Name.Equals(propertyName)).Name;
        }

    }
}
