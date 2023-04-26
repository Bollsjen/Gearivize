using Gerivize.AttributeHelpers;
using Microsoft.AspNetCore.Mvc;

namespace Gerivize.Attributes
{
    public class AuthenticatedUserAttribute : ModelBinderAttribute
    {
        public AuthenticatedUserAttribute()
        {
            BinderType = typeof(AuthenticatedUserModelBinder);
        }
    }
}
