using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.CodeGenerator.Application.ClassService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.ClassMethod;

namespace Nm.Module.CodeGenerator.Application.ClassService
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
