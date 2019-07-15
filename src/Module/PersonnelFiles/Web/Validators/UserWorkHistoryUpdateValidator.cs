using FluentValidation;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Validation.FluentValidation;
using Nm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;

namespace Nm.Module.PersonnelFiles.Web.Validators
{
    public class UserWorkHistoryUpdateValidator : AbstractValidator<UserWorkHistoryUpdateModel>
    {
        public UserWorkHistoryUpdateValidator()
        {
            RuleFor(x => x.ContactPhone).Phone().When(x => x.ContactPhone.NotNull()).WithMessage("请输入正确的手机号");
        }
    }
}
