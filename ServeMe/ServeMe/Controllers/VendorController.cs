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
    public class VendorController : ControllerBase
    {
        private readonly IVendorDomain _vendorDomain;

        public VendorController(IVendorDomain vendorDomain)
        {
            _vendorDomain = vendorDomain;
        }
        // GET: api/<VendorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id)
        {
            return await _vendorDomain.GetVendorDetails(id);
        }


        [HttpGet("dashboard")]
        public async Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id)
        {
            return await _vendorDomain.GetVendorDashboardDetails(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ResponseBaseModel<int>> Register(RegisterVendorRequestModel registerVendorRequestModel)
        {
            if (ModelState.IsValid)
            {
                return await _vendorDomain.Register(registerVendorRequestModel.Vendor, registerVendorRequestModel.Password);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }

        }


        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VendorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
