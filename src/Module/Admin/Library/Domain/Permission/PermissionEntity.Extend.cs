﻿using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Utils.Core.Extensions;

namespace Tm.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 权限扩展属性
    /// </summary>
    public partial class PermissionEntity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Ignore]
        public string ModuleName { get; set; }

        /// <summary>
        /// 请求方法名称
        /// </summary>
        [Ignore]
        public string HttpMethodName => HttpMethod.ToDescription();

    }
}
