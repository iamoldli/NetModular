using System;
using System.Collections.Generic;
using Nm.Module.Admin.Domain.Menu;

namespace Nm.Module.Admin.Application.AccountService.ResultModels
{
    /// <summary>
    /// 账户登录信息
    /// </summary>
    public class LoginResultModel
    {
        /// <summary>
        /// 账户标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 皮肤设置
        /// </summary>
        public SkinConfigModel Skin { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid CID { get; set; } = Guid.Empty;

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<AccountMenuItem> Menus { get; set; }

        /// <summary>
        /// 按钮编码列表
        /// </summary>
        public IList<string> Buttons { get; set; }
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

    /// <summary>
    /// 菜单
    /// </summary>
    public class AccountMenuItem
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string RouteParams { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string RouteQuery { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        public string IconColor { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 链接菜单打开方式
        /// </summary>
        public MenuTarget Target { get; set; }

        /// <summary>
        /// 对话框宽度
        /// </summary>
        public string DialogWidth { get; set; }

        /// <summary>
        /// 对话框高度
        /// </summary>
        public string DialogHeight { get; set; }

        /// <summary>
        /// 对话框可全屏
        /// </summary>
        public bool DialogFullscreen { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<AccountMenuItem> Children { get; set; }
    }
}
