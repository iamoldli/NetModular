using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.UserContactService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserContact;

namespace Nm.Module.PersonnelFiles.Application.UserContactService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserContactAddModel, UserContactEntity>();
            cfg.CreateMap<UserContactEntity, UserContactUpdateModel>();
            cfg.CreateMap<UserContactUpdateModel, UserContactEntity>();
        }
    }
}
