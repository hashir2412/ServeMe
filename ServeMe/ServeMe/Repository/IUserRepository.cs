using ServeMe.Models;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface IUserRepository
    {
        Task<ResponseBaseModel<int>> Register(UserDto user);
        Task<ResponseBaseModel<UserDto>> GetUserDetails(int id);

        Task<ResponseBaseModel<UserDto>> GetUserDetails(string email);
    }
}
