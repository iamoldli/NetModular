using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Quartz.Domain.JobClass;

namespace Nm.Module.Quartz.Application.JobClassService.ViewModels
{
    /// <summary>
    /// 任务类型添加模型
    /// </summary>
    public class JobClassUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的任务类型")]
        public Guid Id { get; set; }

    
    }
}
