using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Module.Admin.Application.FileService.ViewModels
{
    /// <summary>
    /// 文件上传模型
    /// </summary>
    public class FileUploadModel
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [Required(ErrorMessage = "请指定模块编码")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [Required(ErrorMessage = "请指定分组")]
        public string Group { get; set; }

        /// <summary>
        /// 访问方式
        /// </summary>
        public FileAccessMode AccessMode { get; set; }

        /// <summary>
        /// 授权账户，仅当访问方式未Auth时生效
        /// </summary>
        public string Accounts { get; set; }

        /// <summary>
        /// 文件本地物理路径
        /// </summary>
        [IgnoreProperty]
        public string PhysicalPath { get; set; }
    }
}
