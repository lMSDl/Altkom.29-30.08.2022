using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ICrudService<User> _service;

        public UsersController(ICrudService<User> usersService)
        {
            _service = usersService;
        }




        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _service.ReadAsync();

            return Ok(values.ToList());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _service.ReadAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            var id = await _service.CreateAsync(value);

            return CreatedAtAction(nameof(Get), new { id = id }, id);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User value)
        {
            var user = await _service.ReadAsync(id);
            if (user == null)
                return NotFound();

            await _service.UpadteAsync(id, value);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.ReadAsync(id);
            if (user == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
