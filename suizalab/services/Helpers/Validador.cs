using System.ComponentModel.DataAnnotations;

namespace services.Helpers
{
	public class Validador
	{
		internal static void ValidarModelo(object obj)
		{
			ValidationContext validationContext = new(obj);
			List<ValidationResult> validationResults = [];
			bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

			if (!isValid)
			{
				throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
			}
		}
	}
}
