using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MvcSesYüklemeGüncel.Entity.EntityModel;

namespace MvcSesYüklemeGüncel.Entity.EntityDatabase
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        //gelen  modeli _dbSet nesnesine ata
        public Repository(SesContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext null olamaz.");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        //listeleme
        public List<T> List()
        {
            return _dbSet.ToList();
        }

        //şarta göre listeleme
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        //sql cümleciğine göre listeleme
        public IQueryable<T> ListQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        //insert metodu
        public void Insert(T obj)
        {
            _dbSet.Add(obj);

        }

        //Update metodu

        //delete metodu


        //idye göre find
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        //Find şarta göre
        public T Find(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        //public List<T> List(Expression<Func<T, bool>> where)
        //{
        //    throw new NotImplementedException();
        //}

        //public T Find(Expression<Func<T, bool>> where)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
