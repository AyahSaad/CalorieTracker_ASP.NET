using CalorieTracker.DAL.DTO.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace CalorieTracker.PL
{
    public class GlobalExceptionHandler: IExceptionHandler
    {

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unexpected error: {Message}", exception.Message);

            var errorDetails = new ErrorDetails()
            {
                StatusCode  = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred. Please try again later."
                //StackTrace = e.InnerException.Message
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(errorDetails, cancellationToken);
            return true;
        }
    }
}
