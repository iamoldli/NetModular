using FluentValidation;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Application.AccountService.ViewModels;

namespace NetModular.Module.Admin.Web.Validators
{
    public class AccountUpdateValidator : AbstractValidator<AccountUpdateModel>
    {
        public AccountUpdateValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => x.Email.NotNull()).WithMessage("请输入正确的邮箱地址");
        }
    }
}
