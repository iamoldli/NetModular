using FluentValidation;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Validation.FluentValidation;
using Tm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;

namespace Tm.Module.PersonnelFiles.Web.Validators
{
    public class UserWorkHistoryUpdateValidator : AbstractValidator<UserWorkHistoryUpdateModel>
    {
        public UserWorkHistoryUpdateValidator()
        {
            RuleFor(x => x.ContactPhone).Phone().When(x => x.ContactPhone.NotNull()).WithMessage("请输入正确的手机号");
        }
    }
}
