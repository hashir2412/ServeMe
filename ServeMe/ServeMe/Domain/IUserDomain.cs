using ServeMe.Models;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface IUserDomain
    {
        Task<ResponseBaseModel<int>> Register(UserDto user,string password);

        Task<ResponseBaseModel<UserDto>> GetUserDetails(int id);
    }
}
