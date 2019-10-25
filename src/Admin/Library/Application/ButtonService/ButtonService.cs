using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.Button.Models;

namespace NetModular.Module.Admin.Application.ButtonService
{
    public class ButtonService : IButtonService
    {
        private readonly IButtonRepository _buttonRepository;

        public ButtonService(IButtonRepository buttonRepository)
        {
            _buttonRepository = buttonRepository;
        }

        public async Task<IResultModel> Query(ButtonQueryModel model)
        {
            var result = new QueryResultModel<ButtonEntity>
            {
                Rows = await _buttonRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }
    }
}
