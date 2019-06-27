using System.Text.RegularExpressions;
using FluentValidation;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Validation.FluentValidation;
using Nm.Module.PersonnelFiles.Application.UserService.ViewModels;

namespace Nm.Module.PersonnelFiles.Application.UserService.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateModel>
    {
        private readonly Regex _idCardNoRegex = new Regex(@"^[1-6][1-7]\d{4}(19|20)\d{2}(((0[13578]|1[02])(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)(0[1-9]|[12][0-9]|30))|(02(0[1-9]|[1][0-9]|2[0-8])))((\d{3}X)|\d{4})$");

        public UserUpdateValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => x.Email.NotNull()).WithMessage("无效的邮箱地址");
            RuleFor(x => x.IdCardNo).Matches(_idCardNoRegex).When(x => x.IdCardNo.NotNull()).WithMessage("无效的身份证号");
            RuleFor(x => x.Phone).Phone().When(x => x.Phone.NotNull()).WithMessage("请输入正确的手机号");
        }
    }
}
