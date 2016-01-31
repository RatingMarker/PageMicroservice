using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using PageMicroservice.Api.Models;
using PageMicroservice.Models;
using PageMicroservice.Services;

namespace PageMicroservice.Api.Controllers
{
    public class PageModule: NancyModule
    {
        private readonly IPageService pageService;
        private readonly IMapper mapper;

        public PageModule(IPageService pageService,IMapper mapper): base("/pages")
        {
            if (pageService == null)
            {
                throw new ArgumentNullException(nameof(pageService));
            }

            this.pageService = pageService;
            this.mapper = mapper;

            Get["/"] = _ =>
            {
                var pages = pageService.GetAll().ToList();
                var pagesViewModel = mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(pages);
                return pagesViewModel;
            };

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

            Post["/insert"] = _ =>
            {
                var pages = this.Bind<IEnumerable<Page>>();

                pageService.Insert(pages);

                return HttpStatusCode.OK;
            };
        }
    }
}