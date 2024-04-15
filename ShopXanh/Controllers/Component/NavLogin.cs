using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Controllers.Component
{
    public class NavLogin : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            bool isSessionExist = HttpContext.Session.Keys.Contains("UserName");
            if (isSessionExist)
            {
                var userName = HttpContext.Session.GetString("UserName");
                return View("Default",userName);
            }
            return View("Default","");
        }
    }
}
