using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Quartz.Application.GroupService.ViewModels;
using Tm.Module.Quartz.Domain.Group;

namespace Tm.Module.Quartz.Application.GroupService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<GroupAddModel, GroupEntity>();
        }
    }
}
