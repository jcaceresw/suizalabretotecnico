using Microsoft.Data.SqlClient;
using System.Data;

namespace repositories.ADO
{
	public class MetodosADO
	{
		protected static SqlConnection GetConnection()
		{
			SqlConnection cn = new SqlConnection { ConnectionString = "Data Source=DESKTOP-DAVQV78\\SQLEXPRESS;Initial Catalog=SuizaLabDB;Integrated Security=true;TrustServerCertificate=true;" };

			return cn;
		}

		internal static int ExecuteNonQuery(string spName, SqlCommand cmd)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;

			try
			{
				cn.Open();
				int response = cmd.ExecuteNonQuery();
				return response;
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}

		internal static int ExecuteNonQueryTransactional(string spName, SqlCommand cmd)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;

			cn.Open();
			SqlTransaction trans = cn.BeginTransaction();
			cmd.Transaction = trans;

			try
			{
				int response = cmd.ExecuteNonQuery();
				trans.Commit();

				return response;
			}
			catch (Exception)
			{
				trans.Rollback();
				throw;
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}

		internal static int ExecNonQueryWithOutput(string spName, SqlCommand cmd, string output)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramId = new SqlParameter(output, SqlDbType.Int, 8) { Direction = ParameterDirection.Output };
			cmd.Parameters.Add(paramId);
			try
			{
				cn.Open();
				cmd.ExecuteNonQuery();

				string id = cmd.Parameters[output].Value.ToString();
				return int.Parse(id);
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}

		internal static int ExecNonQueryTransactionalWithOutput(string spName, SqlCommand cmd, string output)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramId = new SqlParameter(output, SqlDbType.Int, 8) { Direction = ParameterDirection.Output };
			cmd.Parameters.Add(paramId);

			cn.Open();
			SqlTransaction trans = cn.BeginTransaction();
			cmd.Transaction = trans;

			try
			{
				cmd.ExecuteNonQuery();

				string id = cmd.Parameters[output].Value.ToString();
				trans.Commit();

				return int.Parse(id);
			}
			catch (Exception)
			{
				trans.Rollback();
				throw;
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}

		internal static DataSet ExecuteNonQueryDataSet(SqlCommand cmd)
		{
			DataSet response = new DataSet("tblDataSet");

			using (SqlConnection cn = GetConnection())
			{
				try
				{
					cmd.Connection = cn;
					SqlDataAdapter da = new SqlDataAdapter { SelectCommand = cmd };
					cn.Open();

					da.Fill(response);
				}
				finally
				{
					if (cn.State == ConnectionState.Open)
					{
						cn.Close();
						cn.Dispose();
					}

					cmd.Dispose();
				}
			}

			return response;
		}

		internal static SqlDataReader ExecuteDataReader(string sSpName, SqlCommand cmd)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandTimeout = 0;
			cmd.CommandText = sSpName;
			cmd.CommandType = CommandType.StoredProcedure;

			try
			{
				cn.Open();
				return cmd.ExecuteReader();
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}

		internal static object ExecuteScalarTransactional(string spName, SqlCommand cmd)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;

			cn.Open();
			SqlTransaction trans = cn.BeginTransaction();
			cmd.Transaction = trans;

			try
			{
				object response = cmd.ExecuteScalar();
				trans.Commit();

				return response;
			}
			catch (Exception)
			{
				trans.Rollback();
				throw;
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}

		internal static object ExecuteScalar(string spName, SqlCommand cmd)
		{
			SqlConnection cn = GetConnection();

			if (cmd == null)
			{
				cmd = new SqlCommand();
			}

			cmd.Connection = cn;
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;

			try
			{
				cn.Open();
				object response = cmd.ExecuteScalar();
				return response;
			}
			finally
			{
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
					cn.Dispose();
				}

				cmd.Dispose();
			}
		}
	}
}
