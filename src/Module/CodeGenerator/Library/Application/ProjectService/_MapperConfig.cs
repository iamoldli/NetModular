using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.CodeGenerator.Application.ProjectService.ViewModels;
using Tm.Module.CodeGenerator.Domain.Class;
using Tm.Module.CodeGenerator.Domain.ModelProperty;
using Tm.Module.CodeGenerator.Domain.Project;
using Tm.Module.CodeGenerator.Domain.Property;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Application.ProjectService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProjectAddModel, ProjectEntity>();
            cfg.CreateMap<ProjectEntity, ProjectUpdateModel>();
            cfg.CreateMap<ProjectUpdateModel, ProjectEntity>();

            //代码生成
            cfg.CreateMap<ProjectEntity, ProjectBuildModel>();
            cfg.CreateMap<ClassEntity, ClassBuildModel>();
            cfg.CreateMap<PropertyEntity, PropertyBuildModel>();
            cfg.CreateMap<ModelPropertyEntity, ModelPropertyBuildModel>();
        }
    }
}
