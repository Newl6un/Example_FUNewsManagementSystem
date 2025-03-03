namespace LeQuangTrungMVC.BusinessObjects.ExceptionModels
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }

    }
}
