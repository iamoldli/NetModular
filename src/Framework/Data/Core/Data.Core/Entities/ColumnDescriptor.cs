using System;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core.Entities
{
    public class ColumnDescriptor : IColumnDescriptor
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 列类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; }

        public int Length { get; }

        public bool Max { get; }

        public bool Nullable { get; }

        public int PrecisionM { get; }

        public int PrecisionD { get; }

        public ColumnDescriptor(PropertyInfo property, ISqlAdapter sqlAdapter)
        {
            if (property == null)
                return;

            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
            if (columnAttribute != null)
            {
                Name = columnAttribute.Name;
                TypeName = columnAttribute.TypeName;
                DefaultValue = columnAttribute.DefaultValue;
            }

            if (Name.IsNull())
                Name = property.Name;

            PropertyInfo = property;

            IsPrimaryKey = Attribute.GetCustomAttributes(property).Any(attr => attr.GetType() == typeof(KeyAttribute));

            if (!IsPrimaryKey)
            {
                IsPrimaryKey = property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase);
            }

            var lengthAtt = property.GetCustomAttribute<LengthAttribute>();
            if (lengthAtt != null)
            {
                Length = lengthAtt.Length < 1 ? 50 : lengthAtt.Length;
            }

            var maxAtt = property.GetCustomAttribute<MaxAttribute>();
            Max = maxAtt != null;

            if (property.PropertyType.IsNullable())
            {
                Nullable = true;
            }
            else
            {
                var nullableAtt = property.GetCustomAttribute<NullableAttribute>();
                Nullable = nullableAtt != null;
            }

            var precisionAtt = property.GetCustomAttribute<PrecisionAttribute>();
            if (precisionAtt != null)
            {
                PrecisionM = precisionAtt.M;
                PrecisionD = precisionAtt.D;
            }

            //解析列类型名称和默认值
            var typeName = sqlAdapter.GetColumnTypeName(this, out string defaultValue);
            if (TypeName.IsNull())
                TypeName = typeName;
            if (DefaultValue.IsNull())
                DefaultValue = defaultValue;
        }
    }
}
