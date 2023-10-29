using Vpat.Models;
namespace Vpat.DAO
{
    public interface IUserDao
    {
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        User AddUser(string username, string email, string password, string role);
        bool DeactivateUser(string username);
        bool DeleteUsers();
    }
}
