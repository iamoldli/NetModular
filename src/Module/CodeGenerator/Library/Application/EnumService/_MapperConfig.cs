using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.CodeGenerator.Application.EnumService.ViewModels;
using Tm.Module.CodeGenerator.Domain.Enum;

namespace Tm.Module.CodeGenerator.Application.EnumService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EnumAddModel, EnumEntity>();
            cfg.CreateMap<EnumEntity, EnumUpdateModel>();
            cfg.CreateMap<EnumUpdateModel, EnumEntity>();
        }
    }
}
