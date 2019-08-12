using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.CodeGenerator.Application.ClassService.ViewModels
{
    public class ClassUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的类")]
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [Required(ErrorMessage = "请输入表名")]
        public string TableName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Required(ErrorMessage = "请输入类名")]
        public string Remarks { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public ClassMethodModel Method { get; set; }
    }
}
