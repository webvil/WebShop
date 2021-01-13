using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Contracts;
using WebShop.Core.Models;

namespace WebShop.DataAccess.SQL
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context; // so it is accessible inside the DataAccess.SQL project
        internal DbSet<T> dbSet;

        public SqlRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var entity = Find(Id);
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
    
