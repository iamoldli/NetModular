using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.ProjectService.ResultModels;
using Nm.Module.CodeGenerator.Application.ProjectService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.ClassMethod;
using Nm.Module.CodeGenerator.Domain.Enum;
using Nm.Module.CodeGenerator.Domain.EnumItem;
using Nm.Module.CodeGenerator.Domain.ModelProperty;
using Nm.Module.CodeGenerator.Domain.Project;
using Nm.Module.CodeGenerator.Domain.Project.Models;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Infrastructure.Options;
using Nm.Module.CodeGenerator.Infrastructure.Repositories;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Default;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Application.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ModuleCommonOptions _commonOptions;
        private readonly CodeGeneratorOptions _codeGeneratorOptions;
        private readonly IProjectRepository _repository;
        private readonly IClassRepository _classRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IEnumRepository _enumRepository;
        private readonly IEnumItemRepository _enumItemRepository;
        private readonly IModelPropertyRepository _modelPropertyRepository;
        private readonly IClassMethodRepository _classMethodRepository;

        public ProjectService(IProjectRepository repository, IMapper mapper, IOptionsMonitor<ModuleCommonOptions> optionsMonitor, IClassRepository classRepository, IPropertyRepository propertyRepository, IEnumRepository enumRepository, IEnumItemRepository enumItemRepository, IModelPropertyRepository modelPropertyRepository, IOptionsMonitor<CodeGeneratorOptions> codeGeneratorOptions, IClassMethodRepository classMethodRepository, IUnitOfWork<CodeGeneratorDbContext> uow)
        {
            _repository = repository;
            _mapper = mapper;
            _classRepository = classRepository;
            _propertyRepository = propertyRepository;
            _enumRepository = enumRepository;
            _enumItemRepository = enumItemRepository;
            _modelPropertyRepository = modelPropertyRepository;
            _classMethodRepository = classMethodRepository;
            _uow = uow;
            _codeGeneratorOptions = codeGeneratorOptions.CurrentValue;
            _commonOptions = optionsMonitor.CurrentValue;
        }

        public async Task<IResultModel> Query(ProjectQueryModel model)
        {
            var result = new QueryResultModel<ProjectEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ProjectAddModel model)
        {
            var entity = _mapper.Map<ProjectEntity>(model);
            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            _uow.BeginTransaction();
            var result = await _repository.SoftDeleteAsync(id);
            if (result)
            {
                result = await _classRepository.DeleteByProject(id);
                if (result)
                {
                    result = await _propertyRepository.DeleteByProject(id);
                    if (result)
                    {
                        result = await _modelPropertyRepository.DeleteByProject(id);
                        if (result)
                        {
                            _uow.BeginTransaction();
                            return ResultModel.Success();
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<ProjectUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ProjectUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel<ProjectBuildCodeResultModel>> BuildCode(ProjectBuildCodeModel model)
        {
            var result = new ResultModel<ProjectBuildCodeResultModel>();
            var project = await _repository.GetAsync(model.Id);
            if (project == null)
                return result.Failed("项目不存在");

            var id = Guid.NewGuid().ToString();
            var rootPath = Path.Combine(_commonOptions.TempPath, _codeGeneratorOptions.BuildCodePath);
            var buildModel = new TemplateBuildModel
            {
                RootPath = Path.Combine(rootPath, id),
            };

            //创建项目生成对象
            var projectBuildModel = _mapper.Map<ProjectBuildModel>(project);

            //查询类生成列表
            var classList = await _classRepository.QueryAllByProject(model.Id);
            foreach (var classEntity in classList)
            {
                var classBuildModel = _mapper.Map<ClassBuildModel>(classEntity);
                var propertyList = await _propertyRepository.QueryByClass(classEntity.Id);
                if (propertyList.Any())
                {
                    //查询属性
                    foreach (var propertyEntity in propertyList)
                    {
                        var propertyBuildModel = _mapper.Map<PropertyBuildModel>(propertyEntity);

                        //如果属性类型是枚举，查询枚举信息
                        if (propertyEntity.Type == PropertyType.Enum && propertyEntity.EnumId.NotEmpty())
                        {
                            var enumEntity = await _enumRepository.GetAsync(propertyEntity.EnumId);
                            propertyBuildModel.Enum = new EnumBuildModel
                            {
                                Name = enumEntity.Name,
                                Remarks = enumEntity.Remarks
                            };

                            var enumItemList = await _enumItemRepository.QueryByEnum(propertyEntity.EnumId);
                            propertyBuildModel.Enum.ItemList = enumItemList.Select(m => new EnumItemBuildModel
                            {
                                Name = m.Name,
                                Remarks = m.Remarks,
                                Value = m.Value
                            }).ToList();
                        }

                        classBuildModel.PropertyList.Add(propertyBuildModel);
                    }
                }


                var modelPropertyList = await _modelPropertyRepository.QueryByClass(classEntity.Id);

                if (modelPropertyList.Any())
                {
                    foreach (var propertyEntity in modelPropertyList)
                    {
                        var modelPropertyBuildModel = _mapper.Map<ModelPropertyBuildModel>(propertyEntity);
                        //如果属性类型是枚举，查询枚举信息
                        if (propertyEntity.Type == PropertyType.Enum && propertyEntity.EnumId.NotEmpty())
                        {
                            var enumEntity = await _enumRepository.GetAsync(propertyEntity.EnumId);
                            modelPropertyBuildModel.Enum = new EnumBuildModel
                            {
                                Name = enumEntity.Name,
                                Remarks = enumEntity.Remarks
                            };

                            var enumItemList = await _enumItemRepository.QueryByEnum(propertyEntity.EnumId);
                            modelPropertyBuildModel.Enum.ItemList = enumItemList.Select(m => new EnumItemBuildModel
                            {
                                Name = m.Name,
                                Remarks = m.Remarks,
                                Value = m.Value
                            }).ToList();
                        }

                        classBuildModel.ModelPropertyList.Add(modelPropertyBuildModel);
                    }
                }

                classBuildModel.Method = await _classMethodRepository.GetByClass(classEntity.Id);

                projectBuildModel.ClassList.Add(classBuildModel);
            }

            buildModel.Project = projectBuildModel;

            var builder = new DefaultTemplateBuilder();
            builder.Build(buildModel);

            ZipFile.CreateFromDirectory(buildModel.RootPath, Path.Combine(rootPath, id + ".zip"));

            var resultModel = new ProjectBuildCodeResultModel
            {
                Id = id,
                Name = projectBuildModel.Name + ".zip"
            };
            return result.Success(resultModel);
        }
    }
}
