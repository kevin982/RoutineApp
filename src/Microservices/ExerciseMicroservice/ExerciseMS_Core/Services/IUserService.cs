namespace ExerciseMS_Core.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool UserIsAuthenticated();
    }
}
