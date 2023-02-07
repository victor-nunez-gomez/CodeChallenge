namespace CodeChallenge.DataAccess.Interfaces;

public interface IUserRepository
{
    Task<bool> FindAsync(string username, string password);
}
