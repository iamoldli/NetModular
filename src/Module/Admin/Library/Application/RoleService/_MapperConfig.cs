using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.RoleService.ViewModels;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Domain.RoleMenuButton;

namespace NetModular.Module.Admin.Application.RoleService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<RoleAddModel, Role>();
            cfg.CreateMap<Role, RoleUpdateModel>();
            cfg.CreateMap<RoleUpdateModel, Role>();
            cfg.CreateMap<RoleMenuButtonBindModel, RoleMenuButton>();
        }
    }
}
