﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using PageMicroservice.Api.Infrastructure;
using PageMicroservice.Api.Models;

namespace PageMicroservice.Api.Repositories
{
    public interface IPageRepository: IRepository<Page>
    {
        int AddRange(IEnumerable<Page> pages);
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
                return context.Pages.Where(where).Take(500).ToList();
            }
        }

        public Page Add(Page entity)
        {
            using (var context = contextFactory.Get())
            {
                var page = context.Pages.Add(entity).Entity;
                context.SaveChanges();
                return page;
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
                var site = context.Pages.FirstOrDefault(x => x.PageId == entity.PageId);

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

        public int AddRange(IEnumerable<Page> pages)
        {
            int countSaved = 0;

            using (var context = contextFactory.Get())
            {
                context.AddRange(pages);
                countSaved += context.SaveChanges();
            }

            return countSaved;
        }
    }
}