using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.PermissionService.ViewModels;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.PermissionService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PermissionAddModel, PermissionEntity>();
            cfg.CreateMap<PermissionUpdateModel, PermissionEntity>();
            cfg.CreateMap<PermissionEntity, PermissionUpdateModel>();
        }
    }
}
