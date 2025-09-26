using Serilog;
using System.Text.Encodings.Web;

namespace OfflineTicketing.Web.Middelware
{
    public class ExceptionHandler(RequestDelegate _next)
    {
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred while processing request {Path}", context.Request.Path);
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($@"
                <script>
                    $.notify({{
                        icon: 'pe-7s-gift',
                        message: '{JavaScriptEncoder.Default.Encode(ex.Message)}'
                    }}, {{
                        type: 'error',
                        timer: 10000,
                        placement: {{
                            from: 'bottom',
                            align: 'right'
                        }}
                    }});
                    window.history.replaceState(null, '', window.location.href);
                </script>
            ");
        }

    }
}
