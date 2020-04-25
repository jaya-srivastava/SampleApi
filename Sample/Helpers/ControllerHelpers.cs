using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace iPractice.Helpers
{

    public static class ModelStateHelpers
    {

        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<RuleViolation> errors)
        {

            foreach (RuleViolation issue in errors)
            {
                modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
            }
        }
    }
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    //public class BooleanRequiredAttribute : ValidationAttribute, IClientValidatable
    //{
    //    public override bool IsValid(object value)
    //    {
    //        if (value is bool)
    //            return (bool)value;
    //        else
    //            return true;
    //    }

    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
    //        ModelMetadata metadata,
    //        ControllerContext context)
    //    {
    //        yield return new ModelClientValidationRule
    //        {
    //            ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
    //            ValidationType = "booleanrequired"
    //        };
    //    }
    //}

}
