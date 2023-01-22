using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface IStemService
    {
        Task<MyDTO> GetStem(int id);
        Task<IEnumerable<StemDto>?> GetAllStems();
        Task<MyDTO> AddOrUpdateStem(int id, StemDto stemDto);
        Task<MyDTO> Delete(int id);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> GetForDelete(int id);
    }
}