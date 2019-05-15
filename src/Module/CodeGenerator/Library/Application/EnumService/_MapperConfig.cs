using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.CodeGenerator.Application.EnumService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.Enum;

namespace NetModular.Module.CodeGenerator.Application.EnumService
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
