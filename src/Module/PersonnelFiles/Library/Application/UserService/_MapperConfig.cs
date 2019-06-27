using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.UserService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.User;
using Nm.Module.PersonnelFiles.Domain.UserContact;

namespace Nm.Module.PersonnelFiles.Application.UserService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserAddModel, UserEntity>();
            cfg.CreateMap<UserEntity, UserUpdateModel>();
            cfg.CreateMap<UserUpdateModel, UserEntity>();

            cfg.CreateMap<UserContactUpdateViewModel, UserContactEntity>();
            cfg.CreateMap<UserContactEntity, UserContactUpdateViewModel>();
        }
    }
}
