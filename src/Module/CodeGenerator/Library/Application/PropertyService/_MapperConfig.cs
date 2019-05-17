using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Property;

namespace Nm.Module.CodeGenerator.Application.PropertyService
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
