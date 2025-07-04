using contracts;
using contracts.DTO;
using contracts.Enums;
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
		public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(OrdenesRequest.Cliente), SortOrderOptions sortOrder = SortOrderOptions.ASC)
		{
			ViewBag.SearchFields = new Dictionary<string, string>()
			{
				{ nameof(OrdenesResponse.Cliente), "Cliente" },
				{ nameof(OrdenesResponse.FechaCreacion), "Fecha de Creación" }
			};
			/*
			// Search
			List<PersonResponse> persons = _ordenesServicio.GetFilteredPersons(searchBy, searchString);
			ViewBag.CurrentSearchBy = searchBy;
			ViewBag.CurrentSearchString = searchString;

			// Sort
			List<PersonResponse> sortedPersons = _ordenesServicio.GetSortedPersons(persons, sortBy, sortOrder);
			ViewBag.CurrentSortBy = sortBy;
			ViewBag.CurrentSortOrder = sortOrder;
			
			return View(sortedPersons);*/
			return View();
		}

		[Route("/order/{id:int}")]
		public IActionResult Details(int id)
		{
			OrdenesResponse model = _ordenesServicio.Obtener(id);

			return View(model);
		}

	}
}
