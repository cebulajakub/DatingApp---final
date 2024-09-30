using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Szfindel.Interface;
using Szfindel.Models;
using Szfindel.Repo;

namespace Szfindel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : Controller
    {
        private readonly IMatch _matchRepo;
        private readonly IAccount _accountRepo;
        /// <summary>
        /// Łączenie interfejsu z kontorlerem 
        /// </summary>
        /// <param name="matchRepo"></param>

        public MatchController(IMatch matchRepo, IAccount accountRepo)
        {
            _matchRepo = matchRepo;
            _accountRepo = accountRepo;
        }



        [HttpPost("like")]
        public IActionResult Like([FromForm] int matchedUserId)
        {
            var userId = Request.Cookies["AccountUserId"];
            if (userId == null)
            {
                return BadRequest("Użytkownik nie jest zalogowany.");
            }

            int.TryParse(userId, out int userIdint);

            // Sprawdź, czy istnieje już rekord matcha
            var existingMatch = _matchRepo.CheckMutualLike(userIdint, matchedUserId);
           

            if (existingMatch != null)
            {
                // Jeśli istnieje rekord matcha, zaktualizuj flagę isMatch na true
                existingMatch.IsMatch = true;
                _matchRepo.UpdateMatch(userIdint, matchedUserId);
            }
            else
            {
                // Jeśli nie istnieje rekord matcha, dodaj nowy rekord z flagą isMatch ustawioną na false
                var newMatch = new Match
                {
                    AccountUserId = userIdint,
                    MatchUserId = matchedUserId,
                    IsMatch = false
                };
                _matchRepo.AddLike(newMatch);
            }

            return RedirectToAction("AllAccount", "Account");
        }

        [HttpPost("delete")]
        public IActionResult DeleteMatch([FromForm] int matchedUserId)
        {

            var userId = Request.Cookies["AccountUserId"];
            if (userId == null)
            {
                return BadRequest("Użytkownik nie jest zalogowany.");
            }
            int.TryParse(userId, out int userIdint);
            var match = _matchRepo.IsmatchReturnMatch( matchedUserId, userIdint);
            try
            {
                _matchRepo.Delete(match);
                return RedirectToAction("AllAccount", "Account");
            }
            catch (Exception ex)
            {
                return BadRequest("Błąd usuwania matcha: " + ex.Message);
            }

            
        }
    }
}