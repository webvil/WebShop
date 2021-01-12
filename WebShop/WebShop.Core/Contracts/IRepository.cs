//using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Linq.Expressions;
using WebShop.Core.Models;

namespace WebShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        //IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Insert(T t);
        void Update(T t);
    }
}