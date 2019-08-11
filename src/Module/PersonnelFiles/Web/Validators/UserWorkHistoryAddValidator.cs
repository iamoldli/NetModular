using FluentValidation;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Validation.FluentValidation;
using Nm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;

namespace Nm.Module.PersonnelFiles.Web.Validators
{
    public class UserWorkHistoryAddValidator : AbstractValidator<UserWorkHistoryAddModel>
    {
        public UserWorkHistoryAddValidator()
        {
            RuleFor(x => x.ContactPhone).Phone().When(x => x.ContactPhone.NotNull()).WithMessage("请输入正确的手机号");
        }
    }
}
