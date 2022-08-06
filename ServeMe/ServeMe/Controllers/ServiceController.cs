using Microsoft.AspNetCore.Mvc;
using ServeMe.Domain;
using ServeMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServeMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceDomain _serviceDomain;

        public ServiceController(IServiceDomain userDomain)
        {
            _serviceDomain = userDomain;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<ResponseBaseModel<IEnumerable<ServiceCategoryDto>>> Get()
        {
            return await _serviceDomain.GetServices();
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<ResponseBaseModel<IEnumerable<ServiceDto>>> GetServicesByVendor(int id)
        {
            return await _serviceDomain.GetServicesByVendor(id);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public async Task<ResponseBaseModel<int>> Post(ServiceDto value)
        {
            return await _serviceDomain.Register(value);
        }

        // PUT api/<ServiceController>/5
        [HttpPut]
        public async Task<ResponseBaseModel<int>> Put(ServiceDto value)
        {
            return await _serviceDomain.Update(value);
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
