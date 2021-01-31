using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace NetModular.Lib.Validation.FluentValidation.Validators
{
    /// <summary>
    /// Url验证
    /// </summary>
    public class UrlValidator : PropertyValidator
    {
        private const string Pattern = @"https?://(www.)?[-a-zA-Z0-9@:%.+~#=]{1,256}.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%+.~#?&//=]*)";
        private static Regex _regex;

        public UrlValidator() : base("URL地址无效")
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
