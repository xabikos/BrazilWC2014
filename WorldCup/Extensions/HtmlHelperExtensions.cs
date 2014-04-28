using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WorldCup.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Helper method that adds the required class for bootstrap form controls
        /// </summary>
        public static MvcHtmlString BootstrapEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return html.EditorFor(expression, new {htmlAttributes = new {@class = "form-control"}});
        }

    }
}