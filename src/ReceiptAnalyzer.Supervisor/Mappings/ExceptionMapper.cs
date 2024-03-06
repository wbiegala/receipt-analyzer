using BS.ReceiptAnalyzer.Supervisor.Contract.Errors;

namespace BS.ReceiptAnalyzer.Supervisor.Mappings
{
    public static class ExceptionMapper
    {
        public static ExceptionErrorResponse MapToResponse(Exception exception) =>
            new ExceptionErrorResponse
            {
                Exception = exception.GetType().FullName ?? exception.GetType().Name,
                Message = exception.Message,
            };
    }
}
