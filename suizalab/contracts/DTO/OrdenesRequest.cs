using contracts.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace contracts.DTO
{
	public class OrdenesRequest
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Cliente no puede estar vacío")]
		public string Cliente { get; set; }
		[BindNever]
		public decimal Total { get; set; } = 0;
		[Detalle]
		public List<OrdenDetallesRequest> Detalles { get; set; }
	}
}
