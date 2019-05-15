using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.MenuService.ResultModels;
using NetModular.Module.Admin.Application.MenuService.ViewModels;
using NetModular.Module.Admin.Domain.Menu;

namespace NetModular.Module.Admin.Application.MenuService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuAddModel, MenuEntity>();
            cfg.CreateMap<MenuUpdateModel, MenuEntity>();
            cfg.CreateMap<MenuEntity, MenuTreeResultModel>();
        }
    }
}
