using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.Quartz.Application.JobService.ViewModels
{
    public class JobUpdateModel : JobAddModel
    {
        [Required(ErrorMessage = "请选择任务")]
        public Guid Id { get; set; }
    }
}
