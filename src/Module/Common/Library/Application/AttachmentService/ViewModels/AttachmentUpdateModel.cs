using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Common.Domain.Attachment;

namespace Nm.Module.Common.Application.AttachmentService.ViewModels
{
    /// <summary>
    /// 附件表添加模型
    /// </summary>
    public class AttachmentUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的附件表")]
        public Guid Id { get; set; }

    
    }
}
