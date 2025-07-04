namespace entities
{
	public class OrdenDetalles
	{
		public int Id { get; set; }
		public int OrdenId { get; set; }
		public string Producto { get; set; }
		public int Cantidad { get; set; }
		public decimal PrecioUnitario { get; set; }
		public decimal Subtotal { get; set; }
	}
}
