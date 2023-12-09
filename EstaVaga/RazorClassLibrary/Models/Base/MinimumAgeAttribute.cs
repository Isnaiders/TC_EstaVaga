using System.ComponentModel.DataAnnotations;

namespace RazorClassLibrary.Models.Base
{
	public class MinimumAgeAttribute : ValidationAttribute
	{
		private readonly int _minimumAge;

		public MinimumAgeAttribute(int minimumAge)
		{
			_minimumAge = minimumAge;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime dateOfBirth)
			{
				DateTime today = DateTime.Today;
				int age = today.Year - dateOfBirth.Year;

				// Verifique se o aniversário deste ano já ocorreu
				if (dateOfBirth.Date > today.AddYears(-age))
				{
					age--;

					if (age >= _minimumAge)
					{
						return ValidationResult.Success;
					}
				}
				else
				{
					if (age >= _minimumAge)
					{
						return ValidationResult.Success;
					}
				}
			}

			return new ValidationResult($"Você deve ter pelo menos {_minimumAge} anos de idade.");
		}
	}
}
