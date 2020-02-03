using Hugger_Web_Application.Models;
using System.Linq;

namespace Hugger_Application.Models.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        // private readonly IUnitOfWork _unitOfWork;
        ApplicationContext ApplicationContext;

        public UserRepository(ApplicationContext applicationContext) : base (applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public User GetUserByID(int userID)
        {
            return ApplicationContext.Users.SingleOrDefault(z => z.Id == userID);
        }
    }
}
