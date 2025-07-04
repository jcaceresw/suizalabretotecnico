using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
	public class HomeController : Controller
	{
		[Route("/")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("/details/{id:int}")]
		public IActionResult Details()
		{
			return View();
		}

	}
}
