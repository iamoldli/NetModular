using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Domain.Button;
using Tm.Module.Admin.Domain.Button.Models;

namespace Tm.Module.Admin.Application.ButtonService
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
