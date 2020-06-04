using System.Reflection;

namespace NetModular.Lib.Data.Abstractions.Entities
{
    public interface IColumnDescriptor
    {
        /// <summary>
        /// 列名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 实体属性信息
        /// </summary>
        PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// 是否主键
        /// </summary>
        bool IsPrimaryKey { get; }

        /// <summary>
        /// 长度
        /// </summary>
        int Length { get; }

        /// <summary>
        /// 最大值
        /// </summary>
        bool Max { get; }

        /// <summary>
        /// 可空的
        /// </summary>
        bool Nullable { get; }

        /// <summary>
        /// 精度位数
        /// </summary>
        int PrecisionM { get; }

        /// <summary>
        /// 精度小数
        /// </summary>
        int PrecisionD { get; }

        /// <summary>
        /// 字段类型名称
        /// </summary>
        string TypeName { get; }
    }
}
