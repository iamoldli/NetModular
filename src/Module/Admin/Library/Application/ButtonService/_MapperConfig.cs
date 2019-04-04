using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.ButtonService.ViewModels;
using NetModular.Module.Admin.Domain.Button;

namespace NetModular.Module.Admin.Application.ButtonService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ButtonAddModel, Button>();
            cfg.CreateMap<ButtonUpdateModel, Button>();
            cfg.CreateMap<Button, ButtonUpdateModel>();
        }
    }
}
