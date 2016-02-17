using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PageMicroservice.Api.Infrastructure;
using PageMicroservice.Api.Models;

namespace PageMicroservice.Api.Repositories
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
                return context.Sites.SingleOrDefault(s => s.SiteId == id);
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

        public bool Update(Site entity)
        {
            bool isUpdated = false;

            using (var context = contextFactory.Get())
            {
                context.Sites.Update(entity);
                int commit = context.SaveChanges();

                if (commit > 0)
                {
                    isUpdated = true;
                }
            }
            return isUpdated;
        }

        public bool Delete(Site entity)
        {
            bool isDeleted = false;

            using (var context = contextFactory.Get())
            {
                var site = context.Sites.FirstOrDefault(s => s.SiteId == entity.SiteId);

                if (site != null)
                {
                    context.Sites.Remove(site);
                    int commit = context.SaveChanges();

                    if (commit > 0)
                    {
                        isDeleted = true;
                    }
                }
            }
            return isDeleted;
        }
    }
}