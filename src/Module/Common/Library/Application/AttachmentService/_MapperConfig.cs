using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Common.Application.AttachmentService.ResultModels;
using Tm.Module.Common.Domain.Attachment;

namespace Tm.Module.Common.Application.AttachmentService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AttachmentEntity, AttachmentUploadResultModel>();
        }
    }
}
