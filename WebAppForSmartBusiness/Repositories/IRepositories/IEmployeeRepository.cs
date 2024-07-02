using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Models.Helpers;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesBySubdivision(string subdivisionName);
        IEnumerable<Employee> GetEmployeesByPosition(string positionName);
        IEnumerable<EmployeeForView> GetEmployeesWithFullInfo();
        EmployeeForView GetEmployeesWithFullInfoById(int employeeId);
        IEnumerable<EmployeeForView> GetEmployeeWithFilter(string filter);
        Employee GetEmployeeById(int employeeId);
        Employee GetEmployeeByName(string employeeName);
        Employee CreateEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        bool DeleteEmployeeById(int employeeId);
        bool ExistsEmployeeById(int employeeId);
        bool ExistsEmployeeByFullName(string employeeName);
    }
}
