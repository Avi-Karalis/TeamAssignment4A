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
        public async Task<Candidate?> GetAsync(int id)
        {
            return await _db.Candidates.FirstOrDefaultAsync(candidate => candidate.Id == id);
        }

        public async Task<IEnumerable<Candidate?>> GetAllAsync()
        {
            return await _db.Candidates.ToListAsync<Candidate>();
        }

        public EntityState AddOrUpdate(Candidate candidate)
        {
            _db.Candidates.Update(candidate);
            try
            {
                _db.SaveChangesAsync();
            }
            catch (Exception)
            {

                return EntityState.Added;
            }
            return EntityState.Added;
        }

        public async void Delete(Candidate candidate)
        {
            try
            {
                _db.Candidates.Remove(await _db.Candidates.FindAsync(candidate.Id));
                 await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
               
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Candidates.AnyAsync(e => e.Id == id);
        }
    }
}
