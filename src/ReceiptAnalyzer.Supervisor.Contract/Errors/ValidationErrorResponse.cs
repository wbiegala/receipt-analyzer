namespace BS.ReceiptAnalyzer.Supervisor.Contract.Errors
{
    public class ValidationErrorResponse
    {
        public string Message { get; set; }
        public IEnumerable<ValidationFieldError>? FieldErrors { get; set; }

        public class ValidationFieldError
        {
            public string FieldName { get; set; }
            public string Message { get; set; }
        }
    }
}
