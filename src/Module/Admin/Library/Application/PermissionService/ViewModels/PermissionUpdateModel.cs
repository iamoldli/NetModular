using System;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.PermissionService.ViewModels
{
    public class PermissionUpdateModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required(ErrorMessage = "请选择要修改的权限")]
        public Guid Id { get; set; }

        /// <summary>
        /// 所属模块编码(与请求中的Area对应)
        /// </summary>
        [Required(ErrorMessage = "请选择模块")]
        public string ModuleCode { get; set; }


        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Required(ErrorMessage = "请选择控制器")]
        public string Controller { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Action { get; set; }
    }
}
