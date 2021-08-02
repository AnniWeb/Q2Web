using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("kittens")]
    public class KittenController : ControllerBase
    {
        private readonly ILogger<KittenController> _logger;
        private readonly IMapper _mapper;
        private readonly IKittenService _service;

        public KittenController(
            IKittenService service,
            ILogger<KittenController> logger,
            IMapper mapper
        )
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KittenResponse>> GetList()
        {
            try
            {
                _logger?.LogDebug("GetList");
                
                var data = await _service.GetList(0, 100);
                return _mapper.Map<IEnumerable<KittenResponse>>(data);
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
                Console.WriteLine(e);
                return new List<KittenResponse>();
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(KittenRequest newKitten)
        {
            try
            { 
                _logger?.LogDebug("Add", newKitten);
                var data = _service.Add(_mapper.Map<Kitten>(newKitten));

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
    }
}
