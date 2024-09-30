using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Szfindel.Interface;
using Szfindel.Models;

namespace Szfindel.Controllers
{
    [Authorize]
    public class ApiWeatherController : Controller
    {
        private readonly IApi _api;
        private readonly IAccount _accountUser;

        /// <summary>
        /// Łączenie interfejsów z kontrolerem
        /// </summary>
        /// <param name="api"></param>
        /// <param name="accountUser"></param>
        public ApiWeatherController(IApi api, IAccount accountUser)
        {
                _api= api;
            _accountUser = accountUser;
        }

        /// <summary>
        /// Pobieranie zalogowanego użytkownika poprzez id,
        /// Następnie jest wyłuskiwane jego miasto z bazy danych i przekazywane do metody co pobiera hobby z zewnętrznego api
        /// W przypadku błędu np. z siecią jest wyłapywany wyjątek i wyświetlany komunikat
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var user = Request.Cookies["UserId"];
            int.TryParse(user, out int userIdint);
            var accountUser = _accountUser.GetAccountUserByUserId(userIdint);

            if (accountUser != null)
            {
                string city = accountUser.City;

                try
                {
                    var response = _api.Get(city);

                    ViewBag.CityName = city;
                    ViewData["AccountUserId"] = accountUser.AccountUserId;
                    return View("Index", response);
                }
                catch (Exception ex)
                {
                    
                
                    ViewBag.ExceptionMessage = ex.Message;
                    return View("Error","ApiWeather");
                }
            }
            else
            {
                return RedirectToAction("NoCityError");
            }

        }


        }


 }

 



