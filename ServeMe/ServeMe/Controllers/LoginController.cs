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
    public class LoginController : ControllerBase
    {
        // GET: api/<LoginController>
        private readonly ILoginDomain _loginDomain;

        public LoginController(ILoginDomain loginDomain)
        {
            _loginDomain = loginDomain;
        }

        [HttpGet()]
        public async Task<ResponseBaseModel<BaseUserVendorDto>> Get(string email, string password,bool isCustomer)
        {
            return await _loginDomain.Login(email, password,isCustomer);
        }
    }
}
