using Domain.Models;

namespace Domain.Interfaces;

public interface IBeautyTechClient
{
    Task<List<BeautyTech>> GetAllBeautyTechs();
    Task<BeautyTech> AddBeautyTech(BeautyTechRequest master);
    Task<BeautyTech> GetBeautyTechById(Guid id);
    Task<List<BeautyTech>> GetBeautyTechsByProcedureName(string procedureName);
    Task<BeautyTech> UpdateBeautyTech(BeautyTechRequest beautyTech);
    Task DeleteBeautyTech(BeautyTech beautyTech);
}