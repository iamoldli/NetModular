using FluentValidation;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Validation.FluentValidation;
using Tm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;

namespace Tm.Module.PersonnelFiles.Web.Validators
{
    public class UserWorkHistoryAddValidator : AbstractValidator<UserWorkHistoryAddModel>
    {
        public UserWorkHistoryAddValidator()
        {
            RuleFor(x => x.ContactPhone).Phone().When(x => x.ContactPhone.NotNull()).WithMessage("请输入正确的手机号");
        }
    }
}
