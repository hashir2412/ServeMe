using ServeMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface IVendorRepository
    {
        Task<ResponseBaseModel<int>> Register(VendorDto user);

        Task<ResponseBaseModel<IEnumerable<VendorDto>>> GetVendors();
        Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id);
        Task<ResponseBaseModel<int>> PlaceBid(BidDto bid);

        Task<ResponseBaseModel<int>> UpdateBid(BidDto bid);

        Task<ResponseBaseModel<VendorDto>> GetVendorDetails(string email);
        Task<ResponseBaseModel<VendorDto>> Login(string email, string password);
        Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id);

        Task<ResponseBaseModel<IEnumerable<CartDto>>> GetActiveBidsByVendor(int id);

        Task<ResponseBaseModel<IEnumerable<VendorReviewRatingDto>>> GetVendorReviewRatingsDetails();

    }
}
