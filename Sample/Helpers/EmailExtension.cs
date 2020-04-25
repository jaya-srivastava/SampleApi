using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace iPractice.Helpers
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        private const string defaultErrorMessage = "{0} must be a valid email address";

        public EmailAttribute() :
            //base("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$")
            //old one base("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.(?:[a-zA-Z]{2,5}|COM|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|edu)$")
            base("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.+-]+\\.(?:[a-zA-Z]{2}|COM|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|edu|academy|biz|college|education|int)$")
        { }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(defaultErrorMessage, name);
        }

        protected override ValidationResult IsValid(object value,
                                                ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!base.IsValid(value))
                {
                    return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}