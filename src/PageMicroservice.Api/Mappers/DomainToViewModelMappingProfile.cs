using AutoMapper;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.ViewModels;

namespace PageMicroservice.Api.Mappers
{
    public class DomainToViewModelMappingProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Site, SiteViewModel>();
            CreateMap<Page, PageViewModel>();
        }
    }
}