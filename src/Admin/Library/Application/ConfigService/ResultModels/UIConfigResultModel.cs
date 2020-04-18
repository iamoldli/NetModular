using NetModular.Lib.Config.Abstractions.Impl;

namespace NetModular.Module.Admin.Application.ConfigService.ResultModels
{
    /// <summary>
    /// 前端UI配置返回模型
    /// </summary>
    public class UIConfigResultModel
    {
        /// <summary>
        /// 系统信息
        /// </summary>
        public UISystem System { get; set; }

        /// <summary>
        /// 权限验证
        /// </summary>
        public UIPermission Permission { get; set; }

        /// <summary>
        /// 组件配置
        /// </summary>
        public ComponentConfig Component { get; set; }
    }

    /// <summary>
    /// UI系统信息
    /// </summary>
    public class UISystem
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 版权声明
        /// </summary>
        public string Copyright { get; set; }
    }

    /// <summary>
    /// UI权限验证
    /// </summary>
    public class UIPermission
    {
        /// <summary>
        /// 开启权限验证
        /// </summary>
        public bool Validate { get; set; }

        /// <summary>
        /// 开启按钮权限
        /// </summary>
        public bool Button { get; set; }
    }
}
