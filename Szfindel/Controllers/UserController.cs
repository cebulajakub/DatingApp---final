using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Web;
using System.Linq;
using Szfindel.Repo;
using Szfindel.Models;
using Szfindel.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace Szfindel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUser _userRepository;
        /// <summary>
        /// Łączenieinterfejsy z kontorlerem 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult HomeUser()
        {
            return View();
        }
        /// <summary>
        /// Przekazywanie nowego formulaza do logowania
        /// </summary>
        /// <returns></returns>
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(new User());
        }

        /// <summary>
        /// Przekazywanie nowego formulaza do rejsetracji
        /// </summary>
        /// <returns></returns>

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View(new User());
        }


        /// <summary>
        /// Funkcja pobiera  dane uzupełnione z formularza tworzy osobny token logowania dla użytkownika, którym jest jego Id,
        /// Jeżeli użytkownik loguje się na strone pierwszy raz obowiązkowo musi wypełnić dane osobiste, w przciwnym razie
        /// będzie mieć ogranicznoy dostęp.Poprawne zalogowanie przechodzi do ekranu startowego
        /// </summary>
        /// <returns></returns>


        [HttpPost("login")]
        public IActionResult Login([FromForm] User user)
        {
          
                var existingUser = _userRepository.GetUserByCredentials(user.Username, user.Password);

                if (existingUser != null && ModelState.IsValid)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    Response.Cookies.Append("UserId", existingUser.UserId.ToString()); //przechowuje id w cookie

                    

                   // ViewData["AccountUserId"] = existingUser.AccountUserId;


                // Sprawdzanie czy zalogowano pierwszy raz
                if (existingUser.AccountUserId == null)
                    {
                   
                    return RedirectToAction("Info", "Account");
                    }
                    else
                    {
                    Response.Cookies.Append("AccountUserId", existingUser.AccountUserId.ToString());
                    return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(user.Password), "Nieprawidłowe dane logowania. Użytkownik o podanej nazwie nie istnieje.");
                return View(user);
            }
         
        }




        /// <summary>
        /// Funkcja pobierająca dane z formularza o danych rejestracyjnych, jeżeli dane 
        /// przezchodzą walidacje jest tworzony nowy użytkownik
        /// </summary>
        /// <returns></returns>

        [HttpPost("register")]
        public IActionResult Register([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userRepository.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError(nameof(user.Username), "Użytkownik o tej nazwie już istnieje.");
                    return View(user);
                }

                _userRepository.CreateUser(user);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }


        /// <summary>
        /// Funkcja umożliwjająca usunięcie ciasteczka(tokeny) ze strony wraz z wylogowaiem
        /// </summary>
        /// <returns></returns>


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("AccountUserId");
            return RedirectToAction("Index", "Home");
        }
    }

}
