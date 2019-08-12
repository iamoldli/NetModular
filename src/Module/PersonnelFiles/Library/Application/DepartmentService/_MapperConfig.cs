using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.PersonnelFiles.Application.DepartmentService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Department;

namespace Tm.Module.PersonnelFiles.Application.DepartmentService
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
