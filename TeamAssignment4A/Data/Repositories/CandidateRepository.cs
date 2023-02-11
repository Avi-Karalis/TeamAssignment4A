using Microsoft.AspNetCore.Identity;
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
            return await _db.Candidates.AsNoTracking().Include(cand => cand.CandidateExams).Include(cand => cand.IdentityUser)
                .Include(cand => cand.CandidateExamStems).FirstOrDefaultAsync(x => x.Id == id);
        }

        // Get Candidate without including the CandidateExam entity
        // This is used when creating a Candidadate's Exam
        public async Task<Candidate?> GetCandForExam(int id)
        {
            return await _db.Candidates.Include(cand => cand.IdentityUser)
                .Include(cand => cand.CandidateExamStems).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Candidate?> GetByUser(IdentityUser user)
        {
            return await _db.Candidates.AsNoTracking().Include(cand => cand.CandidateExams).Include(cand => cand.IdentityUser)
                .Include(cand => cand.CandidateExamStems).FirstOrDefaultAsync(x => x.IdentityUser == user);
        }

        public async Task<IEnumerable<Candidate>?> GetAllAsync()
        {
            return await _db.Candidates.AsNoTracking().Include(cand => cand.CandidateExams).Include(cand => cand.IdentityUser)
                .Include(cand => cand.CandidateExamStems).ToListAsync<Candidate>();
        }

        public EntityState AddOrUpdate(Candidate candidate)
        {
            _db.Candidates.Update(candidate);
            return _db.Entry(candidate).State;
        }

        public void Delete(Candidate candidate)
        {
            _db.Candidates.Remove(candidate);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Candidates.AnyAsync(e => e.Id == id);
        }

    }
}
