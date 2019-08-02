using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Quartz.Application.JobService.ViewModels;
using Nm.Module.Quartz.Domain.Job;

namespace Nm.Module.Quartz.Application.JobService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<JobAddModel, JobEntity>();
            cfg.CreateMap<JobEntity, JobUpdateModel>();
            cfg.CreateMap<JobUpdateModel, JobEntity>();
        }
    }
}
