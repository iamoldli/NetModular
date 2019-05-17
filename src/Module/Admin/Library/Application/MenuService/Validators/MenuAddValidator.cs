using FluentValidation;
using Nm.Lib.Validation.FluentValidation;
using Nm.Module.Admin.Application.MenuService.ViewModels;
using Nm.Module.Admin.Domain.Menu;

namespace Nm.Module.Admin.Application.MenuService.Validators
{
    public class MenuAddValidator : AbstractValidator<MenuAddModel>
    {
        public MenuAddValidator()
        {
            When(x => x.Type == MenuType.Route, () =>
            {
                RuleFor(x => x.ModuleCode).Required().WithMessage("请选择所属模块").DependentRules(() =>
                {
                    RuleFor(x => x.RouteName).Required().WithMessage("请选择路由");
                });
            });

            When(x => x.Type == MenuType.Link, () =>
            {
                RuleFor(x => x.Url).Required().Url().WithMessage("请输入正确的链接地址").DependentRules(() =>
                {
                    RuleFor(x => x.Target).NotEqual(MenuTarget.UnKnown).WithMessage("请选择正确的打开方式");
                });
            });
        }
    }
}
