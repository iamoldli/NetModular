using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.CodeGenerator.Application.EnumService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Enum;

namespace Nm.Module.CodeGenerator.Application.EnumService
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
