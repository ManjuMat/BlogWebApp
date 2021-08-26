using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookDALayer;
using Microsoft.EntityFrameworkCore;

namespace BookRepoLayer
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private BookDbContext  myContext = null;
        private DbSet<T> table = null;

        public RepositoryBase()
        {
            this.myContext = new BookDbContext();
            table = myContext.Set<T>();
        }

        public RepositoryBase(BookDbContext _context)
        {
            this.myContext = _context;
            table = _context.Set<T>();
        }

        void IRepositoryBase<T>.Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        IEnumerable<T> IRepositoryBase<T>.GetAll()
        {
            return table.ToList();
        }

        T IRepositoryBase<T>.GetById(object id)
        {
            return table.Find(id);
        }

        void IRepositoryBase<T>.Insert(T obj)
        {
            table.Add(obj);
        }

        void IRepositoryBase<T>.Save()
        {
            myContext.SaveChanges();
        }

        void IRepositoryBase<T>.Update(T obj)
        {
            table.Attach(obj);
            myContext.Entry(obj).State = EntityState.Modified;
        }
    }
}