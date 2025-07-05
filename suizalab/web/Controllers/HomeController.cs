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
			ViewBag.SearchFields = new Dictionary<string, string>()
			{
				{ nameof(OrdenesResponse.Cliente), "Cliente" },
				{ nameof(OrdenesResponse.FechaCreacion), "Fecha de Creación" }
			};
			
			// Search
			List<OrdenesResponse> persons = _ordenesServicio.Listar(searchString, searchInit, searchEnd);			
			ViewBag.CurrentSearchString = searchString;
			ViewBag.CurrentSearchInit = searchInit;
			ViewBag.CurrentSearchEnd = searchEnd;
			/*
			// Sort
			List<OrdenesResponse> sortedPersons = _ordenesServicio.GetSortedPersons(persons, sortBy, sortOrder);
			ViewBag.CurrentSortBy = sortBy;
			ViewBag.CurrentSortOrder = sortOrder;
			
			return View(sortedPersons);*/
			return View(persons);
		}

		[Route("/details/{id:int}")]
		public IActionResult Details(int id)
		{
			OrdenesResponse model = _ordenesServicio.Obtener(id);

			return View(model);
		}

		[Route("/create")]
		public IActionResult Create()
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
