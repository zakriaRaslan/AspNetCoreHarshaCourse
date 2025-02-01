using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationExapmle.CustomValidators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ModelValidationExapmle.Models
{
    public class Person:IValidatableObject
    {
        [Required(ErrorMessage = "{0} can't be Empty")]
        [DisplayName("Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} Length Should between {2} and {1}")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} Should be alphabets only ")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="{0} Can't be Blank")]
        [EmailAddress(ErrorMessage = "The Email is Not Valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} Can't be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage ="{0} Can't be blank")]
        [DisplayName("Confirm Password")]
        [Compare("Password" ,ErrorMessage ="{1} and {0} Should be the same")]
        public string? ConfirmPassword { get; set; }

        [Phone(ErrorMessage ="{0} Is Not valid ")]
        [DisplayName("Phone Number")]
        [ValidateNever] // this will ignore all validation
        public string? PhoneNumber { get; set; }

        [Required]
        [Range(0,999.99 , ErrorMessage = "{0} must be in range of {1} $ to {2} $")]
        public double? Price { get; set; }

        [MinimumBirthYearValidator(2002,ErrorMessage = "The Birth Date must be bigger than {0}")]
        public DateTime? BirthDate { get; set; }

        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate" , ErrorMessage = "'From Date' Must Be Older Than To Date")]
        public DateTime? ToDate { get; set; }

        //  [BindNever] // this attribute will make the property will never bind 
        public DateTime? Age { get; set; }

        public List<string> Tags { get; set; } = new();




        public override string ToString()
        {
            return $"The Person Name => {Name} , Email => {Email} , Password => {Password} ," +
                $"Confirm Password => {ConfirmPassword} , Phone Number => {PhoneNumber} And  " +
                $"Price is => {Price}";
        }

        // this validation will run only when all the other property was true
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(BirthDate.HasValue == false && Age.HasValue == false)
            {
                yield return new ValidationResult("Either Age Or BirthDate it must be supplied" , new[] {nameof(Age)});
            }
        }
    }
}
