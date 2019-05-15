using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.CodeGenerator.Application.PropertyService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.Property;

namespace NetModular.Module.CodeGenerator.Application.PropertyService
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
