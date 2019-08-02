using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Quartz.Application.JobClassService.ViewModels;
using Nm.Module.Quartz.Domain.JobClass;

namespace Nm.Module.Quartz.Application.JobClassService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<JobClassAddModel, JobClassEntity>();
            cfg.CreateMap<JobClassEntity, JobClassUpdateModel>();
            cfg.CreateMap<JobClassUpdateModel, JobClassEntity>();
        }
    }
}
