using FluentValidation;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Application.AccountService.ViewModels;

namespace Nm.Module.Admin.Web.Validators
{
    public class AccountUpdateValidator : AbstractValidator<AccountUpdateModel>
    {
        public AccountUpdateValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => x.Email.NotNull()).WithMessage("请输入正确的邮箱地址");
        }
    }
}
