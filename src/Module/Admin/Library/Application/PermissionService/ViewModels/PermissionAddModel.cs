using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.PermissionService.ViewModels
{
    /// <summary>
    /// 新增权限
    /// </summary>
    public class PermissionAddModel
    {
        /// <summary>
        /// 所属模块编码(与请求中的Area对应)
        /// </summary>
        [Required(ErrorMessage = "请选择模块")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Required(ErrorMessage = "请选择控制器")]
        public string Controller { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 功能列表
        /// </summary>
        [Required(ErrorMessage = "请选择方法")]
        public List<KeyValuePair<string,string>> Actions { get; set; }
    }
}
