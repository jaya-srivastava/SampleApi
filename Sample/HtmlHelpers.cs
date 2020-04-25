using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace iPractice
{
    public class HtmlHelpers
    {
        //public static string Hidden(string name, object value)
        //{
        //    return string.Format("<input type='hidden' id='{0}' name='{1}' value='{2}' />",name, name, value.ToString());
        //}

        public static MvcHtmlString Hidden(string name, object value)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", name, value.ToString()));
        }

        public static MvcHtmlString RadioButtonFor<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression, object value, string innerHtml)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var name = memberExpression.Member.Name;
            var radioButton = string.Format("<input type='radio' name='{0}' value='{1}'>{2}</input>", name, value, innerHtml);
            return MvcHtmlString.Create(radioButton);
        }


        public static MvcHtmlString RadioButtonFor(string name, object value, string id)
        {
            var radioButton = string.Format("<input type='radio' name='{0}' value='{1}' id='{2}' />", name, value, id);
            return MvcHtmlString.Create(radioButton);
        }
    }
}