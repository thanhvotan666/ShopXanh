using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Controllers.Component
{
    public class Head : ViewComponent
    {
        public IViewComponentResult Invoke(string param)
        {
            return View("Default", param);
        }
    }
}
