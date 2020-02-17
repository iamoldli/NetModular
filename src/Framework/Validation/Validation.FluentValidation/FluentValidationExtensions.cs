using System;
using FluentValidation;
using NetModular.Lib.Validation.FluentValidation.Validators;

namespace NetModular.Lib.Validation.FluentValidation
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PhoneValidator());
        }

        /// <summary>
        /// 验证URL地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Url<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new UrlValidator());
        }

        /// <summary>
        /// 必须，不允许为空或者null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Required<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty();
        }

        /// <summary>
        /// 必须，不允许为Guid.Empty或者null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, Guid> Required<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty();
        }

        /// <summary>
        /// 验证IP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IP<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new IPValidator());
        }
    }
}
