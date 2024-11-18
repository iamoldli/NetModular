using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace NetModular.Lib.Validation.FluentValidation.Validators
{
    /// <summary>
    /// 手机号简单验证
    /// </summary>
    public class PhoneValidator<T> : PropertyValidator<T, string>
    {
        private const string Pattern = @"^1[345789]\d{9}$";
        private static readonly Regex _regex = new Regex(Pattern);

        public override string Name => "PhoneValidator";

        public PhoneValidator() : base()
        {
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return _regex.IsMatch(value);
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "手机号无效";
    }
}