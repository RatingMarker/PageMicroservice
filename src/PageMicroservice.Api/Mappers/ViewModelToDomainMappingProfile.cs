using AutoMapper;
using PageMicroservice.Api.Models;
using PageMicroservice.Models;

namespace PageMicroservice.Api.Mappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<SiteViewModel, Site>();
            CreateMap<PageViewModel, Page>();
        }
    }
}