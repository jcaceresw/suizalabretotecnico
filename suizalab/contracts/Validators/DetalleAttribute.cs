using contracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace contracts.Validators
{
	public class DetalleAttribute: ValidationAttribute
	{
		public string DefaultErrorMessage { get; set; } = "Lista de detalle no debe estar vacía";

		public DetalleAttribute()
		{
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is null)
			{
				throw new ArgumentException(DefaultErrorMessage);
			}

			foreach (OrdenDetallesRequest request in (List<OrdenDetallesRequest>)value)
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
