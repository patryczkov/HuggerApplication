using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly UserContext _userContext;

  
        public Repository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public IQueryable<T> FindAll()
        {
            return _userContext.Set<T>().AsNoTracking();
        }

        public void Create(T entity)
        {
            _userContext.Add(entity);
        }

        public void Delete(T entity)
        {
            _userContext.Remove(entity);
        }

        public void Update(T entity)
        {
            _userContext.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _userContext.SaveChangesAsync()) > 0;
        }
    }
}
