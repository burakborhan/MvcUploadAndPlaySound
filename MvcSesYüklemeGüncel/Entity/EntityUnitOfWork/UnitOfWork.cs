using MvcSesYüklemeGüncel.Entity.EntityDatabase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcSesYüklemeGüncel.Entity.EntityUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SesContext _dbContext;

        public UnitOfWork(SesContext dbContext)
        {
            //dbsetet classını çağır.
            

            //Database.SetInitializer<HedefDBContext>(null);

            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;

            // Buradan istediğimiz gibi EntityFramework'ü konfigure ederiz.
            //_dbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }


        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw new Exception("db saveChanges hatası alındı!");
            }
        }

        //IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//Çöp toplayıcısı (garbage collector) 
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            throw new NotImplementedException();
        }
    }
}