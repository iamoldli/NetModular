using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 权限扩展属性
    /// </summary>
    public partial class Permission
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Ignore]
        public string ModuleName { get; set; }

    }
}
