using contracts.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace contracts.DTO
{
	public class OrdenDetallesRequest
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Producto no puede estar vacío")]
		public string Producto { get; set; }
		[Cantidad]
		public int Cantidad { get; set; }
		[Precio]
		public decimal PrecioUnitario { get; set; }
		[BindNever]
		public decimal Subtotal { get; set; } = 0;
	}
}
