using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace NetModular.Lib.Validation.FluentValidation.Validators;

/// <summary>
/// IP验证
/// </summary>
public class IPValidator<T> : PropertyValidator<T, string>
{
    private const string Pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9][01]?[0-9][0-9]?)$";
    private static readonly Regex _regex = new Regex(Pattern);

    public override string Name => "IPValidator";

    public IPValidator() : base()
    {
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        return _regex.IsMatch(value);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "IP地址无效";
}