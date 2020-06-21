using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Lib.Auth.Abstractions
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
        Task<bool> Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod);
    }
}
