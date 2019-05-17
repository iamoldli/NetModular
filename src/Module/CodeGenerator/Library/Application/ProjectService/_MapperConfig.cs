using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.CodeGenerator.Application.ProjectService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.ModelProperty;
using Nm.Module.CodeGenerator.Domain.Project;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Application.ProjectService
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
