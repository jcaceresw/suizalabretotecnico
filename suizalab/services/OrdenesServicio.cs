using contracts;
using contracts.DTO;
using repositories;
using services.Helpers;
using System.Data;

namespace services
{
	public class OrdenesServicio : IOrdenesServicio
	{
		private readonly OrdenesRepositorio _repositorio;

		public OrdenesServicio()
		{
			_repositorio = new OrdenesRepositorio();
		}

		public int Crear(OrdenesRequest orden)
		{
			Validador.ValidarModelo(orden);

			// Calculando el total y subtotales
			decimal total = 0;

			foreach (OrdenDetallesRequest detalle in orden.Detalles)
			{
				decimal subtotal = detalle.Cantidad * detalle.PrecioUnitario;

				total += subtotal;
				detalle.Subtotal = subtotal;
			}

			orden.Total = total;

			return _repositorio.Crear(orden);
		}

		public int Actualizar(OrdenesRequest orden)
		{
			Validador.ValidarModelo(orden);

			// Calculando el total y subtotales
			decimal total = 0;

			foreach (OrdenDetallesRequest detalle in orden.Detalles)
			{
				decimal subtotal = detalle.Cantidad * detalle.PrecioUnitario;

				total += subtotal;
				detalle.Subtotal = subtotal;
			}

			orden.Total = total;

			return _repositorio.Actualizar(orden);
		}

		public int Eliminar(int id)
		{
			return _repositorio.Eliminar(id);
		}

		public void Listar(string? nombreCliente, string? fechaInicio, string? fechaFin)
		{
			throw new NotImplementedException();
		}

		public OrdenesResponse Obtener(int id)
		{
			DataSet tablas = _repositorio.Obtener(id);

			if (tablas != null && tablas.Tables.Count == 2)
			{
				OrdenesResponse respuesta = new OrdenesResponse();

				foreach (DataRow row in tablas.Tables[0].Rows)
				{
					respuesta.Id = Convert.ToInt32(row["Id"]);
					respuesta.Cliente = row["Cliente"].ToString();
					respuesta.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
					respuesta.Total = Convert.ToDecimal(row["Total"]);
				}

				foreach (DataRow row in tablas.Tables[1].Rows)
				{
					OrdenDetallesResponse detalle = new OrdenDetallesResponse();

					detalle.Id = Convert.ToInt32(row["Id"]);
					detalle.Producto = row["Producto"].ToString();
					detalle.Cantidad = Convert.ToInt32(row["Cantidad"]);
					detalle.PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]);
					detalle.Subtotal = Convert.ToDecimal(row["Subtotal"]);

					respuesta.Detalles.Add(detalle);
				}

				return respuesta;
			}

			return null;
		}
	}
}
