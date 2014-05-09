﻿using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Helper method used to render a List box based on chosen package
        /// </summary>
        public static MvcHtmlString BootstrapListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string dataPlaceholder)
        {
            var attributes = new Dictionary<string, object>
            {
                {"class", "form-control chosen-select"},
                {"data-Placeholder", dataPlaceholder}
            };

            if (!(bool)htmlHelper.ViewBag.IsLongRunningPredictionsEnabled)
            {
                attributes.Add("disabled", "disabled");
            }
            
            return htmlHelper.ListBoxFor(expression, selectList, attributes);
        }

    }
}