using System;
using System.ComponentModel.DataAnnotations;
using Nm.Lib.Data.Query;

namespace Nm.Module.Admin.Domain.Button.Models
{
    public class ButtonQueryModel : QueryModel
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [Required(ErrorMessage = "请选择菜单")]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
