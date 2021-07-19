using System.Collections.ObjectModel;
using AutoMapper;
using Database.Model;
using WebApplication.RestRequest;
using WebApplication.RestResponse;

namespace WebApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Kittens, KittenResponse>();
            CreateMap<KittenRequest, Kittens>();
            CreateMap<DbPersonRequest, Persons>();
            CreateMap<PersonRequest, Persons>();
            CreateMap<Persons, PersonResponse>();
            CreateMap<DbClinicRequest, Clinic>();
            CreateMap<ClinicRequest, Clinic>();
            CreateMap<Clinic, ClinicResponse>();
        }
    }
}