using System;
using FluentValidation;

namespace NetModular.Lib.Validation.FluentValidation
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationExtensions
    {
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
    }
}
