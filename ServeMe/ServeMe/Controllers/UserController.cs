using Microsoft.AspNetCore.Mvc;
using ServeMe.Domain;
using ServeMe.Models;
using System;
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

        [HttpGet("order")]
        public async Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetUserOrders(int id)
        {
            return await _orderDomain.GetOrdersByUser(id);
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

        [HttpPost("cancelorder")]
        public async Task<ResponseBaseModel<int>> CancelOrder(int cartId)
        {
            if (ModelState.IsValid)
            {
                return await _orderDomain.CancelCart(cartId);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }

        }



        // PUT api/<UserController>/5
        [HttpPost("modifyorder")]
        public async Task<ResponseBaseModel<int>> ModifyOrder(ModifyOrderRequestModel modifyOrderRequestModel)
        {
            if (ModelState.IsValid)
            {
                return await _orderDomain.ModifyCart(modifyOrderRequestModel.CartId, modifyOrderRequestModel.DateTime);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }
        }

        [HttpPost("confirmbid")]
        public async Task<ResponseBaseModel<int>> ConfirmBid(BidDto bidDto)
        {
            if (ModelState.IsValid)
            {
                return await _orderDomain.ConfirmBid(bidDto);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("review")]
        public async Task<ResponseBaseModel<int>> Post(ReviewsRatingsRequestModel value)
        {
            return await _userDomain.AddReview(value);
        }
    }
}
