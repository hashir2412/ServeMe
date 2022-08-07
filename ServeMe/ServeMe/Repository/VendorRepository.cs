using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<VendorRepository> _logger;
        private readonly IMapper _mapper;

        public VendorRepository(ILogger<VendorRepository> logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<ResponseBaseModel<VendorDashboardDto>> GetVendorDashboardDetails(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { UserID = id };
                var sql = "select Sum(0.8*Rate*Quantity) as 'Total',Cart.Date as 'Date' from Cart inner join Service on Cart.ServiceId=Service.ServiceId where VendorID = @UserID and Cart.StatusID = 2 group by Cart.Date";
                var result = await connection.QueryFirstOrDefaultAsync<Orders>(sql, parameters);
                //var userDto = _mapper.Map<VendorDto>(result);
                return new ResponseBaseModel<VendorDashboardDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 };
                //: new ResponseBaseModel<VendorDashboardDto>()
                //{
                //    Body = userDto,
                //    Message = "Success",
                //    StatusCode = 0
                //};
            }
        }

        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { UserID = id };
                var sql = "select * from Vendors where VendorID = @UserID";
                var result = await connection.QueryFirstOrDefaultAsync<VendorDbModel>(sql, parameters);
                var userDto = _mapper.Map<VendorDto>(result);
                return result == null ? new ResponseBaseModel<VendorDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 } : new ResponseBaseModel<VendorDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(string email)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email };
                var sql = "select * from Vendors where email = @Email";
                var result = await connection.QueryFirstOrDefaultAsync<VendorDbModel>(sql, parameters);
                var userDto = _mapper.Map<VendorDto>(result);
                return result == null ? new ResponseBaseModel<VendorDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 } : new ResponseBaseModel<VendorDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<VendorDto>> Login(string email, string password)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email, Password = password };
                var sql = "select * from Creds where Email = @Email and Password=@Password";
                var result = await connection.QueryFirstOrDefaultAsync<VendorDbModel>(sql, parameters);
                var userDto = _mapper.Map<VendorDto>(result);
                return result == null ? new ResponseBaseModel<VendorDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 } : new ResponseBaseModel<VendorDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<int>> PlaceBid(BidDto bid)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var sql = "INSERT INTO Bid (CartId,VendorId,Amount) VALUES(@CartId,@VendorId,@Amount);SELECT CAST(SCOPE_IDENTITY() as int)";
                var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, bid);
                return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added Bid", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add bid", StatusCode = 1 };
            }
        }

        public async Task<ResponseBaseModel<int>> UpdateBid(BidDto bid)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var sql = "Update Bid set Amount=@Amount where CartId=@CartId";
                var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, bid);
                return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Updated Bid", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to update bid", StatusCode = 1 };
            }
        }

        public async Task<ResponseBaseModel<int>> Register(VendorDto vendor)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var vendorDto = new VendorDto
                {
                    Name = vendor.Name,
                    Phone = vendor.Phone,
                    Email = vendor.Email,
                    ReceiveCommunication = vendor.ReceiveCommunication,
                    Address = vendor.Address,
                    Agreement = vendor.Agreement
                };
                var vendorDbModel = _mapper.Map<VendorDbModel>(vendorDto);
                var sql = "INSERT INTO Vendors (VendorName, Phone,Email,ReceiveCommunication,Address,Agreement) VALUES(@VendorName, @Phone,@Email,@ReceiveCommunication,@Address,@Agreement);SELECT CAST(SCOPE_IDENTITY() as int)";
                var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, vendorDbModel);
                return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Registered", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to register", StatusCode = 1 };
            }
        }

        public async Task<ResponseBaseModel<IEnumerable<CartDto>>> GetActiveBidsByVendor(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var sql = "select * from Cart inner join Service on Cart.ServiceCategoryId = Service.ServiceCategoryID inner join ServiceCategory on ServiceCategory.ServiceCategoryId = Service.ServiceCategoryId left join Bid on Bid.CartId = Cart.CartId where Cart.StatusID = 1 and Service.VendorId= @Id";
                var parameters = new { Id = id };

                var SalesCartList = await connection.QueryAsync<CartDbModel, ServiceCategoryDbModel, BidDbModel, CartDbModel>(sql,
                    (cart, serviceCategory, bid) =>
                    {
                        //if (bid != null)
                        //{
                        //    cart.Bids.Add(bid);
                        //}
                        //cart.S = order;
                        cart.ServiceCategory = serviceCategory;
                        if (bid != null)
                        {
                            cart.Bids.Add(bid);
                        }
                        return cart;
                    }, parameters, splitOn: "ServiceCategoryID,BidId"
                    );
                //List<OrderDto> result = new List<OrderDto>();
                //var salesCartGroupedList = SalesCartList.GroupBy(u => u.Order.OrderID)
                //                      .Select(grp => new { Id = grp.Key, Items = grp.ToList() })
                //                      .ToList();
                //foreach (var i in salesCartGroupedList)
                //{
                //    var cart = SalesCartList.FirstOrDefault(res => res.Order.OrderID == i.Id);
                //    OrderDto finalCart = new OrderDto();
                //    finalCart.Id = i.Id;
                //    finalCart.Items = new List<CartDto>();
                //    i.Items.ForEach(item =>
                //    {
                //        finalCart.Items.Add(_mapper.Map<CartDto>(item));
                //    });
                //    finalCart.AddressLine1 = cart.Order.AddressLine1;
                //    finalCart.AddressLine2 = cart.Order.AddressLine2;
                //    finalCart.State = cart.Order.State;
                //    finalCart.City = cart.Order.City;
                //    finalCart.Pincode = cart.Order.Pincode;
                //    finalCart.Date = cart.Order.Date;
                //    finalCart.Total = cart.Order.Total;
                //    finalCart.Name = cart.Order.Name;
                //    finalCart.Phone = cart.Order.Phone;
                //    result.Add(finalCart);
                //}
                var result = _mapper.Map<IEnumerable<CartDto>>(SalesCartList);
                return new ResponseBaseModel<IEnumerable<CartDto>>() { Body = result, Message = "Success", StatusCode = 0 };
            }
        }
    }
}
