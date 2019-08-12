using System.Collections.Generic;
using Tm.Lib.Utils.Core.Result;

namespace Tm.Module.Admin.Domain.Account
{
    public partial class AccountEntity
    {
        /// <summary>
        /// 关联角色
        /// </summary>
        public List<OptionResultModel> Roles { get; set; }
    }
}
