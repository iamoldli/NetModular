using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Admin.Application.MenuService.ResultModels;
using Tm.Module.Admin.Application.MenuService.ViewModels;
using Tm.Module.Admin.Domain.Menu;

namespace Tm.Module.Admin.Application.MenuService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuAddModel, MenuEntity>();
            cfg.CreateMap<MenuEntity, MenuUpdateModel>();
            cfg.CreateMap<MenuUpdateModel, MenuEntity>();
            cfg.CreateMap<MenuEntity, MenuTreeResultModel>();
        }
    }
}
