using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Students.API.Filters;

namespace Students.API.Attributes
{
    public class AuthorizationAttribute: TypeFilterAttribute
    {
        //private static readonly AuthorizationFilter AF;
        public AuthorizationAttribute() : base(typeof(AuthorizationFilter))
        {

        }
    }
}
