using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Common.Application.AttachmentService.ResultModels;
using Nm.Module.Common.Domain.Attachment;

namespace Nm.Module.Common.Application.AttachmentService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AttachmentEntity, AttachmentUploadResultModel>();
        }
    }
}
