using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataModel
{
    class CustomRequiredValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Vui lòng nhập vào " + validationContext.DisplayName + ".");
            }
        }

    }
}
