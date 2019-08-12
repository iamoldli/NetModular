using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.PersonnelFiles.Application.UserService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.User;
using Tm.Module.PersonnelFiles.Domain.UserContact;

namespace Tm.Module.PersonnelFiles.Application.UserService
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
