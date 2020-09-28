using System.Collections.Generic;
using NetModular.Lib.Data.Abstractions.Attributes;

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

        /// <summary>
        /// 租户名称
        /// </summary>
        [Ignore]
        public string TenantName { get; set; }

        /// <summary>
        /// 账户检测
        /// </summary>
        public IResultModel Check()
        {
            if (Deleted || Status == AccountStatus.Logout)
                return ResultModel.Failed("账户不存在");

            if (Status == AccountStatus.Register)
                return ResultModel.Failed("账户未激活");

            if (Status == AccountStatus.Disabled)
                return ResultModel.Failed("账户已禁用，请联系管理员");

            return ResultModel.Success();
        }
    }
}
