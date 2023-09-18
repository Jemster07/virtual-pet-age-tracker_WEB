using Vpat.Models;
namespace Vpat.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string email, string password, string role);
    }
}
