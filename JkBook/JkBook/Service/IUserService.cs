namespace JkBook.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}