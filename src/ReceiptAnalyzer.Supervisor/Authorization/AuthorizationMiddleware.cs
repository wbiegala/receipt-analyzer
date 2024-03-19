using BS.ReceiptAnalyzer.Supervisor.Contract.Errors;
using System.Text.Json;

namespace BS.ReceiptAnalyzer.Supervisor.Authorization
{
    public class AuthorizationMiddleware : IMiddleware
    {
        private readonly IApiKeyAuthorizationService _authService;

        public AuthorizationMiddleware(IApiKeyAuthorizationService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var header = context.GetApiKey();
            var isAuthorized = await _authService.AuthorizeAsync(header);

            if (!isAuthorized)
            {
                await HandleUnauthorizedAsync(context);
                return;
            }

            await next(context);
        }

        private async Task HandleUnauthorizedAsync(HttpContext context)
        {
            var dto = new UnauthorizedErrorResponse
            {
                Message = "Empty or invalid Api Key"
            };

            context.Response.StatusCode = 401;
            context.Response.Headers.Add("Content-Type", "Application/Json");
            await context.Response.WriteAsync(JsonSerializer.Serialize(dto, options));
        }

        private static JsonSerializerOptions options =>
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
    }
}
