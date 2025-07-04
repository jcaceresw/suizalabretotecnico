using System.ComponentModel.DataAnnotations;

namespace contracts.Validators
{
	internal class CantidadAttribute: ValidationAttribute
	{
		public int Minimum { get; set; } = 0;
		public string DefaultErrorMessage { get; set; } = "Cantidad debe ser mayor a cero";

		public CantidadAttribute()
		{
		}

		public CantidadAttribute(int minimum)
		{
			Minimum = minimum;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null)
			{
				int quantity = Convert.ToInt32(value);

				if (quantity < Minimum)
				{
					return new ValidationResult(string.Format(DefaultErrorMessage, Minimum));
				}
				else
				{
					return ValidationResult.Success;
				}
			}

			return null;
		}
	}
}
