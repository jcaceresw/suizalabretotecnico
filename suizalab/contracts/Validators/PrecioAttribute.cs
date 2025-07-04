using System.ComponentModel.DataAnnotations;

namespace contracts.Validators
{
	internal class PrecioAttribute : ValidationAttribute
	{
		public decimal Minimum { get; set; } = 0;
		public string DefaultErrorMessage { get; set; } = "Precio debe ser mayor a cero";

		public PrecioAttribute()
		{
		}

		public PrecioAttribute(decimal minimum)
		{
			Minimum = minimum;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null)
			{
				decimal price = Convert.ToDecimal(value);

				if (price < Minimum)
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
