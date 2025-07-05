using Azure;
using contracts;
using contracts.DTO;
using services;
using Xunit.Abstractions;

namespace tests
{
	public class OrdenesTest
	{
		private readonly IOrdenesServicio _ordenesServicio;
		private readonly ITestOutputHelper _testOutputHelper;

		public OrdenesTest(ITestOutputHelper testOutputHelper)
		{
			_ordenesServicio = new OrdenesServicio();
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void Crear_DatosErroneos_ClienteVacio()
		{
			OrdenesRequest request = new OrdenesRequest();
			request.Cliente = "";

			ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			{
				_ordenesServicio.Crear(request);
			});

			if (exception != null)
			{
				_testOutputHelper.WriteLine(exception.Message);
			}
		}

		[Fact]
		public void Crear_DatosErroneos_DetallesVacios()
		{
			OrdenesRequest request = new OrdenesRequest();
			request.Cliente = "Falabella";

			ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			{
				_ordenesServicio.Crear(request);
			});

			if (exception != null)
			{
				_testOutputHelper.WriteLine(exception.Message);
			}
		}

		[Fact]
		public void Crear_DatosErroneos_DetallesErroneos()
		{
			OrdenesRequest request = new OrdenesRequest();
			request.Cliente = "Falabella";

			request.Detalles = new List<OrdenDetallesRequest>()
			{
				new OrdenDetallesRequest() { Producto = "Polo", Cantidad = 5, PrecioUnitario = -1.50M },
				new OrdenDetallesRequest() { Producto = "", Cantidad = 1, PrecioUnitario = 3.50M },
				new OrdenDetallesRequest() { Producto = "Corbata", Cantidad = -5, PrecioUnitario = 11.50M },
				new OrdenDetallesRequest() { Producto = "Correa", Cantidad = 3, PrecioUnitario = 10.50M }
			};

			ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			{
				_ordenesServicio.Crear(request);
			});

			if (exception != null)
			{
				_testOutputHelper.WriteLine(exception.Message);
			}
		}

		[Fact]
		public void Crear()
		{
			OrdenesRequest request = new OrdenesRequest();
			request.Cliente = "Falabella";

			request.Detalles = new List<OrdenDetallesRequest>()
			{
				new OrdenDetallesRequest() { Producto = "Polo", Cantidad = 5, PrecioUnitario = 1.50M },
				new OrdenDetallesRequest() { Producto = "Blusa", Cantidad = 1, PrecioUnitario = 3.50M },
				new OrdenDetallesRequest() { Producto = "Corbata", Cantidad = 5, PrecioUnitario = 11.50M },
				new OrdenDetallesRequest() { Producto = "Correa", Cantidad = 3, PrecioUnitario = 10.50M }
			};

			// Verificando que más de un registro fue afectado
			int response = _ordenesServicio.Crear(request);

			Assert.True(response > 0);
		}

		[Fact]
		public void Actualizar()
		{
			OrdenesRequest request = new OrdenesRequest();
			request.Id = 2;
			request.Cliente = "Saga Falabella";

			request.Detalles = new List<OrdenDetallesRequest>()
			{
				new OrdenDetallesRequest() { Id = 4, Producto = "Polos", Cantidad = 15, PrecioUnitario = 7.50M },
				new OrdenDetallesRequest() { Id = 5, Producto = "Blusas", Cantidad = 11, PrecioUnitario = 3M },
				new OrdenDetallesRequest() { Id = 6, Producto = "Corbatas", Cantidad = 15, PrecioUnitario = 1.50M },
				new OrdenDetallesRequest() { Id = 7, Producto = "Correas", Cantidad = 13, PrecioUnitario = 1.50M }
			};

			int response = _ordenesServicio.Actualizar(request);

			// Verificando que más de un registro fue afectado
			Assert.True(response > 0);
		}

		[Fact]
		public void Obtener()
		{
			OrdenesResponse response = _ordenesServicio.Obtener(1);

			// Verificando que se obtuvo una orden con sus detalles
			Assert.NotNull(response);
		}

		[Fact]
		public void Eliminar()
		{
			int response = _ordenesServicio.Eliminar(1);

			// Verificando que más de un registro fue eliminado
			Assert.True(response > 0);
		}

		[Fact]
		public void Listar_SinFiltros()
		{
			List<OrdenesResponse> response = _ordenesServicio.Listar(string.Empty, string.Empty, string.Empty);

			// Verificando que se obtuvo al menos una orden
			Assert.NotNull(response);
		}
	}
}
