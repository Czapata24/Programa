using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HankoSpa.Controllers.Attributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string permission, string module) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = [permission, module];
        }
    }   
    public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly string _module;
        private readonly IUsersService _usersService;
        public CustomAuthorizeFilter(string permission, string module, IUsersService usersService)
        {
            _permission = permission;
            _module = module;
            _usersService = usersService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool userIsAuthenticated = _usersService.CurrentUserIsAuthenticated();  
            bool isAuthorized = await _usersService.CurrentUserIsAuthorizedAsync(_permission, _module);

            if (!userIsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            else if (!isAuthorized)
            {
                context.Result = new RedirectToActionResult("Error", "Account", new { statuscode = 403 });
            }
        }
    }
}
