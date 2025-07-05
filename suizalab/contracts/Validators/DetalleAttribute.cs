using contracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace contracts.Validators
{
	public class DetalleAttribute : ValidationAttribute
	{
		public string DefaultErrorMessage { get; set; } = "Lista de Detalles debe contener al menos un registro";

		public DetalleAttribute()
		{
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{ 
			// Objeto vacío
			if (value is null)
			{
				throw new ArgumentException(DefaultErrorMessage);
			}

			// Objeto sin detalles
			List<OrdenDetallesRequest> detalles = (List<OrdenDetallesRequest>)value;

			if (detalles.Count < 1)
			{
				throw new ArgumentException(DefaultErrorMessage);
			}

			foreach (OrdenDetallesRequest request in detalles)
			{
				ValidationContext modelContext = new(request);
				List<ValidationResult> validationResults = [];
				bool isValid = Validator.TryValidateObject(request, modelContext, validationResults, true);

				if (!isValid)
				{
					throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
				}
			}

			return null;
		}
	}
}
