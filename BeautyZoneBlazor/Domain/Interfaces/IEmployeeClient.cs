using Domain.Models;

namespace Domain.Interfaces;

public interface IEmployeeClient
{
    Task<List<Employee>> GetAllEmployees();
    Task<Employee> CreateEmployee(EmployeeRequest master);
    Task<Employee> GetEmployeeById(Guid id);
    Task<List<Employee>> GetEmployeesByProcedure(string procedureName);
    Task<Employee> UpdateEmployee(EmployeeRequest master);
    Task DeleteEmployee(Employee master);
}