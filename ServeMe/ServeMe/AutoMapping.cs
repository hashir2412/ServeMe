using AutoMapper;
using ServeMe.Repository.Models;
using ServeMe.Models;
namespace ServeMe
{


    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserDbModel, UserDto>()
    .ForMember(dest => dest.Name, input => input.MapFrom(i => i.UserName))
    .ReverseMap();
            CreateMap<ServiceDbModel, ServiceDto>().ReverseMap();
            CreateMap<CartDbModel, CartDto>().ReverseMap();
            CreateMap<ServiceCategoryDbModel, ServiceCategoryDto>().ReverseMap();
            CreateMap<ReviewsRatingsDbModel, ReviewsRatingsRequestModel>().ReverseMap();
            CreateMap<BidDbModel, BidDto>().ReverseMap();
            //         CreateMap<UserDbModel, UserDto>()
            //.ForMember(dest => dest.UserId, input => input.MapFrom(i => i.UserID))
            //.ReverseMap();
            //        CreateMap<UserDto, UserDbModel>()
            //.ForMember(dest => dest.UserName, input => input.MapFrom(i => i.Name))
            //.ReverseMap();
            //         CreateMap<UserDto, UserDbModel>()
            //.ForMember(dest => dest.UserID, input => input.MapFrom(i => i.UserID))
            //.ReverseMap();
            //        CreateMap<VendorDbModel, VendorDto>()
            //.ForMember(dest => dest.Name, input => input.MapFrom(i => i.VendorName))
            //.ReverseMap();
            CreateMap<VendorDbModel, VendorDto>()
    .ForMember(dest => dest.Name, input => input.MapFrom(i => i.VendorName))
    .ReverseMap();

            //        CreateMap<VendorDto, VendorDbModel>()
            //.ForMember(dest => dest.VendorName, input => input.MapFrom(i => i.Name))
            //.ReverseMap();
            //CreateMap<VendorDbModel, VendorDto>().ForMember(dest => dest.Name, a => a.MapFrom(src => src.VendorName));
            //CreateMap<VendorDbModel, VendorDto>().ForMember(dest => dest.VendorId, a => a.MapFrom(src => src.VendorID));
            //CreateMap<VendorDto, VendorDbModel>().ForMember(dest => dest.VendorID, a => a.MapFrom(src => src.VendorId));
            //CreateMap<VendorDto, VendorDbModel>().ForMember(dest => dest.VendorName, a => a.MapFrom(src => src.Name));
        }
    }
}
