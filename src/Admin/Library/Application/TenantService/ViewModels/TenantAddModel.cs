using System;
using System.ComponentModel.DataAnnotations;
using NetModular.Module.Admin.Domain.Tenant;

namespace NetModular.Module.Admin.Application.TenantService.ViewModels
{
    /// <summary>
    /// 租户添加模型
    /// </summary>
    public class TenantAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

    }
}
