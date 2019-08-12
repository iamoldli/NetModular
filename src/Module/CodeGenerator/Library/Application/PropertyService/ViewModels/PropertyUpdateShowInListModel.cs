using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.CodeGenerator.Application.PropertyService.ViewModels
{
    public class PropertyUpdateShowInListModel
    {
        [Required(ErrorMessage = "请选择属性")]
        public Guid Id { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool ShowInList { get; set; }
    }
}
