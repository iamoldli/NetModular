using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.Admin.Application.RoleService.ViewModels
{
    public class RoleAddModel
    {
        [Required(ErrorMessage = "请输入角色名称")]
        public string Name { get; set; }

        public string Remarks { get; set; }

        public int Sort { get; set; }
        /// <summary>
        /// 归属公司
        /// </summary>
        public Guid CID { get; set; }
    }
}
