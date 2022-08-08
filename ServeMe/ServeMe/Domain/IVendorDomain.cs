using ServeMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface IVendorDomain
    {
        Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id);

        Task<ResponseBaseModel<int>> UpdateBid(BidDto bid);

        Task<ResponseBaseModel<int>> PlaceBid(BidDto bid);

        Task<ResponseBaseModel<int>> Register(VendorDto vendor, string password);

        Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id);

        Task<ResponseBaseModel<IEnumerable<CartDto>>> GetActiveBidsByVendor(int id);

        Task<ResponseBaseModel<int>> MarkOrderComplete(CartDto cartDto);


    }
}
