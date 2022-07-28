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
    public class UserController : ControllerBase
    {
        private readonly IUserDomain _userDomain;
        private readonly IOrderDomain _orderDomain;

        public UserController(IUserDomain userDomain, IOrderDomain paymentDomain)
        {
            _userDomain = userDomain;
            _orderDomain = paymentDomain;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ResponseBaseModel<UserDto>> GetUserDetails(int id)
        {
            return await _userDomain.GetUserDetails(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ResponseBaseModel<int>> Register(RegisterUserRequestModel registerUserRequestModel)
        {
            if (ModelState.IsValid)
            {
                return await _userDomain.Register(registerUserRequestModel.User, registerUserRequestModel.Password);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }

        }

        [HttpPost("order")]
        public async Task<ResponseBaseModel<int>> PlaceOrder(OrderRequestModel order)
        {
            if (ModelState.IsValid)
            {
                return await _orderDomain.PlaceOrder(order);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
