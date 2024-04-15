using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Controllers.Component
{
    public class Footer : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
