using System;
using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.Admin.Application.ButtonService.ViewModels
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
