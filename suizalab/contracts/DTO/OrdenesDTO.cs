using System.ComponentModel.DataAnnotations;

namespace contracts.DTO
{
	public class OrdenesDTO
	{
		public int Id { get; set; }
		public DateTime FechaCreacion { get; set; }
		[Required(ErrorMessage = "Cliente no puede estar vacío")]
		public string Cliente { get; set; }
		public decimal Total { get; set; } = 0;
	}
}
