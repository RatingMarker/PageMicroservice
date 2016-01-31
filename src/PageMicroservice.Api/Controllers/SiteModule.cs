using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using PageMicroservice.Models;
using PageMicroservice.Services;

namespace PageMicroservice.Api.Controllers
{
    public class SiteModule: NancyModule
    {
        private readonly ISiteService siteService;

        public SiteModule(ISiteService siteService): base("/sites")
        {
            if (siteService == null)
            {
                throw new ArgumentNullException(nameof(siteService));
            }

            this.siteService = siteService;

            Get["/"] = _ => siteService.GetAll();

            Get["/{id}"] = parameter => siteService.GetById(parameter.id) ?? HttpStatusCode.NotFound;

            Get["{id}/pages"] = parameter => siteService.GetPages(parameter.id) ?? HttpStatusCode.NotFound;

            Post["/"] = _ =>
            {
                var site = this.Bind<Site>();

                site = siteService.Add(site);

                return site;
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