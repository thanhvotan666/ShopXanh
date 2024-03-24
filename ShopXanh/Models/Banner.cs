using Microsoft.AspNetCore.Mvc;

namespace ShopXanh.Models
{
	public class Banner : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
