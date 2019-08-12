using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace Tm.Lib.Validation.FluentValidation.Validators
{
    /// <summary>
    /// IP验证
    /// </summary>
    public class IPValidator : PropertyValidator
    {
        private const string Pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private static Regex _regex;

        public IPValidator() : base("IP地址无效")
        {
            _regex = new Regex(Pattern);
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return false;

            return _regex.IsMatch(context.PropertyValue.ToString());
        }
    }
}
