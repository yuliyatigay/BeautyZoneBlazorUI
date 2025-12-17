using Domain.Models;

namespace Domain.Interfaces;

public interface ICustomerClient
{
    Task<List<Customer>> GetAllCustomers();
    Task<Customer> GetCustomerById(Guid id);
    Task CreateCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task DeleteCustomer(Guid customerId);
}