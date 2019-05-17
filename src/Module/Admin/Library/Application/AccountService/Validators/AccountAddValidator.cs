using FluentValidation;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Application.AccountService.ViewModels;

namespace Nm.Module.Admin.Application.AccountService.Validators
{
    public class AccountAddValidator : AbstractValidator<AccountAddModel>
    {
        public AccountAddValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => x.Email.NotNull()).WithMessage("请输入正确的邮箱地址");
            RuleFor(x => x.Password).Length(6, 100).When(x => x.Password.NotNull()).WithMessage("请输入正确的邮箱地址");
        }
    }
}
