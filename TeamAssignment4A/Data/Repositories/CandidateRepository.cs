using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class CandidateRepository : IGenericRepository<Candidate>
    {
        private WebAppDbContext _db;
        public CandidateRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Candidate> Get(int? id)
        {
            return await _db.Candidates.FindAsync(id);
        }

        public async Task<ICollection<Candidate>> GetAll()
        {
            return await _db.Candidates.ToListAsync<Candidate>();
        }

        public async Task<Candidate> Add(Candidate? candidate)
        {            
            await _db.Candidates.AddAsync(candidate);
            await _db.SaveChangesAsync();
            return candidate;
        }

        public async Task<Candidate> Update(Candidate? candidate)
        {
            if(await _db.Candidates.FindAsync(candidate.Id) != null)
            {
                _db.Candidates.Update(candidate);
                //_db.Entry(candidate).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return candidate;
            }
            return null;
        }

        public async Task<int> Delete(int? id)
        {
            int result = 0;
            if (await _db.Candidates.FindAsync(id) != null)
            {
                _db.Candidates.Remove(await _db.Candidates.FindAsync(id));
                await _db.SaveChangesAsync();
                result = 1;
            }
            return result;
        }
    }
}
