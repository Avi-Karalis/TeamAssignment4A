using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface IStemService : IGenericService<StemDto>
    {
        new Task<IEnumerable<StemDto>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, StemDto stemDto);        
    }
}