using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.Abstractions;
using NetModular.Module.Admin.Application.ModuleInfoService.ViewModels;

namespace NetModular.Module.Admin.Application.ModuleInfoService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ModuleInfoAddModel, ModuleInfo>();
            cfg.CreateMap<ModuleInfoUpdateModel, ModuleInfo>();
            cfg.CreateMap<ModuleInfo, ModuleInfoUpdateModel>();
        }
    }
}
