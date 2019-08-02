using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Quartz.Domain.Job;

namespace Nm.Module.Quartz.Application.JobService.ViewModels
{
    /// <summary>
    /// 任务添加模型
    /// </summary>
    public class JobUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的任务")]
        public Guid Id { get; set; }

    
    }
}
