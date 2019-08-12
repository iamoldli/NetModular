using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels;
using Tm.Module.CodeGenerator.Domain.ModelProperty;

namespace Tm.Module.CodeGenerator.Application.ModelPropertyService
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
