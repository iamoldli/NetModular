using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Nm.Module.Common.Application.AttachmentService.ViewModels
{
    /// <summary>
    /// 附件上传模型
    /// </summary>
    public class AttachmentUploadModel
    {
        public IFormFile File { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        [Required(ErrorMessage = "所属模块不能为空")]
        public string Module { get; set; }

        /// <summary>
        /// 所属分组
        /// </summary>
        [Required(ErrorMessage = "所属分组不能为空")]
        public string Group { get; set; }

        /// <summary>
        /// 是否需要授权访问
        /// </summary>
        public bool Auth { get; set; }
    }
}
