using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PageMicroservice.Data.Contexts;
using PageMicroservice.Data.Infrastructure;
using PageMicroservice.Models;

namespace PageMicroservice.Data.Repositories
{
    public interface ISiteRepository: IRepository<Site>
    {
    }

    public class SiteRepository: ISiteRepository
    {
        private readonly IContextFactory contextFactory;

        public SiteRepository(IContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }
            this.contextFactory = contextFactory;
        }

        public Site GetById(int id)
        {
            using (var context = contextFactory.Get())
            {
                return context.Sites.SingleOrDefault(s=>s.SiteId == id);
            }
        }

        public IEnumerable<Site> GetAll()
        {
            using (var context = contextFactory.Get())
            {
                return context.Sites.ToList();
            }
        }

        public IEnumerable<Site> GetMany(Expression<Func<Site, bool>> @where)
        {
            using (var context = contextFactory.Get())
            {
                return context.Sites.Where(where).ToList();
            }
        }

        public Site Add(Site entity)
        {
            using (var context = contextFactory.Get())
            {
                var site = context.Sites.Add(entity);
                context.SaveChanges();
                return site.Entity;
            }
        }

        public void Update(Site entity)
        {
            using (var context = contextFactory.Get())
            {
                context.Sites.Update(entity);
                context.SaveChanges();
            }
        }

        public void Delete(Site entity)
        {
            using (var context = contextFactory.Get())
            {
                var site = context.Sites.SingleOrDefault(s=>s.SiteId == entity.SiteId);

                if (site != null)
                {
                    context.Sites.Remove(site);
                    context.SaveChanges();
                }
            }
        }
    }
}