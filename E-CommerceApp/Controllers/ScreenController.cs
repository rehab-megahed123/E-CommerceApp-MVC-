using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
	public class ScreenController : Controller
	{
		public IActionResult Screen()
		{
			return View("MainScreen");
		}
	}
}
