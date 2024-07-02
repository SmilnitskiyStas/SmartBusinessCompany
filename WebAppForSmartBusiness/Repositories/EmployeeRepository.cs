using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Models.Helpers;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Employee CreateEmployee(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Employees (UserId, SubdivisionId, PositionId, Status, PeoplePartnerId, OutOfOfficeBalance)" +
                    $"VALUES ({employee.UserId}, {employee.SubdivisionId}, {employee.PositionId}, " +
                    $"'{employee.Status}', {employee.PeoplePartnerId}, {employee.OutOfOfficeBalance})";

                return db.Query<Employee>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteEmployeeById(int employeeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM Employees WHERE EmployeeId = {employeeId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null;
            }
        }

        public bool ExistsEmployeeByFullName(string employeeName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE EmployeeId = {employeeName}").Any();
            }
        }

        public bool ExistsEmployeeById(int employeeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE EmployeeId = {employeeId}").Any();
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE EmployeeId = {employeeId}").FirstOrDefault();
            }
        }

        public Employee GetEmployeeByName(string employeeName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE FullName = {employeeName}").FirstOrDefault();
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees").ToList();
            }
        }

        public IEnumerable<Employee> GetEmployeesByPosition(string positionName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE Position = {positionName}").ToList();
            }
        }

        public IEnumerable<Employee> GetEmployeesBySubdivision(string subdivisionName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE Subdivision = {subdivisionName}").ToList();
            }
        }

        public IEnumerable<EmployeeForView> GetEmployeesWithFullInfo()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT " +
                    "e.EmployeeId AS EmployeeId, " +
                    "u.FirstName AS FirstName, " +
                    "u.LastName AS LastName, " +
                    "s.Name AS Subdivision, " +
                    "p.Name AS Position, " +
                    "e.Status AS Status, " +
                    "pp.FirstName + ' ' + pp.LastName AS PeoplePartner, " +
                    "e.OutOfOfficeBalance AS OutOfOfficeBalance, " +
                    "u.PhoneNumber AS PhoneNumber " +
                    "FROM Employees AS e " +
                    "LEFT JOIN Users AS u ON e.UserId = u.UserId " +
                    "LEFT JOIN Subdivisions AS s ON e.SubdivisionId = s.SubdivisionId " +
                    "LEFT JOIN Positions AS p ON e.PositionId = p.PositionId " +
                    "LEFT JOIN Users AS pp ON e.PeoplePartnerId = pp.UserId ";

                var users = db.Query<EmployeeForView>(sqlQuery).ToList();

                return users;
            }
        }

        public EmployeeForView GetEmployeesWithFullInfoById(int employeeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT " +
                    "e.EmployeeId AS EmployeeId, " +
                    "u.FirstName AS FirstName, " +
                    "u.LastName AS LastName, " +
                    "s.Name AS Subdivision, " +
                    "p.Name AS Position, " +
                    "e.Status AS Status, " +
                    "pp.FirstName + ' ' + pp.LastName AS PeoplePartner, " +
                    "e.OutOfOfficeBalance AS OutOfOfficeBalance, " +
                    "u.PhoneNumber AS PhoneNumber " +
                    "FROM Employees AS e " +
                    "LEFT JOIN Users AS u ON e.UserId = u.UserId " +
                    "LEFT JOIN Subdivisions AS s ON e.SubdivisionId = s.SubdivisionId " +
                    "LEFT JOIN Positions AS p ON e.PositionId = p.PositionId " +
                    "LEFT JOIN Users AS pp ON e.PeoplePartnerId = pp.UserId " +
                    "WHERE 1=1 " +
                    $"AND EmployeeId = {employeeId}";

                var users = db.Query<EmployeeForView>(sqlQuery).FirstOrDefault();

                return users;
            }
        }

        public IEnumerable<EmployeeForView> GetEmployeeWithFilter(string filter)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT " +
                    "e.EmployeeId AS EmployeeId, " +
                    "u.FirstName AS FirstName, " +
                    "u.LastName AS LastName, " +
                    "s.Name AS Subdivision, " +
                    "p.Name AS Position, " +
                    "e.Status AS Status, " +
                    "pp.FirstName + ' ' + pp.LastName AS PeoplePartner, " +
                    "e.OutOfOfficeBalance AS OutOfOfficeBalance, " +
                    "u.PhoneNumber AS PhoneNumber " +
                    "FROM Employees AS e " +
                    "LEFT JOIN Users AS u ON e.UserId = u.UserId " +
                    "LEFT JOIN Subdivisions AS s ON e.SubdivisionId = s.SubdivisionId " +
                    "LEFT JOIN Positions AS p ON e.PositionId = p.PositionId " +
                    "LEFT JOIN Users AS pp ON e.PeoplePartnerId = pp.UserId " +
                    "WHERE 1=1 " +
                    $"{filter}";

                var users = db.Query<EmployeeForView>(sqlQuery);

                return users;
            }
        }

        public Employee UpdateEmployee(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Employees SET UserId = {employee.UserId}, SubdivisionId = {employee.SubdivisionId}, " +
                $"PositionId = {employee.PositionId}, Status = '{employee.Status}', PeoplePartnerId = {employee.PeoplePartnerId}, " +
                $"OutOfOfficeBalance = {employee.OutOfOfficeBalance} " +
                $"WHERE EmployeeId = {employee.EmployeeId}";

                db.Execute(sqlQuery);

                return db.Query<Employee>($"SELECT * FROM Employees WHERE EmployeeId = {employee.EmployeeId}").FirstOrDefault();
            }
        }
    }
}
