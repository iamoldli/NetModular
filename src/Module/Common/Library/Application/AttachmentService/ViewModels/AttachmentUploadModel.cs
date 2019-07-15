using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Nm.Module.Common.Application.AttachmentService.ViewModels
{
    /// <summary>
    /// 附件上传模型
    /// </summary>
    public class AttachmentUploadModel
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        [JsonIgnore]
        public Guid AccountId { get; set; }

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
