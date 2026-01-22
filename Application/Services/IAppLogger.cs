namespace Application.Services
{
    public interface IAppLogger<T>
    {
        void Info(string message);
        void Error(string message, Exception ex = null);
    }
}
