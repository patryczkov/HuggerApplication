using Hugger_Web_Application.Models;

namespace Hugger_Application.Models.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByID(int userID);
    }
}