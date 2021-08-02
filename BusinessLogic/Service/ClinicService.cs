using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstractions.Model;
using BusinessLogic.Abstractions.Service;
using Database.Model;
using DataLayer.Abstractions;
using DataLayer.Abstractions.Repository;
using ClinicDLL = BusinessLogic.Abstractions.Model.Clinic;
using ClinicBLL = Database.Model.Clinic;

namespace BusinessLogic.Service
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _repository;
        private readonly IMapper _mapper;

        public ClinicService(IClinicRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<int> Add(ClinicDLL entity)
        {
            var data = await _repository.Add(_mapper.Map<ClinicBLL>(entity)); 
            return data.Id;
        }

        public Task Update(ClinicDLL entity)
        {
            return _repository.Update(_mapper.Map<ClinicBLL>(entity));
        }

        public Task Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<ClinicDLL> GetById(int id)
        {
            return _mapper.Map<ClinicDLL>(await _repository.GetById(id));
        }

        public async Task<IReadOnlyCollection<ClinicDLL>> GetList(int offset, int limit)
        {
            var data = await _repository.GetList(new Paginator(){Limit = limit, Offset = offset});
            return data.Select(_mapper.Map<ClinicDLL>).ToList();
        }

        public Task<IReadOnlyCollection<ClinicDLL>> SearchByTerm(string term)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Person> AttachPatient(int clinicId, int personId)
        {
            return _mapper.Map<Person>(await _repository.AttachPatient(clinicId, personId));
        }

        public async Task<IEnumerable<Person>> GetPatients(int clinicId, Paginator paginator)
        {
            var data = await _repository.GetPatients(clinicId, paginator);
            return data.Select(_mapper.Map<Person>).ToList(); 
        }
    }
}