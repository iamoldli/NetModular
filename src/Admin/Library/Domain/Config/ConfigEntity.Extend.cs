using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Module.Admin.Domain.Config
{
    public partial class ConfigEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Ignore]
        public string TypeName => Type.ToDescription();
    }
}
