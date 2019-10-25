using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.AccountService.ViewModels
{
    public class AccountUpdateModel
    {
        [Required(ErrorMessage = "请选择账户")]
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 绑定角色列表
        /// </summary>
        [Required(ErrorMessage = "请选择角色")]
        public IList<Guid> Roles { get; set; }
    }
}
