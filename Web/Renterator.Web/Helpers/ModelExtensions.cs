using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Renterator.Web
{
    public static class ModelExtensions
    {
        public static void AddValidationResults(this ModelStateDictionary modelState, IEnumerable<ValidationResult> errors)
        {
            foreach (ValidationResult validationResult in errors)
            {
                string memberName = validationResult.MemberNames.FirstOrDefault() ?? string.Empty;
                modelState.AddModelError(memberName, validationResult.ErrorMessage);
            }
        }
    }
}