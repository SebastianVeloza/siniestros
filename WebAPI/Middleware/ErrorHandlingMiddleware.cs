using log4net;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ErrorHandlingMiddleware));

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.Error("Error.", ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; 

            string result;

            if (exception.Message.StartsWith("|"))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message.Replace("|", ""), message = "A ocurrido un error, por favor contacte al administrador." });
            }
            else
            {
                result = JsonConvert.SerializeObject(new { error = "A ocurrido un error, por favor contacte al administrador.", message = exception.Message.Replace("|", "") });
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }

}
