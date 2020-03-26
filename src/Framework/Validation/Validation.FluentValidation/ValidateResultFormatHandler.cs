using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Validation.FluentValidation
{
    public class ValidateResultFormatHandler : IValidateResultFormatHandler
    {
        public void Format(ResultExecutingContext context)
        {
            var errors = context.ModelState
                .Where(m => m.Value.ValidationState == ModelValidationState.Invalid)
                .Select(m =>
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("{0}：", m.Key);
                    sb.Append(m.Value.Errors.Select(n => n.ErrorMessage).Aggregate((x, y) => x + ";" + y));
                    return sb.ToString();
                })
                .Aggregate((x, y) => x + "|" + y);

            context.Result = new JsonResult(ResultModel.Failed(errors));
        }
    }
}
