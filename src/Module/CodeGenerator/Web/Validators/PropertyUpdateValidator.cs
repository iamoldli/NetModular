using FluentValidation;
using Tm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Tm.Module.CodeGenerator.Domain.Property;

namespace Tm.Module.CodeGenerator.Web.Validators
{
    public class PropertyUpdateValidator : AbstractValidator<PropertyUpdateModel>
    {
        public PropertyUpdateValidator()
        {
            RuleFor(x => x.Length)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .When(x => x.Type == PropertyType.String)
                .WithMessage("请输入正确的长度");

            RuleFor(x => x.Precision)
                .NotNull()
                .LessThanOrEqualTo(38)
                .GreaterThan(0)
                .When(x => x.Type == PropertyType.Decimal || x.Type == PropertyType.Double)
                .WithMessage("请输入正确的精度");

            RuleFor(x => x.Scale)
                .NotNull()
                .LessThanOrEqualTo(x => x.Precision)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Type == PropertyType.Decimal || x.Type == PropertyType.Double)
                .WithMessage("请输入正确的小数位");

            RuleFor(x => x.EnumId)
                .NotEmpty()
                .When(x => x.Type == PropertyType.Enum)
                .WithMessage("请选择枚举");
        }
    }
}
