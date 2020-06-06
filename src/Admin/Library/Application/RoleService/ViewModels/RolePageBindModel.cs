using System;
using System.Collections.Generic;
using NetModular.Lib.Utils.Core.Validations;

namespace NetModular.Module.Admin.Application.RoleService.ViewModels
{
    /// <summary>
    /// 角色页面绑定模型
    /// </summary>
    public class RolePageBindModel
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        [NotEmpty(ErrorMessage = "请选择角色")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 页面列表
        /// </summary>
        public List<PageBindModel> Pages { get; set; } = new List<PageBindModel>();
    }

    /// <summary>
    /// 页面绑定模型
    /// </summary>
    public class PageBindModel
    {
        /// <summary>
        /// 页面编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 按钮列表(按钮编码)
        /// </summary>
        public List<string> Buttons { get; set; } = new List<string>();

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
