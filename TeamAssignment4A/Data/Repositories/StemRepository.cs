using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class StemRepository : IGenericRepository<Stem>
    {
        private readonly WebAppDbContext _db;
        public StemRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Stem?> GetAsync(int id)
        {
            return await _db.Stems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Stem>?> GetAllAsync()
        {
            return await _db.Stems.ToListAsync<Stem>();
        }

        public EntityState AddOrUpdate(Stem stem)
        {
            _db.Stems.Update(stem);
            return _db.Entry(stem).State;
        }

        public void Delete(Stem stem)
        {
            _db.Stems.Remove(stem);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Stems.AnyAsync(e => e.Id == id);
        }
    }
}
