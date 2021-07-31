using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Model;
using DataLayer.Abstractions;
using Clinic = BusinessLogic.Abstractions.Model.Clinic;

namespace BusinessLogic.Abstractions.Service
{
    public interface IClinicService : IService<Clinic, int>
    {
        Task AttachPatient(int clinicId, int personId);
        Task<IEnumerable<Persons>> GetPatients(int clinicId, Paginator paginator);
    }
}