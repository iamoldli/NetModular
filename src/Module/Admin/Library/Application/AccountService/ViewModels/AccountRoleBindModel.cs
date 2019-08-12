using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.Admin.Application.AccountService.ViewModels
{
    /// <summary>
    /// 账户角色绑定模型
    /// </summary>
    public class AccountRoleBindModel
    {
        [Required(ErrorMessage = "请选择账户")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Required(ErrorMessage = "请选择角色")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}
