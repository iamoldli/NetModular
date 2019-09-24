using System;

namespace Nm.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 属性长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : Attribute
    {
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 固定长度
        /// </summary>
        /// <param name="length"></param>
        public LengthAttribute(int length = 50)
        {
            Length = length;
        }
    }
}
