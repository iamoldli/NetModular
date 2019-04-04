using System;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.AccountConfig
{
    /// <summary>
    /// 账户配置
    /// </summary>
    public class AccountConfig : Entity
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 皮肤配置(json字符串)
        /// </summary>
        public string Skin { get; set; }
    }
}
