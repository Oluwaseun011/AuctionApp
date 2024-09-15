namespace AuthService.API;

public interface ICurrentUser
{
    Task GetUserAsync();
}