using AutoMapper;
using Exchange.Data.Models;
using Exchange.Models;
using Exchange.Models.DTOs;

namespace Exchange.Api.Mappings
{
    public class Maps : Profile
    {
        public Maps() 
        {
            CreateMap<PurchaseDTO, Purchase>().ReverseMap();
            CreateMap<Purchase, PurchaseResponseDTO>();
            CreateMap<RateDTO, CurrencyRate>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
       
    }
}
