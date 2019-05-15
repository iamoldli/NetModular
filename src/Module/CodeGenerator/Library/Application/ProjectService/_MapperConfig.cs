using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.CodeGenerator.Application.ProjectService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.Class;
using NetModular.Module.CodeGenerator.Domain.ModelProperty;
using NetModular.Module.CodeGenerator.Domain.Project;
using NetModular.Module.CodeGenerator.Domain.Property;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Application.ProjectService
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
