using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Gerivize.Repositories;
using Microsoft.AspNetCore.Authorization;
using Gerivize.Attributes;
using Gerivize.Models;
using Gerivize.Extensions;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthenticationController(IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = new UserRepository();
            _contextAccessor = httpContextAccessor;
        }

        // GET: api/<AthenticationController>
        [Authorize]
        [HttpGet]
        public ActionResult<User> Get([AuthenticatedUser] User user)
        {
            return Ok(user);
        }

        [HttpPost("{email}/{password}")]
        public async Task<ActionResult> Post(string email, string password)
        {
            User? user = _userRepository.getByEmailAndPassword(email, password);
            if(user == null) return Unauthorized();
            await HttpContext.SignInAsync("gearivise", new ClaimsPrincipal(
                new ClaimsIdentity(new[] 
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    },
                    "gearivise"
                )
            ),
            new AuthenticationProperties()
            {
                IsPersistent = true,
            });
            return Ok();
        }

        // DELETE api/<AthenticationController>/5
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            await HttpContext.SignOutAsync("gearivise",
            new AuthenticationProperties()
            {
                IsPersistent = true
            });
            return Ok();
        }
    }
}
