using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Model;
using DataLayer.Abstractions;
using DataLayer.Abstractions.Repository;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.RestRequest;
using WebApplication.RestResponse;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("clinics")]
    [Description("Клиники")]
    public class ClinicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ClinicController> _logger;
        private readonly IClinicRepository _repository;

        public ClinicController(
            ILogger<ClinicController> logger,
            ApplicationDataContext context, 
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = new ClinicRepository(context);
            _mapper = mapper;
        }
        
        [HttpGet("/clinics")]
        [Description("Получение списка клиник с пагинацией")]
        public async Task<IEnumerable<ClinicResponse>> GetList([FromQuery] int offset = 5, [FromQuery] int limit = 10)
        {
            _logger?.LogDebug("GetList");
            
            var list = new List<ClinicResponse>();
            try
            {
                if (offset < 0 || limit < 1)
                {
                    throw new ArgumentException("Не валидный запрос");
                }
                
                foreach (var person in await _repository.GetList(new Paginator{Offset = offset, Limit = limit}))
                {
                    list.Add(_mapper.Map<ClinicResponse>(person));
                }
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
            }
            
            return list;
        }
        
        [HttpPost("/clinics")]
        [Description("Добавление новой клиники в коллекцию")]
        public async Task<IActionResult> Add([FromBody] ClinicRequest clinic)
        {
            _logger?.LogDebug("Add", clinic);
            
            try
            {
                await _repository.Add(_mapper.Map<Clinic>(clinic));
                return new CreatedResult("Ok", true);
            }
            catch (Exception e)
            {
                _logger?.LogError("Add", e);
            }
            
            return null;
        }
        
        [HttpPut("/clinics")]
        [Description("Обновление существующей клиники в коллекции")]
        public async Task<IActionResult> Update([FromBody] DbClinicRequest clinic)
        {
            _logger?.LogDebug("Update", clinic);
            
            try
            {
                await _repository.Update(_mapper.Map<Clinic>(clinic));
                return Ok();
            }
            catch (Exception e)
            {
                _logger?.LogError("Update", e);
            }
            
            return NotFound();
        }
        
        [HttpDelete("/clinics/{id}")]
        [Description("Удаление клиники из коллекции")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            _logger?.LogDebug("Delete", id);
            
            try
            {
                await _repository.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger?.LogError("Delete", e);
            }
            
            return NotFound();
        }

        [HttpGet("/clinics/{id}/patients")]
        public async Task<ListPatientsOfClinicResponse> ListPatientsOfClinic([FromRoute] int id, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            _logger?.LogDebug("ListPatientsOfClinic");
            
            var result = new ListPatientsOfClinicResponse();
            try
            {
                if (offset < 0 || limit < 1)
                {
                    throw new ArgumentException("Не валидный запрос");
                }

                result.Clinic = _mapper.Map<ClinicResponse>(await _repository.GetById(id));

                if (result.Clinic != null)
                {
                    result.Patients = new List<PersonResponse>();
                    foreach (var person in await _repository.GetPatients(id, new Paginator{Offset = offset, Limit = limit}))
                    {
                        result.Patients.Add(_mapper.Map<PersonResponse>(person));
                    }
                }
                
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
            }
            
            return result;
        }

        [HttpPost("/clinics/{clinic}/attach-patient/{patient}")]
        public async Task<IActionResult> AttachPatientToClinic([FromRoute] int clinic, [FromRoute] int patient)
        {
            _logger?.LogDebug("AttachPatientToClinic");
            
            try
            {
                await _repository.AttachPatient(clinic, patient);

                return Ok();
            }
            catch (Exception e)
            {
                _logger?.LogError("AttachPatientToClinic", e);
            }

            return NotFound();
        }
    }
}