using System.ComponentModel.DataAnnotations;

namespace ModelValidationExapmle.CustomValidators
{
    public class MinimumBirthYearValidatorAttribute:ValidationAttribute
    {
        private int _year { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "The Year Of Birth Must be not less than {0}";
        public MinimumBirthYearValidatorAttribute()
        {

        }  
        public MinimumBirthYearValidatorAttribute(int Year)
        {
            _year = Year;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime date = (DateTime)value;

                if(date.Year > _year)
                {
                    return new ValidationResult(string.Format(ErrorMessage??DefaultErrorMessage,_year));
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
