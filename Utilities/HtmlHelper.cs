using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text;

namespace VisitingBook.Utilities
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent CustomAjax(this IHtmlHelper htmlHelper, string url, string httpMethod, string updateTargetId, string triggerElementId, string? eventToTrigger = "click", string? innerHtml = "Click or Search")
        {
            // Create a div or a container where the AJAX action will be triggered
            var actionTag = new TagBuilder("div");
            actionTag.Attributes.Add("id", triggerElementId);
            actionTag.Attributes.Add("style", "cursor: pointer;");

            // Add JavaScript attributes for AJAX functionality
            actionTag.Attributes.Add("data-ajax", "true");
            actionTag.Attributes.Add("data-ajax-url", url);
            actionTag.Attributes.Add("data-ajax-method", httpMethod.ToUpper());
            actionTag.Attributes.Add("data-ajax-update", $"#{updateTargetId}");

            // Add event trigger (default is 'click', but can be anything like 'change' for input fields)
            actionTag.Attributes.Add("data-ajax-event", eventToTrigger);

            // Add inner content to the tag
            actionTag.InnerHtml.Append(innerHtml);

            // Render the tag as HTML
            var writer = new StringBuilder();
            actionTag.WriteTo(new System.IO.StringWriter(writer), System.Text.Encodings.Web.HtmlEncoder.Default);

            // Return the tag as an IHtmlContent
            return new HtmlString(writer.ToString());
        }

        public static IHtmlContent AjaxPostForm(this IHtmlHelper?    htmlHelper,string actionUrl,string buttonID,object data)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<form id='ajaxform' method='post'");

            foreach(var property in data.GetType().GetProperties())
            {
                var value = property.GetValue(data);
                stringBuilder.AppendLine($"<input type='hidden' name='{property.Name}' value='{value}'/>");
            }

            stringBuilder.AppendLine("<button type='button' id='" + buttonID + "'/>");
            stringBuilder.AppendLine("</form");

            stringBuilder.AppendLine(@"
             <script src='https://code.jquery.com/jquery-3.6.0.min.js'></script>
                <script>
                $("+ buttonID +@").on('click',function(){
                    $.ajax({
                        url : "+actionUrl + @",
                        method : POST,
                        data:$('#ajaxform').serialize(),
                        success:unction(response)
                        {
                            alert('Data Inserted Successfully.');`
                        },
                        error:fucntion(err)
                        {
                            alert(err);
                        }
                    });
                });
                </script>
            ");

             return new HtmlString(stringBuilder.ToString());
        }
    }
}
