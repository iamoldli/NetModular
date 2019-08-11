using System;
using System.ComponentModel.DataAnnotations;
using Nm.Lib.Data.Query;

namespace Nm.Module.Admin.Domain.Button.Models
{
    public class ButtonQueryModel : QueryModel
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        [Required(ErrorMessage = "请选择菜单")]
        public string MenuCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
