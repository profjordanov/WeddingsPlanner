using System.Threading.Tasks;
using WeddingsPlanner.Core.Models;
using Optional;

namespace WeddingsPlanner.Core.Services
{
    public interface IUsersService
    {
        Task<Option<JwtModel, Error>> Login(LoginUserModel model);

        Task<Option<UserModel, Error>> Register(RegisterUserModel model);
    }
}
