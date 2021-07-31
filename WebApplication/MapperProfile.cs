using System.Collections.ObjectModel;
using AutoMapper;
using BusinessLogic.Abstractions.Model;
using Database.Model;
using WebApplication.RestRequest;
using WebApplication.RestResponse;
using ClinicDLL = BusinessLogic.Abstractions.Model.Clinic;
using ClinicBLL = Database.Model.Clinic;

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
            CreateMap<DbClinicRequest, ClinicBLL>();
            CreateMap<ClinicRequest, ClinicBLL>();
            CreateMap<ClinicBLL, ClinicResponse>();
            
            
            CreateMap<Kitten, Kittens>();
            CreateMap<Kittens, Kitten>();
            CreateMap<Person, Persons>();
            CreateMap<Persons, Person>();
            CreateMap<ClinicDLL, ClinicBLL>();
            CreateMap<ClinicBLL, ClinicDLL>();
        }
    }
}