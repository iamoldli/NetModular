using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Admin.Application.RoleService.ViewModels;
using Nm.Module.Admin.Domain.Role;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Application.RoleService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<RoleAddModel, RoleEntity>();
            cfg.CreateMap<RoleEntity, RoleUpdateModel>();
            cfg.CreateMap<RoleUpdateModel, RoleEntity>();
            cfg.CreateMap<RoleMenuButtonBindModel, RoleMenuButtonEntity>();
        }
    }
}
