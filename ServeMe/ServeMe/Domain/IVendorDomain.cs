using ServeMe.Models;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface IVendorDomain
    {
        Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id);

        Task<ResponseBaseModel<int>> Register(VendorDto vendor, string password);

        Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id);

    }
}
