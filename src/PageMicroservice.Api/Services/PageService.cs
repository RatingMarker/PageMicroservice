using System.Collections.Generic;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Repositories;

namespace PageMicroservice.Api.Services
{
    public interface IPageService
    {
        Page GetById(int id);
        IEnumerable<Page> GetAll();
        Page Add(Page page);
        bool Update(Page page);
        bool Remove(Page page);
        int AddRange(IEnumerable<Page> pages);
    }

    public class PageService: IPageService
    {
        private readonly IPageRepository pageRepository;

        public PageService(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        public Page GetById(int id) => pageRepository.GetById(id);

        public IEnumerable<Page> GetAll() => pageRepository.GetAll();

        public Page Add(Page page) => pageRepository.Add(page);

        public bool Update(Page page) => pageRepository.Update(page);

        public bool Remove(Page page) => pageRepository.Delete(page);

        public int AddRange(IEnumerable<Page> pages)
        {
            return pageRepository.AddRange(pages);
        }
    }
}