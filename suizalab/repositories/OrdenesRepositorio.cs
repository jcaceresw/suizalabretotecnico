using contracts.DTO;
using Microsoft.Data.SqlClient;
using repositories.ADO;
using System.Data;

namespace repositories
{
	public class OrdenesRepositorio : MetodosADO
	{
		public int Crear(OrdenesRequest request)
		{
			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.Parameters.AddWithValue("@CLIE", request.Cliente);
				cmd.Parameters.AddWithValue("@TOTA", request.Total);

				int orderId = ExecNonQueryTransactionalWithOutput("SP_CREAR_ORDEN", cmd, "ORID");

				if (orderId > 0)
				{
					int response = 0;

					foreach (OrdenDetallesRequest detalle in request.Detalles)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@ORID", orderId);
						cmd.Parameters.AddWithValue("@PROD", detalle.Producto);
						cmd.Parameters.AddWithValue("@CANT", detalle.Cantidad);
						cmd.Parameters.AddWithValue("@PUNI", detalle.PrecioUnitario);
						cmd.Parameters.AddWithValue("@SUBT", detalle.Subtotal);

						response = ExecuteNonQuery("SP_CREAR_ORDEN_DETALLE", cmd);
					}

					return response;
				}
			}

			return 0;
		}

		public int Actualizar(OrdenesRequest request)
		{
			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.Parameters.AddWithValue("@ORID", request.Id);
				cmd.Parameters.AddWithValue("@CLIE", request.Cliente);
				cmd.Parameters.AddWithValue("@TOTA", request.Total);

				int response = ExecuteNonQuery("SP_ACTUALIZAR_ORDEN", cmd);

				foreach (OrdenDetallesRequest detalle in request.Detalles)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@DEID", detalle.Id);
					cmd.Parameters.AddWithValue("@PROD", detalle.Producto);
					cmd.Parameters.AddWithValue("@CANT", detalle.Cantidad);
					cmd.Parameters.AddWithValue("@PUNI", detalle.PrecioUnitario);
					cmd.Parameters.AddWithValue("@SUBT", detalle.Subtotal);

					response = ExecuteNonQuery("SP_ACTUALIZAR_ORDEN_DETALLE", cmd);
				}

				return response;
			}

			return 0;
		}

		public DataSet Obtener(int id)
		{
			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.Parameters.AddWithValue("@ID", id);

				cmd.CommandText = "SP_OBTENER_ORDEN_POR_ID";
				cmd.CommandType = CommandType.StoredProcedure;

				DataSet ds = ExecuteNonQueryDataSet(cmd);

				if (ds.Tables.Count > 0)
				{
					return ds;
				}
			}

			return null;
		}

		public int Eliminar(int id)
		{
			int response;

			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

				response = ExecuteNonQueryTransactional("SP_ELIMINAR_ORDEN_POR_ID", cmd);
			}

			return response;
		}

		public DataSet Listar(string? nombreCliente, string? fechaInicio, string? fechaFin)
		{
			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.Parameters.AddWithValue("@CLIE", nombreCliente);
				cmd.Parameters.AddWithValue("@FINI", fechaInicio);
				cmd.Parameters.AddWithValue("@FFIN", fechaFin);

				cmd.CommandText = "SP_LISTAR_ORDENES";
				cmd.CommandType = CommandType.StoredProcedure;

				DataSet ds = ExecuteNonQueryDataSet(cmd);

				if (ds.Tables.Count > 0)
				{
					return ds;
				}
			}

			return null;
		}
	}
}
