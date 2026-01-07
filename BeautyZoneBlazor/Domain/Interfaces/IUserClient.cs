using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces;

public interface IUserClient
{
    Task<List<User>> GetAllUsers();
    Task<User> GetUserById(Guid id);
    Task<(bool Success, string Message)> UpdateAccount(User user);
    Task DeleteUser(Guid userId);
}