using System.Collections.Generic;
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
            this.siteService = siteService;

            Get["/"] = _ => siteService.GetAll().ToList();

            Get["/{id}"] = x => siteService.GetById(x.id);

            Post["/"] = x =>
            {
                var site = this.Bind<Site>();

                site = siteService.Add(site);

                return site;
            };

            Put["/{id}"] = x =>
            {
                var site = this.Bind<Site>();

                site.SiteId = x.id;

                siteService.Update(site);

                return HttpStatusCode.OK;
            };

            Delete["/{id}"] = x =>
            {
                var site = new Site() {SiteId = x.id};

                siteService.Remove(site);

                return HttpStatusCode.OK;
            };
        }
    }
}