using System;
using System.ComponentModel.DataAnnotations;
using NetModular.Module.Admin.Domain.Tenant;

namespace NetModular.Module.Admin.Application.TenantService.ViewModels
{
    /// <summary>
    /// 租户添加模型
    /// </summary>
    public class TenantUpdateModel : TenantAddModel
    {
        [Required(ErrorMessage = "请选择要修改的租户")]
        public Guid Id { get; set; }
    }
}
