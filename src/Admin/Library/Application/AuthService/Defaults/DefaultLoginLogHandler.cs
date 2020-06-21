using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Abstractions.Providers;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Domain.LoginLog;

namespace NetModular.Module.Admin.Application.AuthService.Defaults
{
    [Singleton]
    internal class DefaultLoginLogHandler : ILoginLogProvider
    {
        private readonly ILoginLogRepository _repository;
        private readonly ILoginInfo _loginInfo;

        public DefaultLoginLogHandler(ILoginLogRepository repository, ILoginInfo loginInfo)
        {
            _repository = repository;
            _loginInfo = loginInfo;
        }

        public Task Handle(LoginResultModel model)
        {
            var entity = new LoginLogEntity
            {
                AccountId = model.AccountId,
                UserName = model.UserName,
                Email = model.Email,
                Error = model.Error,
                LoginMode = model.LoginMode,
                LoginTime = model.LoginTime,
                Phone = model.Phone,
                Platform = model.Platform,
                Success = model.Success,
                IP = _loginInfo.IP,
                UserAgent = _loginInfo.UserAgent
            };
            return _repository.AddAsync(entity);
        }
    }
}
