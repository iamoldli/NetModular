using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.ClassService.ViewModels;
using Nm.Module.CodeGenerator.Application.ProjectService;
using Nm.Module.CodeGenerator.Application.ProjectService.ResultModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.Class.Models;
using Nm.Module.CodeGenerator.Domain.ClassMethod;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Infrastructure;

namespace Nm.Module.CodeGenerator.Application.ClassService
{
    public class ClassService : IClassService
    {
        private readonly IMapper _mapper;
        private readonly IClassRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly BaseEntityPropertyCollection _baseEntityPropertyCollection;
        private readonly IClassMethodRepository _classMethodRepository;

        private readonly IProjectService _projectService;

        public ClassService(IMapper mapper, IClassRepository repository, BaseEntityPropertyCollection baseEntityPropertyCollection, IPropertyRepository propertyRepository, IClassMethodRepository classMethodRepository, IProjectService projectService)
        {
            _mapper = mapper;
            _repository = repository;
            _baseEntityPropertyCollection = baseEntityPropertyCollection;
            _propertyRepository = propertyRepository;
            _classMethodRepository = classMethodRepository;
            _projectService = projectService;
        }

        public async Task<IResultModel> Query(ClassQueryModel model)
        {
            var result = new QueryResultModel<ClassEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ClassAddModel model)
        {
            var entity = _mapper.Map<ClassEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"类名称({entity.Name})已存在");
            }

            using (var tran = _repository.BeginTransaction())
            {
                if (await _repository.AddAsync(entity, tran))
                {
                    if (entity.BaseEntityType != BaseEntityType.Normal)
                    {
                        var propertyEntities = _baseEntityPropertyCollection.Get(entity.BaseEntityType);
                        propertyEntities.ForEach(m => m.ClassId = entity.Id);
                        //添加基类实体的属性
                        if (await _propertyRepository.AddAsync(propertyEntities, tran))
                        {
                            var methodEntity = _mapper.Map<ClassMethodEntity>(model.Method);
                            methodEntity.ClassId = entity.Id;
                            //添加方法
                            if (await _classMethodRepository.AddAsync(methodEntity, tran))
                            {
                                tran.Commit();
                                return ResultModel.Success();
                            }
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            using (var tran = _repository.BeginTransaction())
            {
                if (await _repository.DeleteAsync(id, tran) && await _propertyRepository.DeleteByClass(id, tran) &&
                    await _classMethodRepository.DeleteByClass(id, tran))
                {
                    tran.Commit();
                    return ResultModel.Success();
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<ClassUpdateModel>(entity);

            var methodEntity = await _classMethodRepository.GetByClass(id);
            model.Method = _mapper.Map<ClassMethodModel>(methodEntity) ?? new ClassMethodModel();

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ClassUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"类名称({entity.Name})已存在");
            }

            using (var tran = _repository.BeginTransaction())
            {
                if (await _repository.UpdateAsync(entity, tran))
                {
                    var methodEntity = await _classMethodRepository.GetByClass(model.Id, tran);
                    if (methodEntity != null)
                    {
                        _mapper.Map(model.Method, methodEntity);
                        //更新方法
                        if (await _classMethodRepository.UpdateAsync(methodEntity, tran))
                        {
                            tran.Commit();
                            return ResultModel.Success();
                        }
                    }
                    else
                    {
                        methodEntity = _mapper.Map<ClassMethodEntity>(model.Method);
                        methodEntity.ClassId = entity.Id;
                        //添加方法
                        if (await _classMethodRepository.AddAsync(methodEntity, tran))
                        {
                            tran.Commit();
                            return ResultModel.Success();
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel<ProjectBuildCodeResultModel>> BuildCode(Guid id)
        {
            var result = new ResultModel<ProjectBuildCodeResultModel>();
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return result.Failed("对象不存在");

            return await _projectService.BuildCode(entity.ProjectId, new List<ClassEntity> { entity });
        }
    }
}
