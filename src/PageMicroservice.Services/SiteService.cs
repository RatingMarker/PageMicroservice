using System.Collections.Generic;
using PageMicroservice.Data.Repositories;
using PageMicroservice.Models;

namespace PageMicroservice.Services
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
        private readonly ISiteRepository siteRepository;
        private readonly IPageRepository pageRepository;

        public SiteService(ISiteRepository siteRepository,IPageRepository pageRepository)
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