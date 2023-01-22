using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface IStemService
    {
        Task<MyDTO> GetStem(int id);
        Task<ICollection<Stem>?> GetAllStems();
        Task<MyDTO> AddOrUpdateStem(int id, Stem Stem);
        Task<MyDTO> Delete(int id);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> GetForDelete(int id);
    }
}