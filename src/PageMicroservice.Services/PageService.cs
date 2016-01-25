using System.Collections.Generic;
using PageMicroservice.Data.Repositories;
using PageMicroservice.Models;

namespace PageMicroservice.Services
{
    public interface IPageService
    {
        Page GetById(int id);
        IEnumerable<Page> GetAll();
        Page Add(Page page);
        void Update(Page page);
        void Remove(Page page);
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

        public void Update(Page page) => pageRepository.Update(page);

        public void Remove(Page page) => pageRepository.Delete(page);
    }
}