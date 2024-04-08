using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Controllers.Component
{
    public class Banner : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
