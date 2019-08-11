using System;
using FluentValidation;
using Nm.Module.Quartz.Application.JobService.ViewModels;

namespace Nm.Module.Quartz.Web.Validators
{
    public class JobUpdateValidator : AbstractValidator<JobUpdateModel>
    {
        public JobUpdateValidator()
        {
            RuleFor(x => x.Interval).GreaterThan(0).When(x => x.TriggerType == Domain.Job.TriggerType.Simple)
                .WithMessage("执行间隔时间必须大于0秒");

            RuleFor(x => x.RepeatCount).GreaterThanOrEqualTo(0).When(x => x.TriggerType == Domain.Job.TriggerType.Simple)
                .WithMessage("执行次数不能小于0");

            RuleFor(x => x.Cron).NotNull().When(x => x.TriggerType == Domain.Job.TriggerType.Cron)
                .WithMessage("请配置CRO表达式");

            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("结束日期不能小于当前日期");

            RuleFor(x => x.BeginDate).LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("开始日期不能大于结束日期");
        }
    }
}
