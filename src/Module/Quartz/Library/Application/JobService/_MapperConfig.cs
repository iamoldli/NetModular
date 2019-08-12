using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Quartz.Application.JobService.ViewModels;
using Tm.Module.Quartz.Domain.Job;

namespace Tm.Module.Quartz.Application.JobService
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
