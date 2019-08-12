﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.CodeGenerator.Application.EnumItemService.ViewModels
{
    public class EnumItemUpdateModel
    {
        [Required(ErrorMessage = "请选择数据")]
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "请输入值")]
        public int Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "请输入备注")]
        public string Remarks { get; set; }
    }
}
