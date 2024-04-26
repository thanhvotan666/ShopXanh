using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Contains("UserEmail"))
            {
                return RedirectToAction(nameof(Login));
            }
            if (!HttpContext.Session.GetString("UserEmail").Equals("admin@gmail.com"))
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if(email.Equals("admin@gmail.com") && password.Equals("admin123"))
            {
                HttpContext.Session.SetString("UserEmail","admin@gmail.com");
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction(nameof(Login));
        }
    }
}
