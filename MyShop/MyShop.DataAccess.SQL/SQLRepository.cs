using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        // the following is a constructor.
        public SQLRepository(DataContext context)
        {
            this.context = context;
            // this is a method of DbSet, which will return entity (Products or ProductCategories)
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
            var t = Find(Id);
            /* EntityState.Detached implies that the entity is not being tracked by context.
             So, we should attach it, which will place entity into context in unchanged state.
             And finally we can remove it. */
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);

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
            /* entity framework caches the data. so, it doesn't impact database directly.
             hence we have two steps:
             1) dbSet.attach() will place entity into context in 'unchanged' state.
             2) enitityState.Modified will change the value in the database. */
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
