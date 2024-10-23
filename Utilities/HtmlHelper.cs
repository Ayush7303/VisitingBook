using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text;

namespace VisitingBook.Utilities
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent CustomAjax(this IHtmlHelper htmlHelper, string url, string httpMethod, string updateTargetId)
        {
            // Create a div where the AJAX action will be displayed
            var resultDiv = new TagBuilder("div");
            resultDiv.Attributes.Add("id", updateTargetId);
            resultDiv.Attributes.Add("style", "font-size: 2em; font-weight: bold;"); // Customize styling as needed

            // Render the result div as HTML
            var writer = new StringBuilder();
            resultDiv.WriteTo(new System.IO.StringWriter(writer), System.Text.Encodings.Web.HtmlEncoder.Default);

            // JavaScript to handle AJAX call and display JSON result on page load
            var script = $@"
                <script>
                    $(document).ready(function() {{
                        $.ajax({{
                            url: '{url}',
                            method: '{httpMethod}',
                            dataType: 'json',
                            success: function(data) {{
                                $('#{updateTargetId}').text(data.count); // Assuming the JSON response is like {{ count: value }}
                            }},
                            error: function(xhr, status, error) {{
                                console.error('AJAX Error:', error);
                                $('#{updateTargetId}').text('Error retrieving data.'); // Display error message
                            }}
                        }});
                    }});
                </script>
            ";

            writer.Append(script); // Append the script to the writer

            // Return the result div and the script as an IHtmlContent
            return new HtmlString(writer.ToString());
        }
       
 public static IHtmlContent CustomAjaxForm(this IHtmlHelper htmlHelper, string formId, string url, string httpMethod, string successMessage, string errorMessage)
{
    // JavaScript to handle AJAX form submission
    var script = $@"
    <script>
        $(document).ready(function() {{
            $('#{formId}').on('submit', function(event) {{
                event.preventDefault(); // Prevent default form submission

                var formData = $(this).serializeArray(); // Serialize form data to an array
                var jsonData = {{}}; // Create an empty object for JSON

                // Convert the serialized array to a JSON object
                formData.forEach(function(item) {{
                    jsonData[item.name] = item.value; // Assign name-value pairs
                }});

                // Set the required fields
                jsonData['CreatedOn'] = new Date().toISOString(); // Set current date as CreatedOn
                jsonData['CreatedBy'] = 'System'; // Replace with actual user if available
                jsonData['UpdatedOn'] = new Date().toISOString(); // Set current date as UpdatedOn
                jsonData['UpdatedBy'] = 'System'; // Replace with actual user if available

                $.ajax({{
                    url: '{url}',
                    method: '{httpMethod}',
                    contentType: 'application/json', // Set the content type to JSON
                    data: JSON.stringify(jsonData), // Convert object to JSON string
                    success: function(response) {{
                        // alert('{successMessage}');
                        $('#{formId}')[0].reset(); // Reset form.
                        
   //TODO Reload Table On Insertion                     
                    }},
                    error: function(xhr, status, error) {{
                        // alert('{errorMessage} ' + xhr.responseText);
                    }}
                }});
            }});
        }});
    </script>";

    return new HtmlString(script);
}




   public static IHtmlContent CustomDataTableAjax(this IHtmlHelper htmlHelper, string tableId, string url)
{
    var sb = new StringBuilder();

    sb.AppendLine("<div class='card-body'>");
    sb.AppendLine($"<table id='{tableId}' class='table table-bordered table-striped'>");
    sb.AppendLine("<thead>");
    sb.AppendLine("<tr id='table-header'></tr>"); // Empty header row for dynamic generation
    sb.AppendLine("</thead>");
    sb.AppendLine("<tbody id='table-body'></tbody>"); // Empty body for dynamic data
    sb.AppendLine("<tfoot>");
    sb.AppendLine("<tr id='table-footer'></tr>"); // Empty footer row for dynamic generation
    sb.AppendLine("</tfoot>");
    sb.AppendLine("</table>");
    sb.AppendLine("</div>");

    sb.AppendLine(@"
    <script src='https://code.jquery.com/jquery-3.6.0.min.js'></script>
    <script src='https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js'></script>
    <script src='https://cdn.datatables.net/buttons/2.3.5/js/dataTables.buttons.min.js'></script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js'></script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js'></script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js'></script>
    <script src='https://cdn.datatables.net/buttons/2.3.5/js/buttons.html5.min.js'></script>
    <script src='https://cdn.datatables.net/buttons/2.3.5/js/buttons.print.min.js'></script>
    <script>
        $(function () {
            // Fetch table data via AJAX
            $.ajax({
                url: '" + url + @"',
                method: 'GET',
                dataType: 'json',
                success: function(data) {
                    if (Array.isArray(data) && data.length > 0) {
                        // Generate header
                        var keys = Object.keys(data[0]);
                        var headerRow = $('#table-header');
                        headerRow.empty(); // Clear existing headers
                        keys.forEach(function(key) {
                            headerRow.append($('<th>').text(key)); // Create <th> for each key
                        });

                        // Populate table body
                        var tbody = $('#table-body');
                        tbody.empty(); // Clear existing rows
                        data.forEach(function(row) {
                            var tr = $('<tr>');
                            keys.forEach(function(key) {
                                tr.append($('<td>').text(row[key] ?? '')); // Create <td> for each value, handle undefined
                            });
                            tbody.append(tr);
                        });

                        // Generate footer (optional, same as header)
                        var footerRow = $('#table-footer');
                        footerRow.empty(); // Clear existing footers
                        keys.forEach(function(key) {
                            footerRow.append($('<th>').text(key)); // Create <th> for each key
                        });

                        // Check if DataTable is already initialized and destroy it
                        if ($.fn.DataTable.isDataTable('#" + tableId + @"')) {
                            $('#" + tableId + @"').DataTable().clear().destroy();
                        }

                        // Initialize DataTable
                        $('#" + tableId + @"').DataTable({
                            'responsive': true,
                            'lengthChange': false,
                            'autoWidth': false,
                            'buttons': ['copy', 'csv', 'excel', 'pdf', 'print', 'colvis'],
                            'pageLength': 6
                        }).buttons().container().appendTo('#" + tableId + @"_wrapper .col-md-6:eq(0)');
                    } else {
                        $('#table-body').append('<tr><td colspan=\'100%\'>No data available.</td></tr>');
                    }
                },
                error: function(xhr, status, error) {
                    console.error('AJAX Error:', error);
                    $('#table-body').append('<tr><td colspan=\'100%\'>Error retrieving data.</td></tr>');
                }
            });
        }); 
    </script>");
    return new HtmlString(sb.ToString());
}


        
    }
}