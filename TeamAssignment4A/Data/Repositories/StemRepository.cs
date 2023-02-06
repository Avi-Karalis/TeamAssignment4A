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
            return await _db.Stems.Include(stem => stem.Topic)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Stem>?> GetAllAsync()
        {
            return await _db.Stems.Include(stem => stem.Topic).ToListAsync<Stem>();
        }

        public async Task<IEnumerable<int>?> GetStemIdsByCert(Certificate certificate)
        {
            return await _db.Stems.Where(stem => stem.Topic.Certificate == certificate)
                .Select(stem => stem.Id).ToListAsync();
        }

        public async Task<IEnumerable<Stem>?> GetStemsByTopic(string description)
        {
            return await _db.Stems.Where(stem => stem.Topic.Description == description).ToListAsync();
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
