using ServeMe.Models;
using ServeMe.Repository;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class VendorDomain : IVendorDomain
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly ICredsRepository _credsRepository;
        public VendorDomain(IVendorRepository userRepository, ICredsRepository credsRepository)
        {
            _vendorRepository = userRepository;
            _credsRepository = credsRepository;
        }

        public async Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id)
        {
            return await _vendorRepository.GetVendorDashboardDetails(id);
        }

        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id)
        {
            return await _vendorRepository.GetVendorDetails(id);
        }

        public async Task<ResponseBaseModel<int>> Register(VendorDto vendor, string password)
        {
            var userExists = await _credsRepository.UserExists(vendor.Email);
            if (userExists)
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Vendor already exists. Please login", StatusCode = 2 };
            }
            else
            {
                var res = await _credsRepository.Register(vendor.Email, password, false);
                if (res)
                {
                    return await _vendorRepository.Register(vendor);
                }
                else
                {
                    return new ResponseBaseModel<int>() { Body = -1, Message = "Failed to register", StatusCode = 1 };
                }
            }
        }
    }
}
