using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Database.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("kittens")]
    public class KittenController : ControllerBase
    {
        private readonly ILogger<KittenController> _logger;
        private IKittenRepository _repository;

        public KittenController(
            ILogger<KittenController> logger,
            IKittenRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Kittens> GetList()
        {
            try
            {
                _logger?.LogDebug("GetList");
                return _repository.Get();
            }
            catch (Exception e)
            {
                _logger?.LogError("GetList", e);
                Console.WriteLine(e);
                return new List<Kittens>();
            }
        }
        
        [HttpPost]
        public IActionResult Add(Kittens newKitten)
        {
            try
            { 
                _logger?.LogDebug("Add", newKitten);
                return new CreatedResult("OK", _repository.Add(newKitten));
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
