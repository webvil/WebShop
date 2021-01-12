using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Contracts;
using WebShop.Core.Models;

namespace WebShop.DataAccess.SQL
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext context;
        readonly string className;

        public SqlRepository(DataContext context)
        {
            this.context = context;
            className = typeof(T).Name;
        }
        public IQueryable<T> Collection()
        {
            return context.Set<T>();

        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var entity = Find(Id);
            context.Set<T>().Remove(entity);
            Commit();
        }

        public T Find(string Id)
        {
            T t = context.Set<T>().Find(Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public void Insert(T t)
        {
            context.Set<T>().Add(t);
            Commit();
        }

        public void Update(T t)
        {
            T tToUpdate = context.Set<T>().FirstOrDefault();
            if (tToUpdate != null)
            {
                tToUpdate = t;

            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}
    
