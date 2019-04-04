using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;

namespace NetModular.Module.Admin.Application.AccountService.ViewModels
{
    public class AccountQueryModel : QueryModel
    {
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
