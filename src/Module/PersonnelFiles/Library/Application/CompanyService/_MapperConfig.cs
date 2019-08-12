using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.PersonnelFiles.Application.CompanyService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Company;

namespace Tm.Module.PersonnelFiles.Application.CompanyService
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
