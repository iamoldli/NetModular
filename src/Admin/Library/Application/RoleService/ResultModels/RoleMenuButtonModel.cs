using System;

namespace NetModular.Module.Admin.Application.RoleService.ResultModels
{
    /// <summary>
    /// 角色菜单的按钮信息
    /// </summary>
    public class RoleMenuButtonModel
    {
        /// <summary>
        /// 按钮编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}
