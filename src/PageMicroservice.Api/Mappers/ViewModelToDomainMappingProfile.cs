using AutoMapper;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.ViewModels;

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