using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Controllers.Component
{
    public class Nav : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
