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
                return context.Pages.SingleOrDefault(x=>x.SiteId == id);
            }
        }

        public IEnumerable<Page> GetAll()
        {
            using (var context = contextFactory.Get())
            {
                return context.Pages.ToList();
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

        public void Update(Page entity)
        {
            using (var context = contextFactory.Get())
            {
                context.Pages.Update(entity);
                context.SaveChanges();
            }
        }

        public void Delete(Page entity)
        {
            using (var context = contextFactory.Get())
            {
                var site = context.Pages.SingleOrDefault(x => x.SiteId == entity.SiteId);

                if (site != null)
                {
                    context.Pages.Remove(site);
                    context.SaveChanges();
                }
            }
        }
    }
}