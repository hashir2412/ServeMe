using ServeMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface IServiceDomain
    {
        Task<ResponseBaseModel<int>> Register(ServiceDto service);

        Task<ResponseBaseModel<int>> Update(ServiceDto service);

        Task<ResponseBaseModel<IEnumerable<ServiceDto>>> GetServices();

        Task<ResponseBaseModel<IEnumerable<ServiceDto>>> GetServicesByVendor(int id);
    }
}
