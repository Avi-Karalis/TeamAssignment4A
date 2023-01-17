using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TeamAssignment4A.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(int? id);
        Task<ICollection<T?>> GetAllAsync();
        Task<int> AddOrUpdateAsync(T? item);        
        Task<bool> DeleteAsync(int? id);
    }
}
