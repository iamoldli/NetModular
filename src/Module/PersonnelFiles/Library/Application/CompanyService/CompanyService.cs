using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Auth.Abstractions;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.CompanyService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Company;
using Tm.Module.PersonnelFiles.Domain.Company.Models;

namespace Tm.Module.PersonnelFiles.Application.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _repository;

        private readonly ILoginInfo _loginInfo;
        public CompanyService(IMapper mapper, ILoginInfo loginInfo, ICompanyRepository repository)
        {
            _mapper = mapper;
            _loginInfo = loginInfo;
            _repository = repository;
        }

        public async Task<IResultModel> Query(CompanyQueryModel model)
        {
            model.AccountID = _loginInfo.AccountId;
            
            var result = new QueryResultModel<CompanyEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(CompanyAddModel model)
        {
            var entity = _mapper.Map<CompanyEntity>(model);
            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<CompanyUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(CompanyUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            CompanyQueryModel model = new CompanyQueryModel();
            model.Name = "";
            model.Page.Index = 1;
            model.Page.Size = 1000;
            model.AccountID = _loginInfo.AccountId;
            var all = await _repository.Query(model);

          
 

            var list = all.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Id
            }).ToList();
            return ResultModel.Success(list);
        }
    }
}
