using System.Collections.Generic;
using Nm.Lib.Utils.Core.Result;

namespace Nm.Module.Admin.Domain.Account
{
    public partial class AccountEntity
    {

        /// <summary>
        /// 关联角色
        /// </summary>
        public List<OptionResultModel> Roles { get; set; }
    }
}
