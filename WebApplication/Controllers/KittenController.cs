using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("kittens")]
    public class KittenController : ControllerBase
    {
        private readonly ILogger<KittenController> _logger;
        private IKittenRepository _repository;
        private IMapper _mapper;

        public KittenController(
            ILogger<KittenController> logger,
            ApplicationDataContext context, 
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = new KittensRepository(context);
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<KittenResponse> GetList()
        {
            try
            {
                _logger?.LogDebug("GetList");
                var list = new List<KittenResponse>();
                foreach (var kitten in _repository.Get())
                {
                    list.Add(_mapper.Map<KittenResponse>(kitten));
                }
                return list;
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
                Console.WriteLine(e);
                return new List<KittenResponse>();
            }
        }
        
        [HttpPost]
        public IActionResult Add(KittenRequest newKitten)
        {
            try
            { 
                _logger?.LogDebug("Add", newKitten);
                return new CreatedResult("OK", _repository.Add(_mapper.Map<Kittens>(newKitten)));
            }
            catch (Exception e)
            {
                _logger?.LogError("Add", e);
                Console.WriteLine(e);
                return Problem(); 
            }
            
        }
    }
}
