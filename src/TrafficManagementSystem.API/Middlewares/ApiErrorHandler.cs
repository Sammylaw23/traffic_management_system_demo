using System.Net;
using TrafficManagementSystem.Application.Exceptions;
using TrafficManagementSystem.Application.Wrappers;

namespace TrafficManagementSystem.API.Middlewares
{
    public class ApiErrorHandler
    {
        private readonly RequestDelegate _next;
        private ILogger<ApiErrorHandler> _logger;

        public ApiErrorHandler(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ApiErrorHandler>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>()
                {
                    Succeeded = false,
                    Data = "Operation failed.",
                    Messages = new List<string>()
                };

                switch (error)
                {
                    case InvalidEmailAddressException e:
                        responseModel.Messages.Add(e.Message);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ApiException e:
                        responseModel.Messages.Add(e.Message);
                        response.StatusCode = e.StatusCode ?? (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Messages.AddRange(e.Errors);
                        break;
                    case NotFoundException e:
                        responseModel.Messages.Add(e.Message);
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Data = null;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
#if DEBUG
                        responseModel.Messages.Add(error.Message);
#else
                        if (responseModel.Messages.Count == 0)
                            responseModel.Messages.Add("An error occurred.");
#endif
                        break;
                }
                await response.WriteAsJsonAsync(responseModel);
            }
        }
    }
}
