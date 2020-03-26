using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Validation.FluentValidation
{
    public class ValidateResultFormatHandler : IValidateResultFormatHandler
    {
        public void Format(ResultExecutingContext context)
        {
            //只返回第一条错误信息
            context.Result = new JsonResult(
                ResultModel.Failed(
                    context.ModelState.Values.First(m => m.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).Errors.FirstOrDefault().ErrorMessage));
        }
    }
}
