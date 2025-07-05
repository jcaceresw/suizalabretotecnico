using contracts;
using contracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IOrdenesServicio _ordenesServicio;

		public HomeController(IOrdenesServicio ordenesServicio)
		{
			_ordenesServicio = ordenesServicio;
		}

		[Route("/")]
		public IActionResult Index(string? searchString, string? searchInit, string? searchEnd/*, string sortBy = nameof(OrdenesRequest.Cliente), SortOrderOptions sortOrder = SortOrderOptions.ASC*/)
		{
			ViewBag.CurrentSearchString = searchString;
			ViewBag.CurrentSearchInit = searchInit;
			ViewBag.CurrentSearchEnd = searchEnd;
			ViewBag.Error = "";

			DateTime fechaInicio = Convert.ToDateTime(searchInit);
			DateTime fechaFin = Convert.ToDateTime(searchEnd);

			// Search
			List<OrdenesResponse> persons = null;

			
			if (!string.IsNullOrEmpty(searchEnd) && fechaInicio > fechaFin)
			{
				ViewBag.Error = "La fecha de Inicio no puede ser mayor a la fecha de Fin";

				persons = _ordenesServicio.Listar(searchString, string.Empty, string.Empty);
			}
			else
			{
				persons = _ordenesServicio.Listar(searchString, searchInit, searchEnd);
			}

			return View(persons);
		}

		[Route("/details/{id:int}")]
		public IActionResult Details(int id)
		{
			OrdenesResponse model = _ordenesServicio.Obtener(id);

			return View(model);
		}

		[HttpGet]
		[Route("/create")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Route("/create")]
		public IActionResult Create([FromForm] OrdenesRequest orden)
		{
			return View();
		}

		[Route("/edit/{id:int}")]
		public IActionResult Edit(int id)
		{
			return View();
		}
	}
}
