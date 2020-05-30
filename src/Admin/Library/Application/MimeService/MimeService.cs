using System.Threading.Tasks;
using NetModular.Module.Admin.Application.MimeService.ViewModels;
using NetModular.Module.Admin.Domain.Mime;
using NetModular.Module.Admin.Domain.Mime.Models;

namespace NetModular.Module.Admin.Application.MimeService
{
    public class MimeService : IMimeService
    {
        private readonly IMimeRepository _repository;

        public MimeService(IMimeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResultModel> Query(MimeQueryModel model)
        {
            var result = new QueryResultModel<MimeEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(MimeAddModel model)
        {
            var entity = new MimeEntity
            {
                Ext = model.Ext.Trim(),
                Value = model.Value.Trim()
            };

            if (await _repository.Exists(entity))
                return ResultModel.HasExists;

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

            var model = new MimeUpdateModel
            {
                Id = id,
                Ext = entity.Ext,
                Value = entity.Value
            };

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(MimeUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.Exists(entity))
                return ResultModel.Failed("扩展名已存在");

            entity.Ext = model.Ext;
            entity.Value = model.Value;

            var result = await _repository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }
    }
}
