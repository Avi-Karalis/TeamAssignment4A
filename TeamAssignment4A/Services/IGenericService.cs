using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<MyDTO> Get(int id);
        Task<IEnumerable<T>?> GetAll();
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> AddOrUpdate(int id, T item);
        Task<MyDTO> GetForDelete(int id);
        Task<MyDTO> Delete(int id);
    }
}
