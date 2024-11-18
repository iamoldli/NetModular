using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace NetModular.Lib.Validation.FluentValidation.Validators;

/// <summary>
/// Url验证
/// </summary>
public class UrlValidator<T> : PropertyValidator<T, string>
{
    private const string Pattern = @"https?://(www\.)?[-a-zA-Z0-9@:%.+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%+.~#?&//=]*)";
    private static readonly Regex _regex = new Regex(Pattern);

    public override string Name => "UrlValidator";

    public UrlValidator() : base()
    {
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        return _regex.IsMatch(value);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "URL地址无效";
}