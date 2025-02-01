using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationExapmle.Models;

namespace ModelValidationExapmle.CustomModelBinders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new();

            //Name
            if(bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            {
                person.Name = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
                if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
                    person.Name += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
            }

            //Email
            if (bindingContext.ValueProvider.GetValue(nameof(person.Email)).Length > 0)
                person.Email = bindingContext.ValueProvider.GetValue(nameof(person.Email)).FirstValue;

            //Password
            if (bindingContext.ValueProvider.GetValue(nameof(person.Password)).Length > 0)
                person.Password = bindingContext.ValueProvider.GetValue(nameof(person.Password)).FirstValue;

            //ConfirmPassword
            if (bindingContext.ValueProvider.GetValue(nameof(person.ConfirmPassword)).Length > 0)
                person.ConfirmPassword = bindingContext.ValueProvider.GetValue(nameof(person.ConfirmPassword)).FirstValue;

            //PhoneNumber
            if (bindingContext.ValueProvider.GetValue(nameof(person.PhoneNumber)).Length > 0)
                person.PhoneNumber = bindingContext.ValueProvider.GetValue(nameof(person.PhoneNumber)).FirstValue;

            //Price
            if (bindingContext.ValueProvider.GetValue(nameof(person.Price)).Length > 0)
                person.Price = Convert.ToDouble(bindingContext.ValueProvider.GetValue(nameof(person.Price)).FirstValue);

            //BirthDate
            if (bindingContext.ValueProvider.GetValue(nameof(person.BirthDate)).Length > 0)
                person.BirthDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue(nameof(person.BirthDate)).FirstValue);

            //FromDate
            if (bindingContext.ValueProvider.GetValue(nameof(person.FromDate)).Length > 0)
                person.FromDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue(nameof(person.FromDate)).FirstValue);

            //ToDate
            if (bindingContext.ValueProvider.GetValue(nameof(person.ToDate)).Length > 0)
                person.ToDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue(nameof(person.ToDate)).FirstValue);

            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;

        }
    }
}
