using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByNameAndEmail(User user);
        User GetUserByName(string userName);
        User GetUserByEmail(string email);
        User CreateUser(User user);
        User UpdateUser(User user);
        bool DeleteUserById(int userId);
        bool ExistsUserById(int userId);
        bool ExistsUserByFullName(string firstName, string lastName);
    }
}
