using System.Collections.Generic;
using Nm.Lib.Utils.Core.Enums;

namespace Nm.Lib.Auth.Web
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
        bool Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod);
    }
}
