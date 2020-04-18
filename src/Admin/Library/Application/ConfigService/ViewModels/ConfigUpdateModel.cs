using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Module.Admin.Application.ConfigService.ViewModels
{
    public class ConfigUpdateModel
    {
        [Required(ErrorMessage = "配置类编码不能为空")]
        public string Code { get; set; }

        public ConfigType Type { get; set; }

        [Required(ErrorMessage = "配置内容不能为空")]
        public string Json { get; set; }
    }
}
