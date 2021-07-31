using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstractions.Model;
using BusinessLogic.Abstractions.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.RestRequest;
using WebApplication.RestResponse;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("persons")]
    [Description("Персоны")]
    public class PersonsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonService _service;

        public PersonsController(
            IPersonService service, 
            ILogger<PersonsController> logger,
            IMapper mapper
        )
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("/persons/{id}")]
        [Description("Получение человека по идентификатору")]
        public async Task<PersonResponse> GetById([FromRoute] int id)
        {
            _logger?.LogDebug("GetById", id);
            
            try
            {
                if (id < 1)
                {
                    throw new ArgumentException("Невалидный ид");
                }
                
                return _mapper.Map<PersonResponse>(await _service.GetById(id));
            }
            catch (Exception e)
            {
                _logger?.LogError("GetById", e);
            }

            return null;
        }
        
        [HttpGet("/persons/search")]
        [Description("Поиск человека по имени")]
        public async Task<IEnumerable<PersonResponse>> SearchByTerm([FromQuery] string term)
        {
            _logger?.LogDebug("SearchByTerm", term);
            
            var list = new List<PersonResponse>();
            try
            {
                if (term == null || term.Length < 1)
                {
                    throw new ArgumentException("Не заполнен запрос");
                }
                
                var data = await _service.SearchByTerm(term);
                return _mapper.Map<IEnumerable<PersonResponse>>(data);
            }
            catch (Exception e)
            {
                _logger?.LogError("SearchByTerm", e);
            }
            
            return list;
        }
        
        [HttpGet("/persons")]
        [Description("Получение списка людей с пагинацией")]
        public async Task<IEnumerable<PersonResponse>> GetList([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            _logger?.LogDebug("GetList");
            
            var list = new List<PersonResponse>();
            try
            {
                if (offset < 0 || limit < 1)
                {
                    throw new ArgumentException("Не валидный запрос");
                }
                
                var data = await _service.GetList(offset, limit);
                return _mapper.Map<IEnumerable<PersonResponse>>(data);
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
            }
            
            return list;
        }
        
        [HttpPost("/persons")]
        [Description("Добавление новой персоны в коллекцию")]
        public async Task Add([FromBody] PersonRequest newPerson)
        {
            _logger?.LogDebug("Add", newPerson);
            
            try
            {
                await _service.Add(_mapper.Map<Person>(newPerson));
            }
            catch (Exception e)
            {
                _logger?.LogError("Add", e);
            }
        }
        
        [HttpPut("/persons")]
        [Description("Обновление существующей персоны в коллекции")]
        public async Task Update([FromBody] DbPersonRequest newPerson)
        {
            _logger?.LogDebug("Update", newPerson);
            
            try
            {
                await _service.Update(_mapper.Map<Person>(newPerson));
            }
            catch (Exception e)
            {
                _logger?.LogError("Update", e);
            }
        }
        
        [HttpDelete("/persons/{id}")]
        [Description("Удаление персоны из коллекции")]
        public async Task Delete([FromRoute] int id)
        {
            _logger?.LogDebug("Delete", id);
            
            try
            {
                await _service.Delete(id);
            }
            catch (Exception e)
            {
                _logger?.LogError("Delete", e);
            }
        }
    }
}