using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ChatRoom.DAL.Models.Auth;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            string username = _userManager.GetUserName(User);

            return View("Index", username);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
