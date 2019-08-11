using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.Menu;

namespace Nm.Module.Admin.Application.MenuService.ViewModels
{
    /// <summary>
    /// 新增菜单
    /// </summary>
    public class MenuAddModel
    {
        /// <summary>
        /// 所属模块
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 路由名称
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
        /// 是否显示
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 打开方式
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
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }

        /// <summary>
        /// 按钮列表
        /// </summary>
        public List<MenuButtonAddModel> Buttons { get; set; }
    }

    /// <summary>
    /// 菜单按钮添加模型
    /// </summary>
    public class MenuButtonAddModel
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
