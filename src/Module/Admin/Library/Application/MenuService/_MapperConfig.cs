using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Admin.Application.MenuService.ResultModels;
using Nm.Module.Admin.Application.MenuService.ViewModels;
using Nm.Module.Admin.Domain.Menu;

namespace Nm.Module.Admin.Application.MenuService
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
