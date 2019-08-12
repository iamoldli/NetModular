using FluentValidation;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Application.AccountService.ViewModels;

namespace Tm.Module.Admin.Web.Validators
{
    public class AccountUpdateValidator : AbstractValidator<AccountUpdateModel>
    {
        public AccountUpdateValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => x.Email.NotNull()).WithMessage("请输入正确的邮箱地址");
        }
    }
}
