using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Common.Application.AttachmentService.ViewModels;
using Nm.Module.Common.Domain.Attachment;

namespace Nm.Module.Common.Application.AttachmentService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AttachmentAddModel, AttachmentEntity>();
            cfg.CreateMap<AttachmentEntity, AttachmentUpdateModel>();
            cfg.CreateMap<AttachmentUpdateModel, AttachmentEntity>();
        }
    }
}
