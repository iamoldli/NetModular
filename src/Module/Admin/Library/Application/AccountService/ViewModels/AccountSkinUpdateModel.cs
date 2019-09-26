namespace Nm.Module.Admin.Application.AccountService.ViewModels
{
    /// <summary>
    /// 账户皮肤配置信息修改
    /// </summary>
    public class AccountSkinUpdateModel
    {
        /// <summary>
        /// 皮肤名称
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
