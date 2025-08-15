using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestwithASPNETUdemy.Model;
using RestwithASPNETUdemy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestwithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            var _person = _personService.FindById(Id);
            if (_person == null) return NotFound();
            return Ok(_person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person _person)
        {
            if (_person == null) return BadRequest();
            return Ok(_personService.Create(_person));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Person _person)
        {
            if (_person == null) return BadRequest();
            return Ok(_personService.Update(_person));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(long Id)
        {
            _personService.Delete(Id);
            return NoContent();
        }
    }
}
