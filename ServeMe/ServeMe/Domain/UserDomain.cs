using ServeMe.Models;
using ServeMe.Repository;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;
        private readonly ICredsRepository _credsRepository;
        public UserDomain(IUserRepository userRepository, ICredsRepository credsRepository)
        {
            _userRepository = userRepository;
            _credsRepository = credsRepository;
        }

        public async Task<ResponseBaseModel<UserDto>> GetUserDetails(int id)
        {
            return await _userRepository.GetUserDetails(id);
        }

        public async Task<ResponseBaseModel<int>> Register(UserDto user, string password)
        {
            var userExists = await _credsRepository.UserExists(user.Email);
            if (userExists)
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "User already exists. Please login", StatusCode = 2 };
            }
            else
            {
                var res = await _credsRepository.Register(user.Email, password, true);
                if (res)
                {
                    return await _userRepository.Register(user);
                }
                else
                {
                    return new ResponseBaseModel<int>() { Body = -1, Message = "Failed to register", StatusCode = 1 };
                }
            }
        }

        public async Task<ResponseBaseModel<int>> AddReview(ReviewsRatingsRequestModel value)
        {
            return await _userRepository.AddReview(value);
        }
    }
}
