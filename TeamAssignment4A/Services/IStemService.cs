using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface IStemService 
    {
        Task<MyDTO> Get(int id);
        Task<IEnumerable<StemDto>?> GetAll();
        Task<MyDTO> Add(int id, StemDto stemDto);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> Update(int id, StemDto stemDto);        
        Task<MyDTO> GetForDelete(int id);
        Task<MyDTO> Delete(int id);
    }
}