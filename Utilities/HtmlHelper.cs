using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace VisitingBook.Utilities
{
    public static class HtmlHelper
    {
        public static IHtmlContent CustomAjax(this IHtmlHelper htmlHelper, string url, string httpMethod, string updateTargetId, string triggerElementId, string eventToTrigger = "click")
        {
            // Create a div or a container where the AJAX action will be triggered (e.g., button, link, div)
            var actionTag = new TagBuilder("div");
            actionTag.Attributes.Add("id", triggerElementId);
            actionTag.Attributes.Add("style", "cursor: pointer;");

            // Add JavaScript attributes for AJAX functionality
            actionTag.Attributes.Add("data-ajax", "true");
            actionTag.Attributes.Add("data-ajax-url", url);
            actionTag.Attributes.Add("data-ajax-method", httpMethod);
            actionTag.Attributes.Add("data-ajax-update", $"#{updateTargetId}");

            // Add event trigger (default is 'click', but can be anything like 'change' for input fields)
            actionTag.Attributes.Add("data-ajax-event", eventToTrigger);

            // Add some inner content to the tag (e.g., a label or button text)
            actionTag.InnerHtml.Append("Click or Search");

            // Render the tag as HTML
            var writer = new StringBuilder();
            actionTag.WriteTo(new System.IO.StringWriter(writer), System.Text.Encodings.Web.HtmlEncoder.Default);

            // Return the tag as an IHtmlContent
            return new HtmlString(writer.ToString());
        }
    }
}