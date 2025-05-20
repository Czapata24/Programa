using HankoSpa.DTOs;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HankoSpa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;

        public AccountController (IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _usersService.LoginAsync(dto);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrecta");
            }
            return View(dto);

        }

        [HttpGet]
        [Route("/Errors/{statuscode:int}")]

        public IActionResult Error (int statuscode)
        {
            string errorMessage = "Ha ocurrido un error";
            switch (statuscode)
            {
                case StatusCodes.Status401Unauthorized:
                    errorMessage = "Debes iniciar sesion";
                    break;
                case StatusCodes.Status403Forbidden:
                    errorMessage = "No tienes permiso para estar aqui";
                    break;
                case StatusCodes.Status404NotFound:
                    errorMessage = "La pagina que estas intentando acceder no existe";
                    break;
            }
            ViewBag.ErrorMessage = errorMessage;
            return View(statuscode);
        }
        [HttpGet]
        public IActionResult NotAuthorized()
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Logout()
        {
            await _usersService.LogoutAsync();
            return RedirectToAction(nameof(Login));

        }

    }
}
