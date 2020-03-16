using AutoMapper;
using ProjectApi.DataTransferObjects;
using ProjectApi.Models;

namespace ProjectApi.Helpers
{
   public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Farmer, FarmerForListDTO>();
            CreateMap<Farmer, FarmerDetailsDTO>();


            CreateMap<Inventory, InventoryForListDTO>();

             CreateMap<EOP, EOPForListDTO>();

            CreateMap<User, UserForListDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>();

            CreateMap<UserForDetailedDto, User>();
            CreateMap<User,UserForDetailedDto >();

            CreateMap<State, StateForCreationDto>();
            CreateMap<StateForCreationDto, State>();

            CreateMap<State, StateDto>();
            CreateMap<StateDto, State>();

                
               
        }
    }
}