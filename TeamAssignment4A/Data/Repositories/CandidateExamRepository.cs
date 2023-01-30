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
            return await _db.CandidateExams.Include(ce => ce.Exam)
                .Include(ce => ce.Candidate).FirstOrDefaultAsync(x => x.Id == id);
        }        

        public async Task<IEnumerable<CandidateExam>?> GetAllAsync()
        {
            return await _db.CandidateExams.Include(ce => ce.Exam)
                .Include(ce => ce.Candidate).ToListAsync<CandidateExam>();
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

        public async Task<bool> Exists(int id)
        {
            return await _db.CandidateExams.AnyAsync(e => e.Id == id);
        }            
    }
}
