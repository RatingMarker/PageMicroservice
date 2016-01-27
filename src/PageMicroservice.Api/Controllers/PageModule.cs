using System;
using Nancy;
using Nancy.ModelBinding;
using PageMicroservice.Models;
using PageMicroservice.Services;

namespace PageMicroservice.Api.Controllers
{
    public class PageModule: NancyModule
    {
        private readonly IPageService pageService;

        public PageModule(IPageService pageService): base("/pages")
        {
            if (pageService == null)
            {
                throw new ArgumentNullException(nameof(pageService));
            }

            this.pageService = pageService;

            Get["/"] = _ => pageService.GetAll();

            Get["/{id}"] = parameter => pageService.GetById(parameter.id) ?? HttpStatusCode.NotFound;

            Post["/"] = _ =>
            {
                var page = this.Bind<Page>();

                page = pageService.Add(page);

                return page;
            };

            Put["/{id}"] = parameter =>
            {
                var page = this.Bind<Page>();

                page.PageId = parameter.id;

                bool isUpdated = pageService.Update(page);

                return isUpdated ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            };

            Delete["/{id}"] = parameter =>
            {
                var page = new Page() { PageId = parameter.id };

                bool isDeleted = pageService.Remove(page);

                return isDeleted ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            };
        }
    }
}