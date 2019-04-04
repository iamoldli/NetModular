using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Validation.FluentValidation
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加FluentValidation
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMvcBuilder AddValidators(this IMvcBuilder builder, IServiceCollection services)
        {
            //注入验证结果格式化器
            services.TryAddSingleton<IValidateResultFormatHandler, ValidateResultFormatHandler>();

            builder.AddFluentValidation(fv =>
            {
                var modules = services.BuildServiceProvider().GetService<IModuleCollection>();
                foreach (var moduleInfo in modules)
                {
                    fv.RegisterValidatorsFromAssembly(moduleInfo.AssembliesInfo.Application);
                }
            });

            //当一个验证失败时，后续的验证不再执行
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            return builder;
        }
    }
}
