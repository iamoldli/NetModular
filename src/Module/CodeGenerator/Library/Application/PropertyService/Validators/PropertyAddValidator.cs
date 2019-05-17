using FluentValidation;
using Nm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Property;

namespace Nm.Module.CodeGenerator.Application.PropertyService.Validators
{
    public class PropertyAddValidator : AbstractValidator<PropertyAddModel>
    {
        public PropertyAddValidator()
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
