using System;
using System.Collections.Generic;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Module.Admin.Infrastructure.AccountPermissionResolver;

namespace NetModular.Module.Admin.Application.AuthService.ResultModels
{
    /// <summary>
    /// 认证信息
    /// </summary>
    public class AuthInfoModel
    {
        /// <summary>
        /// 账户标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
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
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 皮肤设置
        /// </summary>
        public SkinConfigModel Skin { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public IList<AccountMenuItem> Menus { get; set; }

        /// <summary>
        /// 页面编码列表
        /// </summary>
        public IList<string> Pages { get; set; }

        /// <summary>
        /// 按钮编码列表
        /// </summary>
        public IList<string> Buttons { get; set; }

        /// <summary>
        /// 详情信息(用于扩展登录对象信息)
        /// </summary>
        public object Details { get; set; }
    }

    /// <summary>
    /// 皮肤设置
    /// </summary>
    public class SkinConfigModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// 字号
        /// </summary>
        public string FontSize { get; set; }
    }
}
