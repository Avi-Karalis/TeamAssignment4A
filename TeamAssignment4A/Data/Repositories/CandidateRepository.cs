using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class CandidateRepository : IGenericRepository<Candidate>
    {
        private readonly WebAppDbContext _db;
        public CandidateRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Candidate?> GetAsync(int? id)
        {
            return await _db.Candidates.FirstOrDefaultAsync(candidate => candidate.Id == id);
        }

        public async Task<ICollection<Candidate?>> GetAllAsync()
        {
            return await _db.Candidates.ToListAsync<Candidate>();
        }

        public async Task<int> AddOrUpdateAsync(Candidate? candidate)
        {
            _db.Candidates.Update(candidate);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {

                return -1;
            }
            return candidate.Id;
        }

        public async Task<int> DeleteAsync(int? id)
        {
            try
            {
                _db.Candidates.Remove(await _db.Candidates.FindAsync(id));
                return await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
