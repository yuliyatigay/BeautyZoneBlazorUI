using Domain.Models;

namespace Domain.Interfaces;

public interface IProcedureClient
{
    Task<List<Procedure>> GetAllProcedures();
    Task<Procedure> CreateProcedure(Procedure procedure);
    Task<Procedure> GetProcedureById(Guid id);
    Task<Procedure> UpdateProcedure(Procedure procedure);
    Task DeleteProcedure(Procedure procedure);
}