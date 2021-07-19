using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Model;

namespace Database.Repository
{
    public interface IPersonRepository : IRepository<Persons>
    {
        Task<Persons> GetById(int id);
        Task<IEnumerable<Persons>> GetListWithNav(int offset, int limit);
        Task<IEnumerable<Persons>> Search(string term);
    }
}