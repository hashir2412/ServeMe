using ServeMe.Models;
using ServeMe.Repository;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class LoginDomain : ILoginDomain
    {
        private readonly ICredsRepository _credsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVendorRepository _vendorRepository;

        public LoginDomain(ICredsRepository credsRepository, IUserRepository userRepository, IVendorRepository vendorRepository)
        {
            _credsRepository = credsRepository;
            _userRepository = userRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<ResponseBaseModel<BaseUserVendorDto>> Login(string username, string password, bool isCustomer)
        {
            var result = await _credsRepository.Login(username, password, isCustomer);
            if (result.StatusCode == 0)
            {
                if (isCustomer)
                {
                    var user = await _userRepository.GetUserDetails(username);
                    return new ResponseBaseModel<BaseUserVendorDto>()
                    {
                        StatusCode = 0,
                        Body = user.Body,
                        Message = "Success"
                    };
                } else
                {
                    var user = await _vendorRepository.GetVendorDetails(username);
                    return new ResponseBaseModel<BaseUserVendorDto>()
                    {
                        StatusCode = 0,
                        Body = user.Body,
                        Message = "Success"
                    };
                }
            } else
            {
                return new ResponseBaseModel<BaseUserVendorDto>()
                {
                    StatusCode = result.StatusCode,
                    Body = null,
                    Message = result.Message
                };
            }
        }

    }
}
