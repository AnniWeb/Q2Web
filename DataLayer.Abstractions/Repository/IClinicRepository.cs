using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Model;

namespace DataLayer.Abstractions.Repository
{
    public interface IClinicRepository : IRepository<Clinic>
    {
        Task<Persons> AttachPatient(int clinicId, int personId);
        Task<IEnumerable<Persons>> GetPatients(int clinicId, Paginator paginator);
        Task<IEnumerable<Clinic>> GetList(Paginator paginator);
    }
}