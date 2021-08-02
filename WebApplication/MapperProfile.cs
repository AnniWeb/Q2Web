using System.Collections.ObjectModel;
using AutoMapper;
using BusinessLogic.Abstractions.Model;
using Database.Model;
using WebApplication.RestRequest;
using WebApplication.RestResponse;
using ClinicBLL = BusinessLogic.Abstractions.Model.Clinic;
using ClinicDLL = Database.Model.Clinic;

namespace WebApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // BLL <=> Rest
            CreateMap<Kitten, KittenResponse>();
            CreateMap<KittenRequest, Kitten>();
            CreateMap<DbPersonRequest, Person>();
            CreateMap<PersonRequest, Person>();
            CreateMap<Person, PersonResponse>();
            CreateMap<DbClinicRequest, ClinicBLL>();
            CreateMap<ClinicRequest, ClinicBLL>();
            CreateMap<ClinicBLL, ClinicResponse>();
            
            // DLL <=> BLL
            CreateMap<Kitten, Kittens>();
            CreateMap<Kittens, Kitten>();
            CreateMap<Person, Persons>();
            CreateMap<Persons, Person>();
            CreateMap<ClinicDLL, ClinicBLL>();
            CreateMap<ClinicBLL, ClinicDLL>();
        }
    }
}