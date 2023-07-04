using System.Net;

namespace LinkUpWorld.UsersMicroservice.Application.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundException(context, ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);
            }
            catch (UnauthorizedException ex)
            {
                await HandleUnauthorizedException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleOtherExceptions(context, ex);
            }           
        }

        private Task HandleNotFoundException(HttpContext context, NotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return context.Response.WriteAsync(ex.Message);
        }

        private Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(string.Join(Environment.NewLine, ex.Errors));
        }

        private Task HandleUnauthorizedException(HttpContext context, UnauthorizedException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return context.Response.WriteAsync(ex.Message);
        }

        private Task HandleOtherExceptions(HttpContext context, Exception ex)
        {            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}
