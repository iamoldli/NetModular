using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Auth.Abstractions;

namespace NetModular.Module.Admin.Application.AccountService.ViewModels
{
    public class AccountAddModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 账户是否锁定(锁定后不允许在账户管理中修改)
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 绑定角色列表
        /// </summary>
        [Required(ErrorMessage = "请选择角色")]
        public List<Guid> Roles { get; set; }
    }
}
