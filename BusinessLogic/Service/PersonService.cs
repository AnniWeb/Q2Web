using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstractions.Model;
using BusinessLogic.Abstractions.Service;
using Database.Model;
using DataLayer.Abstractions.Repository;

namespace BusinessLogic.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<int> Add(Person entity)
        {
            var data = await _repository.Add(_mapper.Map<Persons>(entity));
            return data.Id;
        }

        public Task Update(Person entity)
        {
            return _repository.Update(_mapper.Map<Persons>(entity));
        }

        public Task Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<Person> GetById(int id)
        {
            return _mapper.Map<Person>(await _repository.GetById(id));
        }

        public async Task<IReadOnlyCollection<Person>> GetList(int offset, int limit)
        {
            var data = await _repository.GetListWithNav(offset, limit);
            return data.Select(_mapper.Map<Person>).ToList();
        }

        public async Task<IReadOnlyCollection<Person>> SearchByTerm(string term)
        {
            var data = await _repository.Search(term);
            return data.Select(_mapper.Map<Person>).ToList();
        }
    }
}