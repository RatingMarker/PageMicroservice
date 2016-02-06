using System;
using System.Collections.Generic;
using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using PageMicroservice.Api.Models;
using PageMicroservice.Models;
using PageMicroservice.Services;

namespace PageMicroservice.Api.Controllers
{
    public class SiteModule: NancyModule
    {
        private readonly ISiteService siteService;
        private readonly IMapper mapper;

        public SiteModule(ISiteService siteService, IMapper mapper): base("/sites")
        {
            if (siteService == null)
            {
                throw new ArgumentNullException(nameof(siteService));
            }
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.siteService = siteService;
            this.mapper = mapper;

            Get["/"] = _ =>
            {
                var sites = siteService.GetAll();
                var sitesViewModel = mapper.Map<IEnumerable<Site>, IEnumerable<SiteViewModel>>(sites);
                return sitesViewModel;
            };

            Get["/{id}"] = parameter =>
            {
                var site = siteService.GetById(parameter.id);
                return site != null ? mapper.Map<Site, SiteViewModel>(site) : HttpStatusCode.NotFound;
            };

            Get["{id}/pages"] = parameter =>
            {
                var pages = siteService.GetPages(parameter.id);
                return pages != null ? mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(pages) : HttpStatusCode.NotFound;
            };

            Post["/"] = _ =>
            {
                var site = this.Bind<Site>();
                site = siteService.Add(site);
                var siteViewModel = mapper.Map<Site, SiteViewModel>(site);
                return siteViewModel;
            };

            Put["/{id}"] = parameter =>
            {
                var site = this.Bind<Site>();
                site.SiteId = parameter.id;
                bool isUpdated = siteService.Update(site);
                return isUpdated ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            };

            Delete["/{id}"] = parameter =>
            {
                var site = new Site() {SiteId = parameter.id};
                bool isDeleted = siteService.Remove(site);
                return isDeleted ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            };
        }
    }
}