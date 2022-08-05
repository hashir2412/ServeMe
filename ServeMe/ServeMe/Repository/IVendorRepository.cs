using ServeMe.Models;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface IVendorRepository
    {
        Task<ResponseBaseModel<int>> Register(VendorDto user);
        Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id);
        Task<ResponseBaseModel<VendorDto>> GetVendorDetails(string email);
        Task<ResponseBaseModel<VendorDto>> Login(string email, string password);
        Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id);

    }
}
