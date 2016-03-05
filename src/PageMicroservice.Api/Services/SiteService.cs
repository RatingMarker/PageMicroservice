using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Repositories;

namespace PageMicroservice.Api.Services
{
    public interface ISiteService
    {
        Site GetById(int id);
        IEnumerable<Site> GetAll();
        Site Add(Site site);
        bool Update(Site site);
        bool Remove(Site site);
        IEnumerable<Page> GetPages(int id);
        IEnumerable<Page> GetPages(int id, int state);
    }

    public class SiteService: ISiteService
    {
        private readonly IPageRepository pageRepository;
        private readonly ISiteRepository siteRepository;

        public SiteService(ISiteRepository siteRepository, IPageRepository pageRepository)
        {
            this.siteRepository = siteRepository;
            this.pageRepository = pageRepository;
        }

        public Site GetById(int id) => siteRepository.GetById(id);

        public IEnumerable<Site> GetAll() => siteRepository.GetAll();

        public Site Add(Site site) => siteRepository.Add(site);

        public bool Update(Site site) => siteRepository.Update(site);

        public bool Remove(Site site) => siteRepository.Delete(site);

        public IEnumerable<Page> GetPages(int id)
        {
            return pageRepository.GetMany(x => x.SiteId == id);
        }

        public IEnumerable<Page> GetPages(int id, int state)
        {
            Expression<Func<Page, bool>> where;

            switch (state)
            {
                case 1:
                    where = x => x.SiteId == id && x.FoundDate == null;
                    break;
                case 2:
                    where = x => x.SiteId == id && x.LastScanDate == null && x.Url.EndsWith(".xml");
                    break;
                case 3:
                    where = x => x.SiteId == id && x.LastScanDate == null;
                    break;
                case 4:
                    where = x => x.SiteId == id && x.LastScanDate < DateTime.Today.AddDays(-1);
                    break;
                default:
                    where = x => x.SiteId == id;
                    break;
            }

            return pageRepository.GetMany(where);
        }
    }
}