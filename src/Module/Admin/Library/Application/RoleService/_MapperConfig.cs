using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Admin.Application.RoleService.ViewModels;
using Tm.Module.Admin.Domain.Role;
using Tm.Module.Admin.Domain.RoleMenuButton;

namespace Tm.Module.Admin.Application.RoleService
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
