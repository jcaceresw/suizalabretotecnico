using contracts.DTO;

namespace contracts
{
	public  interface IOrdenesServicio
	{
		/// <summary>
		/// Crea una nueva orden con sus detalles
		/// </summary>
		/// <param name="orden">Orden y sus detalles a crear</param>
		void Crear(OrdenesDTO orden);

		/// <summary>
		/// Actualiza la orden y sus detalles
		/// </summary>
		/// <param name="orden">Orden y sus detalles a actualizar</param>
		void Actualizar(OrdenesDTO orden);

		/// <summary>
		/// Elimina una orden y sus detalles
		/// </summary>
		/// <param name="id">ID de la orden a eliminar</param>
		void Eliminar(int id);

		/// <summary>
		/// Obtiene una order y sus detalles
		/// </summary>
		/// <param name="id">ID de la orden a obtener</param>
		void Obtener(int id);

		/// <summary>
		/// Listar las ordenes
		/// </summary>
		/// <param name="nombreCliente">Filtro opcional por nombre del cliente</param>
		/// <param name="fechaInicio">Filtro opcional por fecha de creación (rango inicial)</param>
		/// <param name="fechaFin">Filtro opcional por fecha de creación (rango final)</param>
		void Listar(string? nombreCliente, string? fechaInicio, string? fechaFin);
	}
}
