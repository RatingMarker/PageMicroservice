using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PageMicroservice.Api.Infrastructure
{
    public interface IRepository<T> where T: class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        T Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}