using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.CodeGenerator.Application.ModelPropertyService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.ModelProperty;

namespace NetModular.Module.CodeGenerator.Application.ModelPropertyService
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
