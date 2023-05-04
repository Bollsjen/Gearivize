using Gerivize.Managers;
using Gerivize.Models;
using Gerivize.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly UserManager _userManager;

        public UserController()
        {
            _userRepository = new UserRepository();
            _userManager = new UserManager();
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> users = _userRepository.getAll();
            if(users.Count == 0) { return NotFound(); }
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("id/{id}")]
        public ActionResult<User> Get(Guid id)
        {
            User user = _userRepository.getById(id);
            if(user == null) { return NotFound(); }
            return Ok(user);
        }

        [HttpGet("email-password/{email}/{password}")]
        public ActionResult<User> GetByEmailAndPassword(string email, string password)
        {
            User user = _userRepository.getByEmailAndPassword(email, password);
            if(user == null) { return NotFound();}
            return Ok(user);
        }

        // POST api/<UserController>
        [Authorize]
        [HttpPost]
        public ActionResult<User> Post([FromBody] User value)
        {
            User user = _userRepository.createUser(value);
            if(user == null) { return NotFound(); }
            return Ok(user);
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut]
        public ActionResult<User> Put([FromBody] User value)
        {
            User user = _userRepository.updateUser(value);
            if(user == null) { return NotFound();}
            return Ok(user);
        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(Guid id)
        {
            User user = _userRepository.deleteUser(id);
            if(user == null) { return NotFound(); };
            return Ok(user);
        }
    }
}
