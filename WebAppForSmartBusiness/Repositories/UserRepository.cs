using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User CreateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Users (FirstName, LastName, Login, Password, Email, PhoneNumber) " +
                    $"VALUES ('{user.FirstName}', '{user.LastName}', '{user.Login}', '{user.Password}', '{user.Email}', '{user.PhoneNumber}'); " +
                    $"SELECT CAST (SCOPE_IDENTITY() AS int);";

                var idUserCreated = db.Query<int>(sqlQuery).FirstOrDefault();

                return db.Query<User>($"SELECT * FROM Users WHERE UserId = {idUserCreated}").FirstOrDefault();
            }
        }

        public bool DeleteUserById(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM Users WHERE UserId = {userId}; SELECT CAST (SCOPE_IDENTITY() AS int);")
                    is null ? true : false;
            }
        }

        public bool ExistsUserByFullName(string firstName, string lastName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>($"SELECT * FROM Users WHERE FirstName = '{firstName}' AND LastName = '{lastName}'").Any();
            }
        }

        public bool ExistsUserById(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>($"SELECT * FROM Users WHERE UserId = {userId}").Any();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>($"SELECT * FROM Users WHERE Email = {email}").FirstOrDefault();
            }
        }

        public User GetUserById(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>($"SELECT * FROM Users WHERE UserId = {userId}").FirstOrDefault();
            }
        }

        public User GetUserByName(string userName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>($"SELECT * FROM Users WHERE FirstName + ' ' + LastName = '{userName}'").FirstOrDefault();
            }
        }

        public User GetUserByNameAndEmail(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>($"SELECT * FROM Users WHERE FirstName = '{user.FirstName}' AND LastName = '{user.LastName}' " +
                    $"AND Email = '{user.Email}'").FirstOrDefault();
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public User UpdateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Users SET FirstName = '{user.FirstName}', LastName = '{user.LastName}', Login = '{user.Login}', " +
                    $"Password = '{user.Password}', Email = '{user.Email}', PhoneNumber = '{user.PhoneNumber}' " +
                    $"WHERE UserId = {user.UserId}";

                db.Execute(sqlQuery);

                return db.Query<User>($"SELECT * FROM Users WHERE UserId = {user.UserId}").FirstOrDefault();
            }
        }
    }
}
