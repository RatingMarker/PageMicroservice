using System.Collections.Generic;
using Mapster;
using Nancy;
using Nancy.ModelBinding;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Services;
using PageMicroservice.Api.ViewModels;

namespace PageMicroservice.Api.Controllers
{
    public class SiteModule: NancyModule
    {
        public SiteModule(ISiteService siteService, IAdapter adapter): base("/api/sites")
        {
            Get["/"] = _ =>
            {
                var sites = siteService.GetAll();
                var sitesViewModel = adapter.Adapt<IEnumerable<SiteViewModel>>(sites);
                return sitesViewModel;
            };

            Get["/{id}"] = parameter =>
            {
                var site = siteService.GetById(parameter.id);
                return site != null ? adapter.Adapt<SiteViewModel>(site) : HttpStatusCode.NotFound;
            };

            Get["{id}/pages"] = parameter =>
            {
                var pages = siteService.GetPages(parameter.id);
                return pages != null ? adapter.Adapt<IEnumerable<PageViewModel>>(pages) : HttpStatusCode.NotFound;
            };

            Get["{id}/pages/{state}"] = parameter =>
            {
                var pages = siteService.GetPages(parameter.id, parameter.state);
                return pages != null ? adapter.Adapt<IEnumerable<PageViewModel>>(pages) : HttpStatusCode.NotFound;
            };

            Post["/"] = _ =>
            {
                var site = this.Bind<Site>();
                site = siteService.Add(site);
                var siteViewModel = adapter.Adapt<SiteViewModel>(site);
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