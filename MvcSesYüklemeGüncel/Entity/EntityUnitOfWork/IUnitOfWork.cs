using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSesYüklemeGüncel.Entity.EntityDatabase;

namespace MvcSesYüklemeGüncel.Entity.EntityUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}