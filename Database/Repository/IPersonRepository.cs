using System.Collections;
using System.Collections.Generic;
using Database.Model;

namespace Database.Repository
{
    public interface IPersonRepository : IRepository<Persons>
    {
        Persons GetById(int id);
        IEnumerable<Persons> GetListWithNav(int offset, int limit);
        IEnumerable<Persons> Search(string term);
    }
}