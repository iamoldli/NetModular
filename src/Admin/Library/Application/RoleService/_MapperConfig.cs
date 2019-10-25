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
            cfg.CreateMap<RoleAddModel, RoleEntity>();
            cfg.CreateMap<RoleEntity, RoleUpdateModel>();
            cfg.CreateMap<RoleUpdateModel, RoleEntity>();
            cfg.CreateMap<RoleMenuButtonBindModel, RoleMenuButtonEntity>();
        }
    }
}
