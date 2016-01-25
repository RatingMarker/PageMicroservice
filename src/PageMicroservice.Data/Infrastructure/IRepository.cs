﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PageMicroservice.Data.Infrastructure
{
    public interface IRepository<T> where T: class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}