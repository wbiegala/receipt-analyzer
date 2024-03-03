namespace BS.ReceiptAnalyzer.Domain.Exceptions
{
    public class InvalidStateException : ApplicationException
    {
        public InvalidStateException() { }
        public InvalidStateException(string message): base(message) { }
    }
}
