using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TeamAssignment4A.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<ICollection<T>?> GetAllAsync();
        EntityState AddOrUpdate(T item);        
        void Delete(T item);
        Task<bool> Exists(int id);
    }
}
