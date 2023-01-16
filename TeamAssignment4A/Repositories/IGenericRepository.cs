using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TeamAssignment4A.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int? id);
        Task<ICollection<T>> GetAll();
        Task<T> Add(T? item);
        Task<T> Update(T? item);
        Task<int> Delete(int? id);
    }
}
