using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels;
using Nm.Module.CodeGenerator.Domain.ModelProperty;

namespace Nm.Module.CodeGenerator.Application.ModelPropertyService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ModelPropertyAddModel, ModelPropertyEntity>();
            cfg.CreateMap<ModelPropertyEntity, ModelPropertyUpdateModel>();
            cfg.CreateMap<ModelPropertyUpdateModel, ModelPropertyEntity>();
        }
    }
}
