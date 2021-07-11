using System;
using System.Collections.Generic;
using System.ComponentModel;
using AutoMapper;
using Database;
using Database.Model;
using Database.Repository;
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
        private IMapper _mapper;
        private ILogger<PersonsController> _logger;
        private IPersonRepository _repository;

        public PersonsController(
            ILogger<PersonsController> logger,
            ApplicationDataContext context, 
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = new PersonRepository(context);
            _mapper = mapper;
        }

        [HttpGet("/persons/{id}")]
        [Description("Получение человека по идентификатору")]
        public PersonResponse GetById([FromRoute] int id)
        {
            _logger?.LogDebug("GetById", id);
            
            try
            {
                if (id < 1)
                {
                    throw new ArgumentException("Невалидный ид");
                }
                
                return _mapper.Map<PersonResponse>(_repository.GetById(id));
            }
            catch (Exception e)
            {
                _logger?.LogError("GetById", e);
            }

            return null;
        }
        
        [HttpGet("/persons/search")]
        [Description("Поиск человека по имени")]
        public IEnumerable<PersonResponse> SearchByTerm([FromQuery] string term)
        {
            _logger?.LogDebug("SearchByTerm", term);
            
            var list = new List<PersonResponse>();
            try
            {
                if (term == null || term.Length < 1)
                {
                    throw new ArgumentException("Не заполнен запрос");
                }
                
                foreach (var person in _repository.Search(term))
                {
                    list.Add(_mapper.Map<PersonResponse>(person));
                }
            }
            catch (Exception e)
            {
                _logger?.LogError("SearchByTerm", e);
            }
            
            return list;
        }
        
        [HttpGet("/persons")]
        [Description("Получение списка людей с пагинацией")]
        public IEnumerable<PersonResponse> GetList([FromQuery] int offset = 5, [FromQuery] int limit = 10)
        {
            _logger?.LogDebug("GetList");
            
            var list = new List<PersonResponse>();
            try
            {
                if (offset < 0 || limit < 1)
                {
                    throw new ArgumentException("Не валидный запрос");
                }
                
                foreach (var person in _repository.GetListWithNav(offset, limit))
                {
                    list.Add(_mapper.Map<PersonResponse>(person));
                }
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
            }
            
            return list;
        }
        
        [HttpPost("/persons")]
        [Description("Добавление новой персоны в коллекцию")]
        public IActionResult Add([FromBody] PersonRequest newPerson)
        {
            _logger?.LogDebug("Add", newPerson);
            
            try
            {
                return new CreatedResult("Ok", _repository.Add(_mapper.Map<Persons>(newPerson)));
            }
            catch (Exception e)
            {
                _logger?.LogError("Add", e);
            }
            
            return null;
        }
        
        [HttpPut("/persons")]
        [Description("Обновление существующей персоны в коллекции")]
        public IActionResult Update([FromBody] DbPersonRequest newPerson)
        {
            _logger?.LogDebug("Update", newPerson);
            
            try
            {
                return new CreatedResult("Ok", _repository.Update(_mapper.Map<Persons>(newPerson)));
            }
            catch (Exception e)
            {
                _logger?.LogError("Update", e);
            }
            
            return NotFound();
        }
        
        [HttpDelete("/persons/{id}")]
        [Description("Удаление персоны из коллекции")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger?.LogDebug("Delete", id);
            
            try
            {
                if (_repository.Delete(id))
                {
                    return Ok();
                };
            }
            catch (Exception e)
            {
                _logger?.LogError("Delete", e);
            }
            
            return NotFound();
        }
    }
}