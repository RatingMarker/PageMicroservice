using System.Collections.Generic;
using System.Linq;
using Mapster;
using Nancy;
using Nancy.ModelBinding;
using NLog;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Services;
using PageMicroservice.Api.ViewModels;

namespace PageMicroservice.Api.Controllers
{
    public class PageModule: NancyModule
    {
        public PageModule(
            IPageService pageService,
            IAdapter adapter,
            ILogger logger): base("/api/pages")
        {
            Get["/"] = _ =>
            {
                var pages = pageService.GetAll().ToList();
                var pagesViewModel = adapter.Adapt<IEnumerable<PageViewModel>>(pages);
                return pagesViewModel;
            };

            Get["/{id}"] = parameter =>
            {
                var page = pageService.GetById(parameter.id);
                return page != null ? adapter.Adapt<PageViewModel>(page) : HttpStatusCode.NotFound;
            };

            Post["/"] = _ =>
            {
                var pageViewModel = this.Bind<PageViewModel>();

                var page = pageService.Add(adapter.Adapt<Page>(pageViewModel));
                pageViewModel = adapter.Adapt<PageViewModel>(page);
                return pageViewModel;
            };

            Post["/insert"] = _ =>
            {
                var pagesViewModel = this.Bind<IEnumerable<PageViewModel>>();

                var pages = adapter.Adapt<IEnumerable<Page>>(pagesViewModel);

                logger.Debug(pages.Count());

                int countSaved = pageService.AddRange(pages);

                var counter = new CounterViewModel()
                {
                    Count = pagesViewModel.Count(),
                    Saved = countSaved
                };

                return counter;
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
                var page = new Page() {PageId = parameter.id};
                bool isDeleted = pageService.Remove(page);
                return isDeleted ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            };
        }
    }
}