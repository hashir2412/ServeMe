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
        private readonly IOrderDomain _orderDomain;

        public VendorController(IVendorDomain vendorDomain, IOrderDomain orderDomain)
        {
            _vendorDomain = vendorDomain;
            _orderDomain = orderDomain;
        }
        // GET: api/<VendorController>
        [HttpGet]
        public async Task<ResponseBaseModel<IEnumerable<VendorReviewRatingDto>>> GetAsync()
        {
            return await _vendorDomain.GetVendorReviewRatingsDetails();
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

        [HttpGet("order")]
        public async Task<ResponseBaseModel<IEnumerable<CartDto>>> GetVendorOrders(int id)
        {
            return await _orderDomain.GetOrdersByVendor(id);
        }

        [HttpGet("activebid")]
        public async Task<ResponseBaseModel<IEnumerable<CartDto>>> GetActiveBidsByVendor(int id)
        {
            if (ModelState.IsValid)
            {
                return await _vendorDomain.GetActiveBidsByVendor(id);
            }
            else
            {
                return new ResponseBaseModel<IEnumerable<CartDto>>() { Body = new List<CartDto>(), Message = "Error", StatusCode = 1 };
            }

        }

        [HttpPost("bid")]
        public async Task<ResponseBaseModel<int>> PlaceBid(BidDto bid)
        {
            if (ModelState.IsValid)
            {
                return await _vendorDomain.PlaceBid(bid);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }

        }

        [HttpPut("bid")]
        public async Task<ResponseBaseModel<int>> UpdateBid(BidDto bid)
        {
            if (ModelState.IsValid)
            {
                return await _vendorDomain.UpdateBid(bid);
            }
            else
            {
                return new ResponseBaseModel<int>() { Body = -1, Message = "Error", StatusCode = 1 };
            }

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

        [HttpPost("ordercomplete")]
        public async Task<ResponseBaseModel<int>> MarkOrderComplete(CartDto cart)
        {
            if (ModelState.IsValid)
            {
                return await _vendorDomain.MarkOrderComplete(cart);
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
