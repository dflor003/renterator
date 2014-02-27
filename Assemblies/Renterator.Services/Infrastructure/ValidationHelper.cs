using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Renterator.Services.Infrastructure
{
    public static class ValidationHelper
    {
        public static void ValidateModel(object obj)
        {
            ValidationContext context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, context, results, validateAllProperties: true))
            {
                throw new ValidationException(results);
            }
        }
    }
}
