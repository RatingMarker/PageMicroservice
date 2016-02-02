using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using PageMicroservice.Data.Infrastructure;
using PageMicroservice.Models;

namespace PageMicroservice.Data.Repositories
{
    public interface IPageRepository: IRepository<Page>
    {
        void Insert(IEnumerable<Page> pages);
    }

    public class PageRepository: IPageRepository
    {
        private readonly IContextFactory contextFactory;

        public PageRepository(IContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Page GetById(int id)
        {
            using (var context = contextFactory.Get())
            {
                return context.Pages.SingleOrDefault(x => x.PageId == id);
            }
        }

        public IEnumerable<Page> GetAll()
        {
            using (var context = contextFactory.Get())
            {
                var pages = context.Pages.Include(x => x.Site).ToList();
                return pages;
            }
        }

        public IEnumerable<Page> GetMany(Expression<Func<Page, bool>> @where)
        {
            using (var context = contextFactory.Get())
            {
                return context.Pages.Where(where).ToList();
            }
        }

        public Page Add(Page entity)
        {
            using (var context = contextFactory.Get())
            {
                var site = context.Pages.Add(entity).Entity;
                context.SaveChanges();
                return site;
            }
        }

        public bool Update(Page entity)
        {
            bool isUpdated = false;

            using (var context = contextFactory.Get())
            {
                context.Pages.Update(entity);
                int commit = context.SaveChanges();

                if (commit > 0)
                {
                    isUpdated = true;
                }
            }
            return isUpdated;
        }

        public bool Delete(Page entity)
        {
            bool isDeleted = false;

            using (var context = contextFactory.Get())
            {
                var site = context.Pages.SingleOrDefault(x => x.SiteId == entity.SiteId);

                if (site != null)
                {
                    context.Pages.Remove(site);
                    int commit = context.SaveChanges();

                    if (commit > 0)
                    {
                        isDeleted = true;
                    }
                }
            }
            return isDeleted;
        }

        public void Insert(IEnumerable<Page> pages)
        {
            using (var context = contextFactory.Get())
            {
                context.AddRange(pages);
                context.SaveChangesAsync();
            }
        }
    }
}