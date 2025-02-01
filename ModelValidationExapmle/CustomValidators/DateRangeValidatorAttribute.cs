using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidationExapmle.CustomValidators
{
    public class DateRangeValidatorAttribute:ValidationAttribute
    {
        private string _otherPropertyName {  get; set; }
        public DateRangeValidatorAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null) 
            {
                DateTime Todate = Convert.ToDateTime(value);

                PropertyInfo? opjectProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
                if (opjectProperty != null) 
                {
                    DateTime FromDate = Convert.ToDateTime(opjectProperty.GetValue(validationContext.ObjectInstance));
                    if (FromDate > Todate) 
                    {
                        return new ValidationResult(ErrorMessage, new string[] { _otherPropertyName, validationContext.MemberName });
                    }
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
