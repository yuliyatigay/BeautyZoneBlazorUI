using Domain.Models;

namespace Domain.Interfaces;

public interface IAuthClient
{
    Task<(bool Success, string Message)> Register(UserRegister user);
    Task<(bool success, string message)> Login(UserLogin login);
}