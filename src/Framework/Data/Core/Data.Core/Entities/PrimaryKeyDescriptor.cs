using System;
using System.Reflection;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;

namespace NetModular.Lib.Data.Core.Entities
{
    public class PrimaryKeyDescriptor : IPrimaryKeyDescriptor
    {
        public string Name { get; }
        public PrimaryKeyType Type { get; }
        public PropertyInfo PropertyInfo { get; }

        public PrimaryKeyDescriptor(PropertyInfo p)
        {
            PropertyInfo = p;
            var columnAttribute = p.GetCustomAttribute<ColumnAttribute>();
            Name = columnAttribute != null ? columnAttribute.Name : p.Name;

            if (p.PropertyType.IsInt())
            {
                Type = PrimaryKeyType.Int;
            }
            else if (p.PropertyType.IsLong())
            {
                Type = PrimaryKeyType.Long;
            }
            else if (p.PropertyType.IsGuid())
            {
                Type = PrimaryKeyType.Guid;
            }
            else if (p.PropertyType.IsString())
            {
                Type = PrimaryKeyType.String;
            }
            else
            {
                throw new ArgumentException("无效的主键类型", nameof(p.PropertyType));
            }
        }

        public PrimaryKeyDescriptor()
        {
            Type = PrimaryKeyType.NoPrimaryKey;
        }

        public bool Is(PrimaryKeyType type)
        {
            return Type == type;
        }

        public bool IsNo()
        {
            return Type == PrimaryKeyType.NoPrimaryKey;
        }

        public bool IsInt()
        {
            return Type == PrimaryKeyType.Int;
        }

        public bool IsLong()
        {
            return Type == PrimaryKeyType.Long;
        }

        public bool IsGuid()
        {
            return Type == PrimaryKeyType.Guid;
        }
    }
}
