using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hugger_Application.Models.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        //private readonly IUnitOfWork _unitOfWork;
        ApplicationContext ApplicationContext;

        public Repository(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public IQueryable<T> FindAll()
        {
            return ApplicationContext.Set<T>().AsNoTracking();
        }

        public void Create(T entity)
        {
            ApplicationContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ApplicationContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            ApplicationContext.Set<T>().Update(entity);
        }
        public void Save()
        {
            ApplicationContext.SaveChanges();
        }
}
}
