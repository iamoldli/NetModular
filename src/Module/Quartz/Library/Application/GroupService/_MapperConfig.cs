using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Quartz.Application.GroupService.ViewModels;
using Nm.Module.Quartz.Domain.Group;

namespace Nm.Module.Quartz.Application.GroupService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<GroupAddModel, GroupEntity>();
            cfg.CreateMap<GroupEntity, GroupUpdateModel>();
            cfg.CreateMap<GroupUpdateModel, GroupEntity>();
        }
    }
}
