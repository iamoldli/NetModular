using System.ComponentModel;

namespace Tm.Lib.Data.Abstractions.Enums
{
    /// <summary>
    /// 查询运算符
    /// </summary>
    public enum QueryOperator
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description(" = ")]
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description(" <> ")]
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        [Description(" > ")]
        GreaterThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(" >= ")]
        GreaterThanOrEqual,
        /// <summary>
        /// 小于
        /// </summary>
        [Description(" < ")]
        LessThan,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description(" <= ")]
        LessThanOrEqual,
        /// <summary>
        /// 包含
        /// </summary>
        [Description(" IN ")]
        In,
        /// <summary>
        /// 包含
        /// </summary>
        [Description(" NOT IN ")]
        NotIn,
    }
}
