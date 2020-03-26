using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.Admin.Domain.Account.Models
{
    public class AccountQueryModel : QueryModel
    {
        /// <summary>
        /// 账户类型
        /// </summary>
        public AccountType? Type { get; set; }

        public int Age { get; set; }

        public int AgeTest { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public AccountStatus Status { get; set; }
    }
}
