namespace LeQuangTrungMVC.BusinessObjects.ExceptionModels
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string? message) : base(message)
        {
        }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message) : base(message)
        {
        }
    }

}
