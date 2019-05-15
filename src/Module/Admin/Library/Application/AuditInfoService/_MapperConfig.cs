using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.AuditInfoService.ResultModels;
using NetModular.Module.Admin.Domain.AuditInfo;

namespace NetModular.Module.Admin.Application.AuditInfoService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AuditInfoEntity, AuditInfoQueryResultModel>();
        }
    }
}
