using contracts.Validators;
using System.ComponentModel.DataAnnotations;

namespace contracts.DTO
{
	public class OrdenDetallesDTO
	{
		public int Id { get; set; }
		public int OrdenId { get; set; }
		[Required(ErrorMessage = "Producto no puede estar vacío")]
		public string Producto { get; set; }
		[Cantidad]
		public int Cantidad { get; set; }
		[Precio]
		public decimal PrecioUnitario { get; set; }
		public decimal Subtotal { get; set; }
	}
}
