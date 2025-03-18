using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
	public class HomeScreenController : Controller
	{
		public IActionResult Index()
		{
			return View("HomeScreen");
		}
	}
}
