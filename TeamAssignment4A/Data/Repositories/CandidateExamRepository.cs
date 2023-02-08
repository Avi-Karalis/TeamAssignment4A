using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Data.Repositories
{
    public class CandidateExamRepository : IGenericRepository<CandidateExam>
    {
        private readonly WebAppDbContext _db;
        public CandidateExamRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<CandidateExam?> GetAsync(int id)
        {
            return await _db.CandidateExams.Include(ce => ce.Exam).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems).FirstOrDefaultAsync(x => x.Id == id);
        }        

        public async Task<IEnumerable<CandidateExam>?> GetAllAsync()
        {
            return await _db.CandidateExams
                .Include(ce => ce.Exam).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems).ToListAsync<CandidateExam>();
        }
        
        public async Task<IEnumerable<CandidateExam>?> GetAllAsync(int candidateId)
        {
            return await _db.CandidateExams
                .Where(ce => ce.Candidate.Id == candidateId && ce.CandidateScore == null)
                .Include(ce => ce.Exam).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems).ToListAsync<CandidateExam>();
        }

        public EntityState AddOrUpdate(CandidateExam candidateExam)
        {
            _db.CandidateExams.Update(candidateExam);
            return _db.Entry(candidateExam).State;
        }

        public void Delete(CandidateExam candidateExam)
        {
            _db.CandidateExams.Remove(candidateExam);
        }

        public async Task<bool> AlreadySubmitted(int id)
        {
            return await _db.CandidateExams
                .AnyAsync(ce => ce.Id == id && ce.CandidateExamStems != null);
        }

        public async Task<bool> AlreadyMarked(int id)
        {
            return await _db.CandidateExams
                .AnyAsync(ce => ce.Id == id && ce.CandidateScore != null);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.CandidateExams.AnyAsync(ce => ce.Id == id);
        }
    }
}
