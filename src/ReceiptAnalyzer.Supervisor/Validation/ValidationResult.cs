using BS.ReceiptAnalyzer.Supervisor.Contract.Errors;

namespace BS.ReceiptAnalyzer.Supervisor.Validation
{
    public sealed record ValidationResult
    {
        public bool IsValid { get; init; }
        public string? Message { get; init; }

        public IEnumerable<ValidationFail> Fails { get; init; } = new List<ValidationFail>();

        public sealed record ValidationFail
        {
            public string FieldName { get; init; }
            public string Message { get; init; }
        }
    }

    public static class ValidationResultExtensions
    {
        public static ValidationErrorResponse ToResponse(this ValidationResult result) => new ValidationErrorResponse
        {
            Message = result.Message,
            FieldErrors = result.Fails.Select(fail => new ValidationErrorResponse.ValidationFieldError
            {
                FieldName = fail.FieldName,
                Message = fail.Message,
            })
        };
    }
}
