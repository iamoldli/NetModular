using Microsoft.AspNetCore.Mvc.Filters;

namespace Nm.Lib.Auth.Abstractions
{
    /// <summary>
    /// 权限验证处理接口
    /// </summary>
    public interface IPermissionValidateHandler
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        bool Validate(AuthorizationFilterContext context);
    }
}
