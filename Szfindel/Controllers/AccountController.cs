using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Szfindel.Interface;
using Szfindel.Models;
using Szfindel.Repo;
//using System.Collections.Generic; 

namespace Szfindel.Controllers
{
    /// <summary>
    /// AccountController.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccount _accountRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHobby _hobbyRepo;
        private readonly IUser _userRepo;
        private readonly IMessage _messageRepo;
        private readonly IMatch _matchRepo;


        /// <summary>
        ///  Łączenie interfejsy z kontorlerem 
        /// </summary>
        /// <param name="userRepository"></param>
        public AccountController(IAccount accountRepo, IWebHostEnvironment webHostEnvironment, IHobby hobbyRepo,IUser userRepo, IMessage messageRepo,IMatch matchRepo)
        {
            _accountRepo = accountRepo;
            _webHostEnvironment = webHostEnvironment;
            _hobbyRepo = hobbyRepo;
            _userRepo = userRepo;
            _messageRepo = messageRepo;
            _matchRepo = matchRepo;
        }
        
        /// <summary>
        /// Funkcja zwraca widok formularza do wypełnienia
        /// </summary>
        /// <returns></returns>


        [HttpGet("Info")]
        public IActionResult Info()
        {

            return View(new AccountUser());
        }
        
        /// <summary>
        /// Funkcja zapisy danych User <-> Account + sprawdzenie czy są one poprawne
        /// </summary>
        /// <returns></returns>
        [HttpPost("info")]
        public IActionResult Info([FromForm] AccountUser accountUser)

        {

            if (ModelState.IsValid)
            {

                var IDrequest = Request.Cookies["UserId"];
                if (IDrequest != null)
                {
                    int.TryParse(IDrequest, out int userIdint);
                    accountUser.UserId = userIdint;
                }
                //jezeli w bazie jest account o podanym UserID to nie tworzymy

                if (_accountRepo.AccountHasUser(accountUser))
                {
                    ModelState.AddModelError(string.Empty, "Istnieja juz dane do uzytkownika");

                }
                else
                {

                    _accountRepo.AddForgeinKeyToUser(_accountRepo.AddAccountUserInfo(accountUser));
                }


                Response.Cookies.Append("AccountUserId", accountUser.AccountUserId.ToString());
                return RedirectToAction("Index", "Home");
            }


            return View(accountUser);
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {

            return View();
        }

        [HttpGet("Ustawienia")]
        public IActionResult Ustawienia()
        {

            return View();
        }

        //Widok formularza do uzupełnienia danych o sobie np. zmiana imienia/wieku dodanie zdjęcia


        /// <summary>
        /// Widok formularza do uzupełnienia danych o sobie np. zmiana imienia/wieku dodanie zdjęcia
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("UpdateAccount")]
        public IActionResult UpdateAccount()
        {
            var IDrequest = Request.Cookies["UserId"];

            if (IDrequest != null)
            {
                int.TryParse(IDrequest, out int userIdint);
                var acc = _accountRepo.GetAccountUserByUserId(userIdint);
                if (acc != null)
                {
                    IEnumerable<Hobby> hobbies = _accountRepo.GetHobbies();
                    ViewBag.Hobbies = hobbies;
                  
                    return View(acc); // Przekazujemy obiekt acc do widoku
                }
            }
            return View(); // Jeśli nie udało się pobrać danych użytkownika, zwracamy pusty widok
        }

        /// <summary>
        /// Funkcja sprawdzająca poprwaeność wypełnienia formularza przez użytkownika o swoich danych osobowych.
        /// Jeżeli wszystkie pola się zgadzją informacje o użytkowniku zostają zaaktualizowane
        /// </summary>
        /// <param name="updatedAccount"></param>
        /// <param name="file"></param>
        /// <returns></returns>

        [HttpPost("UpdateAccount")]
        public IActionResult UpdateAccount([FromForm] AccountUser updatedAccount, IFormFile? file)
        {

            var actualImg = updatedAccount.Image;
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(wwwRootPath, @"images\people");//dodawanie scieżki do zdjecia

                    //usuwanie działa
                    if (!string.IsNullOrEmpty(updatedAccount.Image))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, updatedAccount.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    /// @cond
                    updatedAccount.Image = @"\images\people\" + fileName;
                    /// @endcond
                    _accountRepo.UpdateImage(updatedAccount);
                }



                _accountRepo.UpdateAccount(updatedAccount);

                return RedirectToAction("Index", "Home");
            }

            return View(updatedAccount);
        }
        //Wyswietlanie posczególnej osoby z danymi o niej
        /// <summary>
        /// Funkcja wyświetla informacje o użytkowniku,którego wybraliśmy z widoku Account/AllAccount
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("AccountProfil/{id}")]
        public IActionResult AccountProfil(int id)
        {
            var userProfile = _accountRepo.GetAccountUserByAccUserId(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            
            // Pobieramy ID zalogowanego użytkownika
            var senderId = Request.Cookies["UserId"];

            int.TryParse(senderId, out int senderIdint);
            var acc = _accountRepo.GetAccountUserByUserId(senderIdint);
            ViewBag.SenderId = senderIdint;
            var userHobbies = _hobbyRepo.GetUserHobbyById(id);
            ViewBag.USRHOBBY = userHobbies;
            //  ViewData["AccountUserId"] = acc.AccountUserId;
            var match = _matchRepo.Ismatch(senderIdint, id);
            ViewBag.Match = match;

            return View(userProfile);
        }

        //lista uzytkowników
        /// <summary>
        /// Funkcja wyświetlająca wszystkich użytkowników z bazy danych, przekazuje do widoku  zmienną id po to aby nie wyświetlać
        /// profilu samego siebie
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllAccount")]
        public IActionResult AllAccount()
        {
            var IDrequest = Request.Cookies["UserId"];
            if (IDrequest != null)
            {
                int.TryParse(IDrequest, out int userIdint);
                var acc = _accountRepo.GetAccountUserByUserId(userIdint);
                int Id = acc.AccountUserId;
                ViewBag.Uid = Id;
                var matches = _matchRepo.OurMatch(Id);
                ViewBag.Matches = matches;
                // ViewData["AccountUserId"] = acc.AccountUserId;
            }
            //wszystkich użytkowników z bazy danych
            var accountUsers = _accountRepo.GetAllAccountUsers();
            
            //lista użytkowników do widoku
            return View(accountUsers);
        }
        

        /// <summary>
        /// Funkcja filtrująca wszystkich użytkowników po ich danych wiekowych w widoku Account/AllAccount
        /// </summary>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        [HttpGet("FilteredUsers")]
        public IActionResult FilteredUsers(int? minAge, int? maxAge)
        {
            var IDrequest = Request.Cookies["UserId"];
            if (IDrequest != null)
            {
                int.TryParse(IDrequest, out int userIdint);
                var acc = _accountRepo.GetAccountUserByUserId(userIdint);
                int Id = acc.AccountUserId;
                ViewBag.Uid = Id;

            }
            IEnumerable<AccountUser> filteredUsers = _accountRepo.GetUsersByAgeRange(minAge, maxAge);

            // Przekazanie wyników filtrowania do widoku
            return View("AllAccount", filteredUsers);
        }

        /// <summary>
        /// Usuwanie konta użytkownika wraz z całymi jego aktywnościami takimi jak hobby, message itp.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteAccount")]
        public IActionResult DeleteAccount()
        {
            // Pobierz identyfikator użytkownika do usunięcia
            var userId = Request.Cookies["AccountUserId"];
            if (userId == null)
            {
                return BadRequest("Użytkownik nie jest zalogowany.");
            }

            int.TryParse(userId, out int userIdint);
            AccountUser accuser = _accountRepo.GetAccountUserByAccUserId(userIdint);

            try
            {
                // Usuń hobby uzytkowika
                _hobbyRepo.DeleteUsersHobbiesByID(userIdint);
                // Usuń wiadomsci uzytkowika
                _messageRepo.DeleteMessagesUsers(userIdint);
                // Usuń wszystkie matche
                _matchRepo.DeleteUsersMatchesByID(userIdint);
                // Usuń samego użytkownika
                _userRepo.DeleteUserById(userIdint);
                // Usuń wszystkie dane użytkownika
                _accountRepo.DeleteAccountByID(userIdint);





                // Wyczyść sesję
                HttpContext.Session.Clear();

                // Usuń ciasteczka
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Response.Cookies.Delete("AccountUserId");
                Response.Cookies.Delete("UserId");


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Obsługa błędu
                return StatusCode(500, $"Wystąpił błąd podczas usuwania konta: {ex.Message}");
            }
        }

    }
}
