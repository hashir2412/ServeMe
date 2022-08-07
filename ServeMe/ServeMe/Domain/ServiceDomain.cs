using ServeMe.Models;
using ServeMe.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class ServiceDomain : IServiceDomain
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceDomain(IServiceRepository userRepository)
        {
            _serviceRepository = userRepository;
        }
        public async Task<ResponseBaseModel<IEnumerable<ServiceCategoryDto>>> GetServices()
        {
            return await _serviceRepository.GetServices();
        }

        public async Task<ResponseBaseModel<IEnumerable<ServiceCategoryDto>>> GetServicesByVendor(int id)
        {
            return await _serviceRepository.GetServicesByVendor(id);
        }

        public async Task<ResponseBaseModel<int>> Register(ServiceDto service)
        {
            return await _serviceRepository.Register(service);
        }

        public async Task<ResponseBaseModel<int>> Update(ServiceDto service)
        {
            return await _serviceRepository.Update(service);
        }
    }
}
