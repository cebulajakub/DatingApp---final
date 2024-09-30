using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Szfindel.Interface;
using Szfindel.Models;
using Szfindel.Repo;

namespace Szfindel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : Controller
    {
        private readonly IHobby _hobbyRepo;
        /// <summary>
        /// Łączenie interfejsu z kontorlerem 
        /// </summary>
        /// <param name="hobbyRepo"></param>

        public HobbyController(IHobby hobbyRepo)
        {
            _hobbyRepo = hobbyRepo;

        }
        [HttpGet("Test")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Funkcja pobiera i przekazuje do widoku informacje o wszystkich hobby znajdujacych się w bazie danych.
        /// Za pomocą Viewbagów przekazywane są informacje o aktualnym użytkowniku wraz z jego Hobby
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AllHobbies()
        {

            var Hobbies = _hobbyRepo.GetAllHobby();
            var userId = Request.Cookies["AccountUserId"];
            int.TryParse(userId, out int userIdint);
            ViewBag.accid = userIdint;


            var userHobbies = _hobbyRepo.GetUserHobbyById(userIdint);
            ViewBag.USRHOB = userHobbies;


            //lista hobby do widoku
            return View(Hobbies);

        }

      /// <summary>
      /// Funkcja za pomocą której jesteśmy w stanie dodać do bazy danych poszczególne hobby do aktualnie zalogowanego użykownika
      /// z widoku Hobby/AllHobbies
      /// </summary>
      /// <param name="hobbyId"></param>
      /// <returns></returns>
        [HttpPost("AddHobby")]
        public IActionResult Addhobby([FromForm] int hobbyId)
        {
            var AccId = Request.Cookies["AccountUserId"];
            int.TryParse(AccId, out int accid);

            _hobbyRepo.AddUserHobby(accid, hobbyId);
            

            return RedirectToAction("AllHobbies", "Hobby");
        }
        /// <summary>
        ///  Funkcja za pomocą której jesteśmy w stanie usunąć z bazy danych dodane Hobby użytkoniwka
        ///  za pomocą widoku Hobby/AllHobbies
        /// </summary>
        /// <param name="hobbyId"></param>
        /// <returns></returns>
        [HttpPost("RemoveHobby")]
        public IActionResult Removehobby([FromForm] int hobbyId)
        {
            var AccId = Request.Cookies["AccountUserId"];
            int.TryParse(AccId, out int accid);

            _hobbyRepo.RemoveUserHobby(accid, hobbyId);


            return RedirectToAction("AllHobbies", "Hobby");
        }




    }
}
