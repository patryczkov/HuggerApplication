using Hugger_Application.Models.TokenModels;

namespace Hugger_Application.Services.TokenServices
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string login, int userId, int userRoleId);
    }
}
