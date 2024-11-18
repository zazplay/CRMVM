using AutoMapper;
using CRMVM_DAL.Models.Entities;
using CRMVM_BLL.DTO;
using CRMVM_Back_Presentation.Models.Requests;
using CRMVM_Back_Presentation.Models.Responses;

namespace CRMVM_BLL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Deal, DealDTO>().ReverseMap();

            CreateMap<DealDTO, DealResponse>().ReverseMap();
            CreateMap<ClientDTO, Client>().ReverseMap();
            CreateMap<CreateClientRequest, ClientDTO>();
            CreateMap<ClientDTO, ClientResponse>().ReverseMap();

            CreateMap<CreateDealRequest, DealDTO>();
            CreateMap<UpdateDealRequest, DealDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}