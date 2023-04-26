using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using Gerivize.Extensions;
using Gerivize.Models;
using System.Security.Claims;
using Gerivize.Repositories;

namespace Gerivize.AttributeHelpers
{
    public class AuthenticatedUserModelBinder : IModelBinder
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserRepository _userRepository;
        public AuthenticatedUserModelBinder(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _userRepository = new UserRepository();
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _userRepository.getById(userId);

            if (user != null)
            {
                bindingContext.Result = ModelBindingResult.Success(user);
            }
            else
            {
                bindingContext.ModelState.AddModelError("", "User not authenticated");
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
