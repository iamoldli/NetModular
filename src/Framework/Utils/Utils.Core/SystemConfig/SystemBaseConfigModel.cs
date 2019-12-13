using System.ComponentModel.DataAnnotations;

namespace NetModular.Lib.Utils.Core.SystemConfig
{
    /// <summary>
    /// 系统基础配置信息
    /// </summary>
    public class SystemBaseConfigModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "请输入系统标题")]
        [ConfigDescription("sys_title", "系统标题")]
        public string Title { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [ConfigDescription("sys_logo", "系统Logo")]
        public string Logo { get; set; }

        /// <summary>
        /// 首页地址
        /// </summary>
        [ConfigDescription("sys_home", "系统首页地址")]
        public string Home { get; set; }

        /// <summary>
        /// 个人信息页
        /// </summary>
        [ConfigDescription("sys_userinfo_page", "个人信息页")]
        public string UserInfoPage { get; set; }

        /// <summary>
        /// 版权声明
        /// </summary>
        [ConfigDescription("sys_copyright", "版权声明")]
        public string Copyright { get; set; }
    }
}
