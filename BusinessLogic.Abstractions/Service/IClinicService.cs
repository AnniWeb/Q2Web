using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.Model;
using Database.Model;
using DataLayer.Abstractions;
using Clinic = BusinessLogic.Abstractions.Model.Clinic;

namespace BusinessLogic.Abstractions.Service
{
    public interface IClinicService : IService<Clinic, int>
    {
        Task<Person> AttachPatient(int clinicId, int personId);
        Task<IEnumerable<Person>> GetPatients(int clinicId, Paginator paginator);
    }
}