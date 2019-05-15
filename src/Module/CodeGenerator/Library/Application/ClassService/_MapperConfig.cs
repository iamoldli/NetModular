using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.CodeGenerator.Application.ClassService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.Class;

namespace NetModular.Module.CodeGenerator.Application.ClassService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ClassAddModel, ClassEntity>();
            cfg.CreateMap<ClassEntity, ClassUpdateModel>();
            cfg.CreateMap<ClassUpdateModel, ClassEntity>();
        }
    }
}
