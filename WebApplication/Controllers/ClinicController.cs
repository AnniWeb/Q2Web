using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstractions.Model;
using BusinessLogic.Abstractions.Service;
using DataLayer.Abstractions;
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
        private readonly IClinicService _service;

        public ClinicController(
            IClinicService service,
            ILogger<ClinicController> logger,
            IMapper mapper
        )
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
        
        [HttpGet("/clinics")]
        [Description("Получение списка клиник с пагинацией")]
        public async Task<IEnumerable<ClinicResponse>> GetList([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            _logger?.LogDebug("GetList");
            
            try
            {
                if (offset < 0 || limit < 1)
                {
                    throw new ArgumentException("Не валидный запрос");
                }
                
                var data = await _service.GetList(offset, limit);
                return _mapper.Map<IEnumerable<ClinicResponse>>(data);
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
            }
            
            return new List<ClinicResponse>();
        }
        
        [HttpPost("/clinics")]
        [Description("Добавление новой клиники в коллекцию")]
        public async Task<ActionResult> Add([FromBody] ClinicRequest clinic)
        {
            _logger?.LogDebug("Add", clinic);
            
            try
            {
                var data = await _service.Add(_mapper.Map<Clinic>(clinic));

                if (data != null)
                {
                    return new CreatedResult("Ok", data);
                }
            }
            catch (Exception e)
            {
                _logger?.LogError("Add", e);
            }

            return Problem();
        }
        
        [HttpPut("/clinics")]
        [Description("Обновление существующей клиники в коллекции")]
        public async Task Update([FromBody] DbClinicRequest clinic)
        {
            _logger?.LogDebug("Update", clinic);
            
            try
            {
                await _service.Update(_mapper.Map<Clinic>(clinic));
            }
            catch (Exception e)
            {
                _logger?.LogError("Update", e);
            }
        }
        
        [HttpDelete("/clinics/{id}")]
        [Description("Удаление клиники из коллекции")]
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

                result.Clinic = _mapper.Map<ClinicResponse>(await _service.GetById(id));

                if (result.Clinic != null)
                {
                    var patients = await _service.GetPatients(id, new Paginator {Offset = offset, Limit = limit});
                    result.Patients = _mapper.Map<IEnumerable<PersonResponse>>(patients);
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
                var person = await _service.AttachPatient(clinic, patient);

                if (person == null)
                {
                    return NotFound();
                }
                return Ok(person.Id);
            }
            catch (Exception e)
            {
                _logger?.LogError("AttachPatientToClinic", e);
            }

            return NotFound();
        }
    }
}