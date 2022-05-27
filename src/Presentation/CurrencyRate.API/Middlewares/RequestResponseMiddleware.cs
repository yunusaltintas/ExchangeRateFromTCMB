using CurrencyRate.Application.Dtos.BaseResponse;
using CurrencyRate.Application.Dtos.Exceptions;
using System.Net;
using System.Text.Json;

namespace CurrencyRate.API.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseMiddleware
        (
            RequestDelegate next
        )
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            Exception? exception = null;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;

                var response = HandleException(context, ex);

                await context.Response.WriteAsync(response);
            }

        }
        private string HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            ErrorMessage errorMessage = new();
            if (exception is CustomException customException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorMessage = customException.ErrorMessage;
            }
            else
            {
                errorMessage.ErrorDescription = exception.Message;
            }

            var response = new CustomResponseDto().Error(context.Response.StatusCode, errorMessage);

            return JsonSerializer.Serialize(response);
        }
    }
}