using System;
using System.Threading.Tasks;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 登录信息详情生成器
    /// </summary>
    public interface ILoginInfoDetailsBuilder
    {
        /// <summary>
        /// 账户类型
        /// </summary>
        AccountType AccountType { get; }

        /// <summary>
        /// 生成详细信息
        /// </summary>
        /// <returns></returns>
        Task<object> Build(Guid accountId);
    }
}
