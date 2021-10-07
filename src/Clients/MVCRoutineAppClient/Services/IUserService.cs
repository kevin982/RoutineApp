namespace MVCRoutineAppClient.Services
{
	public interface IUserService
	{
		bool IsAdmin(string accessToken);
	}
}