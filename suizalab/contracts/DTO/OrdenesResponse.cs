namespace contracts.DTO
{
	public class OrdenesResponse
	{
		public int Id { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string Cliente { get; set; }
		public decimal Total { get; set; }
		public List<OrdenDetallesResponse> Detalles { get; set; } = new List<OrdenDetallesResponse>();
	}
}
