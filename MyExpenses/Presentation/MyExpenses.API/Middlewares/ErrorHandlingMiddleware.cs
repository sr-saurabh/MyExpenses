using System.Net;
using MyExpenses.Domain.core.Models.Responses;

namespace MyExpenses.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (Exception ex)
            {
                Response response = new Response() { StatusCode = ResponseStatus.Error };
                HttpStatusCode code;
                switch (ex)
                {
                    case KeyNotFoundException
               // or NoSuchUserException
               or FileNotFoundException:
                        code = HttpStatusCode.NotFound;
                        break;
                    //case EntityAlreadyExists:
                    //    code = HttpStatusCode.Conflict;
                    //    break;
                    case UnauthorizedAccessException:
                        // or ExpiredPasswordException
                        //or UserBlockedException:
                        code = HttpStatusCode.Unauthorized;
                        break;
                    case ArgumentException
               or InvalidOperationException:
                        code = HttpStatusCode.BadRequest;
                        break;
                    default:
                        code = HttpStatusCode.InternalServerError;
                        break;
                }

                context.Response.StatusCode = (int)code;    
                //ex.Message= Uri.EscapeDataString(ex.Message);
                response.Message = Uri.EscapeDataString(ex.Message);

                //if (_webHostEnvironment.IsDevelopment())
                //{
                //    response.Exception = ex.FlattenException();
                //}

                await context.Response.WriteAsJsonAsync(response);
            }

            // Log the response
            // LogResponse(context);

        }
    }
}
