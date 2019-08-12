using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.CodeGenerator.Application.ClassService.ViewModels;
using Tm.Module.CodeGenerator.Domain.Class;
using Tm.Module.CodeGenerator.Domain.ClassMethod;

namespace Tm.Module.CodeGenerator.Application.ClassService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ClassAddModel, ClassEntity>();
            cfg.CreateMap<ClassEntity, ClassUpdateModel>();
            cfg.CreateMap<ClassUpdateModel, ClassEntity>();

            cfg.CreateMap<ClassMethodEntity, ClassMethodModel>();
            cfg.CreateMap<ClassMethodModel, ClassMethodEntity>();
        }
    }
}
