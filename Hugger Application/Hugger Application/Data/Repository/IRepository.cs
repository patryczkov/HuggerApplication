using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        void Create (T entity);
        void Update (T entity);
        void Delete (T entity);
        Task<bool> SaveChangesAsync();
    }
}
