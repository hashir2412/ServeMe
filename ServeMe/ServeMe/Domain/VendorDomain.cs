using ServeMe.Models;
using ServeMe.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class VendorDomain : IVendorDomain
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly ICredsRepository _credsRepository;
        private readonly IOrderRepository _orderRepository;
        public VendorDomain(IVendorRepository userRepository, ICredsRepository credsRepository, IOrderRepository orderRepository)
        {
            _vendorRepository = userRepository;
            _credsRepository = credsRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id)
        {
            return await _vendorRepository.GetVendorDashboardDetails(id);
        }

        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id)
        {
            return await _vendorRepository.GetVendorDetails(id);
        }

        public async Task<ResponseBaseModel<int>> PlaceBid(BidDto bid)
        {
            return await _vendorRepository.PlaceBid(bid);
        }

        public async Task<ResponseBaseModel<int>> UpdateBid(BidDto bid)
        {
            return await _vendorRepository.UpdateBid(bid);
        }

        public async Task<ResponseBaseModel<IEnumerable<CartDto>>> GetActiveBidsByVendor(int id)
        {
            return await _vendorRepository.GetActiveBidsByVendor(id);
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

        public async Task<ResponseBaseModel<int>> MarkOrderComplete(CartDto cartDto)
        {
            return await _orderRepository.MarkOrderComplete(cartDto);
        }

        public async Task<ResponseBaseModel<IEnumerable<VendorReviewRatingDto>>> GetVendorReviewRatingsDetails()
        {
            return await _vendorRepository.GetVendorReviewRatingsDetails();
        }
    }
}
