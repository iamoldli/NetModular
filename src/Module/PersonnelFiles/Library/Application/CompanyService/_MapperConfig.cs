using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.CompanyService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.Company;

namespace Nm.Module.PersonnelFiles.Application.CompanyService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CompanyAddModel, CompanyEntity>();
            cfg.CreateMap<CompanyEntity, CompanyUpdateModel>();
            cfg.CreateMap<CompanyUpdateModel, CompanyEntity>();
        }
    }
}
