using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Tm.Module.CodeGenerator.Domain.Property;

namespace Tm.Module.CodeGenerator.Application.PropertyService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PropertyAddModel, PropertyEntity>();
            cfg.CreateMap<PropertyEntity, PropertyUpdateModel>();
            cfg.CreateMap<PropertyUpdateModel, PropertyEntity>();
        }
    }
}
