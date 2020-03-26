using System.Collections.Generic;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Module.Admin.Domain.Account
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
