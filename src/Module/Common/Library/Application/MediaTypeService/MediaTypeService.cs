using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.MediaTypeService.ViewModels;
using Nm.Module.Common.Domain.MediaType;
using Nm.Module.Common.Domain.MediaType.Models;

namespace Nm.Module.Common.Application.MediaTypeService
{
    public class MediaTypeService : IMediaTypeService
    {
        private readonly IMediaTypeRepository _repository;

        public MediaTypeService(IMediaTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResultModel> Query(MediaTypeQueryModel model)
        {
            var result = new QueryResultModel<MediaTypeEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(MediaTypeAddModel model)
        {
            if (await _repository.Exists(model.Ext))
                return ResultModel.HasExists;

            var entity = new MediaTypeEntity
            {
                Ext = model.Ext,
                Value = model.Value
            };

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return ResultModel.NotExists;

            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = new MediaTypeUpdateModel
            {
                Ext = entity.Ext,
                Value = entity.Value
            };

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(MediaTypeUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.Exists(model.Ext, model.Id))
                return ResultModel.Failed("扩展名已存在");

            entity.Ext = model.Ext;
            entity.Value = model.Value;

            var result = await _repository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }
    }
}
