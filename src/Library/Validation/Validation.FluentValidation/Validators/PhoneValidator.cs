using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace Tm.Lib.Validation.FluentValidation.Validators
{
    /// <summary>
    /// 手机号简单验证
    /// </summary>
    public class PhoneValidator : PropertyValidator
    {
        private const string Pattern = @"^1[345789]\d{9}$";
        private static Regex _regex;

        public PhoneValidator() : base("手机号无效")
        {
            _regex=new Regex(Pattern);
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return false;

            return _regex.IsMatch(context.PropertyValue.ToString());
        }
    }
}
