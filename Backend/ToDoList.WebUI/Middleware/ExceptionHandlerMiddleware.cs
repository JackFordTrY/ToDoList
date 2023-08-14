using FluentValidation;
using System.Text;
using ToDoList.Domain.Exceptions;
using static System.Net.HttpStatusCode;

namespace ToDoList.WebUI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var code = exception switch
            {
                UserAlreadyExistsException or
                UserDoesntExistException or
                ValidationException => BadRequest,
                _ => InternalServerError
            };

            var message = new StringBuilder(exception.Message);

            if (exception is ValidationException)
            {
                message.Clear().Append("Entered data did not match validation requirements.");
            }

            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsJsonAsync(new { message = message.ToString(), code = (int)code });
        }

    }
}
