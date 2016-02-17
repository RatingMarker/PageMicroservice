using System.Collections.Generic;
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
    }
}