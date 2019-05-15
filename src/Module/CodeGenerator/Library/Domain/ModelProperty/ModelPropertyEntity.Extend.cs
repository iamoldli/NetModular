using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Module.CodeGenerator.Domain.ModelProperty
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
