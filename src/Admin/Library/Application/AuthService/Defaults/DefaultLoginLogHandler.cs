using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Application.AuthService.Interfaces;
using NetModular.Module.Admin.Domain.LoginLog;

namespace NetModular.Module.Admin.Application.AuthService.Defaults
{
    [Singleton]
    internal class DefaultLoginLogHandler : ILoginLogHandler
    {
        private readonly ILoginLogRepository _repository;

        public DefaultLoginLogHandler(ILoginLogRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(LoginLogEntity entity)
        {
            return _repository.AddAsync(entity);
        }
    }
}
