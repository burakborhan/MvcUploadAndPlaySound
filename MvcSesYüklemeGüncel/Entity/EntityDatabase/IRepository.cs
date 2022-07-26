using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcSesYüklemeGüncel.Entity.EntityDatabase
{
    public interface IRepository<T> where T : class
    {
        List<T> List();

        List<T> List(Expression<Func<T, bool>> where);

        IQueryable<T> ListQueryable();

        void Insert(T obj);


        T GetById(int id);

        T Find(Expression<Func<T, bool>> where);
    }
}