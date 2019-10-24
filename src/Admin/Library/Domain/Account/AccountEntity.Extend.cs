using System.Collections.Generic;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Result;

namespace Nm.Module.Admin.Domain.Account
{
    public partial class AccountEntity
    {
        /// <summary>
        /// 关联角色
        /// </summary>
        [Ignore]
        public List<OptionResultModel> Roles { get; set; }

        /// <summary>
        /// 账户类型名称
        /// </summary>
        [Ignore]
        public string TypeName => Type.ToDescription();
    }
}
