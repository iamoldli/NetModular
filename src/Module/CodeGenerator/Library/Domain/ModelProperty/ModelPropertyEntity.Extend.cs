using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Module.CodeGenerator.Domain.ModelProperty
{
    public partial class ModelPropertyEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Ignore]
        public string TypeName => Type.ToDescription();

        /// <summary>
        /// 枚举名称
        /// </summary>
        [Ignore]
        public string EnumName { get; set; }
    }
}
