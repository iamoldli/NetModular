using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.DepartmentService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.Department;

namespace Nm.Module.PersonnelFiles.Application.DepartmentService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DepartmentAddModel, DepartmentEntity>();
            cfg.CreateMap<DepartmentEntity, DepartmentUpdateModel>();
            cfg.CreateMap<DepartmentUpdateModel, DepartmentEntity>();
        }
    }
}
