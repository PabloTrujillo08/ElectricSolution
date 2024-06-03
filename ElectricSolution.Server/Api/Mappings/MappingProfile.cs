using AutoMapper;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.DTO;

namespace ElectricSolution.Server.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region IdentityRegion
            CreateMap<Client, ClientRegisterRequestDTO>();
            CreateMap<ClientRegisterRequestDTO, Client>();

        //    CreateMap<RegisterModel, Address>();
            CreateMap<Address, AddressCreateRequestDTO>();
            CreateMap<AddressCreateRequestDTO, Address>();

            //CreateMap<AddressUpdateRequestDTO, Address>();
            //CreateMap<Address, AddressUpdateRequestDTO>();

            CreateMap<AddressUpdateRequestDTO, Client>();
            CreateMap<Client, AddressUpdateRequestDTO>();

            //CreateMap<AddressCreateRequestDTO, Address>()
            //         .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.ProvinceId));


            //CreateMap<ClientRegisterRequestDTO, Address>()
            //    .ForMember(dest => dest.Street, act => act.MapFrom(src => src.Address));

            //CreateMap<Address, ClientRegisterRequestDTO>()
            //   .ForMember(dest => dest.Address, act => act.MapFrom(src => src.Street));






            //CreateMap<RegisterModel, Client>()
            //                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => new List<Address> { new Address
            //                                    {Street = src.Address,DoorNumber = src.DoorNumber,City = src.City,ZipCode = src.ZipCode }}));



            //CreateMap<RegisterModel, Client>()
            //                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //                   .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //                   .AfterMap((src, dest) =>
            //                   {
            //                       foreach (var address in dest.Addresses)
            //                           address.Client = dest;
            //                   });

            #endregion IdentityRegion
        }
    }
}
