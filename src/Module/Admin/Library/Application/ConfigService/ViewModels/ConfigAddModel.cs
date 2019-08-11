using System.ComponentModel.DataAnnotations;

namespace Nm.Module.Admin.Application.ConfigService.ViewModels
{
    public class ConfigAddModel
    {
        [Required(ErrorMessage = "请输入Key")]
        public string Key { get; set; }

        [Required(ErrorMessage = "请输入值")]
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
