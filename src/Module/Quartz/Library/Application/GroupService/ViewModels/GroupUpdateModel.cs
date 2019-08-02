using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Quartz.Domain.Group;

namespace Nm.Module.Quartz.Application.GroupService.ViewModels
{
    /// <summary>
    /// 任务组添加模型
    /// </summary>
    public class GroupUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的任务组")]
        public Guid Id { get; set; }

    
    }
}
