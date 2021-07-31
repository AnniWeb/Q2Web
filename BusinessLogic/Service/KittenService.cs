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
    public class KittenService : IKittenService
    {
        private readonly IKittenRepository _repository;
        private readonly IMapper _mapper;

        public KittenService(IKittenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task Add(Kitten entity)
        {
            return _repository.Add(_mapper.Map<Kittens>(entity));
        }

        public Task Update(Kitten entity)
        {
            return _repository.Update(_mapper.Map<Kittens>(entity));
        }

        public Task Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<Kitten> GetById(int id)
        {
            return _mapper.Map<Kitten>(await _repository.GetById(id));
        }

        public async Task<IReadOnlyCollection<Kitten>> GetList(int offset, int limit)
        {
            var data = await _repository.Get();
            return data.Select(_mapper.Map<Kitten>).ToList();
        }

        public Task<IReadOnlyCollection<Kitten>> SearchByTerm(string term)
        {
            throw new System.NotImplementedException();
        }
    }
}