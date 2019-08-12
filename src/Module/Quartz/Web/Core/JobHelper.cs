using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tm.Lib.Module.Abstractions;
using Tm.Lib.Quartz.Abstractions;
using Tm.Lib.Utils.Core.Attributes;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Utils.Core.Result;
using Quartz;

namespace Tm.Module.Quartz.Web.Core
{
    /// <summary>
    /// 任务帮助类
    /// </summary>
    [Singleton]
    public class JobHelper
    {
        /// <summary>
        /// 模块下拉列表
        /// </summary>
        public readonly IList<OptionResultModel> ModuleSelect;

        /// <summary>
        /// 模块信息
        /// </summary>
        public readonly IList<JobModuleDescriptor> JobModuleDescriptors;

        public JobHelper(IModuleCollection moduleCollection)
        {
            JobModuleDescriptors = new List<JobModuleDescriptor>();
            ModuleSelect = new List<OptionResultModel>();

            foreach (var module in moduleCollection)
            {
                ModuleSelect.Add(new OptionResultModel
                {
                    Label = module.Name,
                    Value = module.Id
                });

                var jobModuleDescriptor = new JobModuleDescriptor
                {
                    Id = module.Id,
                    Descriptor = module,
                    JobClassSelect = new List<OptionResultModel>()
                };

                #region ==添加任务类==

                if (module.AssemblyDescriptor.Quartz != null)
                {
                    var jobClassTypes = module.AssemblyDescriptor.Quartz.GetTypes().Where(m => typeof(IJob).IsAssignableFrom(m));
                    foreach (var jobClassType in jobClassTypes)
                    {
                        var jobClassOption = new OptionResultModel
                        {
                            Label = jobClassType.Name,
                            Value = $"{jobClassType.FullName}, {jobClassType.Assembly.GetName().Name}"
                        };

                        var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(jobClassType, typeof(DescriptionAttribute));
                        if (descAttr != null && descAttr.Description.NotNull())
                        {
                            jobClassOption.Label = descAttr.Description;
                        }

                        jobModuleDescriptor.JobClassSelect.Add(jobClassOption);
                    }
                }

                #endregion

                JobModuleDescriptors.Add(jobModuleDescriptor);
            }
        }

        public IList<OptionResultModel> GetJobSelect(string moduleId)
        {
            var jobModuleDescriptor = JobModuleDescriptors.FirstOrDefault(m => m.Id.EqualsIgnoreCase(moduleId));
            if (jobModuleDescriptor != null)
            {
                return jobModuleDescriptor.JobClassSelect;
            }

            return new List<OptionResultModel>();
        }
    }
}
