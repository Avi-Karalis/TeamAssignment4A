using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Data.Repositories
{
    public class CandidateExamRepository
    {
        private readonly WebAppDbContext _db;
        public CandidateExamRepository(WebAppDbContext context)
        {
            _db = context;
        }

        // Get Examination by Id
        public async Task<CandidateExam?> GetAsync(int id)
        {
            return await _db.CandidateExams.Include(ce => ce.Exam)
                .Include(ce => ce.Candidate).Include(ce => ce.CandidateExamStems)
                .FirstOrDefaultAsync(ce => ce.Id == id);
        }

        // Get all Examinations that a specific Candidate has not already sat for
        public async Task<IEnumerable<CandidateExam>?> GetBooked(int candidateId)
        {
            return await _db.CandidateExams
                .Where(ce => (ce.Candidate.Id == candidateId && ce.ExaminationDate != null) && ce.CandidateScore == null)
                .Include(ce => ce.Exam).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems).ToListAsync<CandidateExam>();
        }

        // Get all Examinations that a specific Candidate has sat for
        // and are already marked
        public async Task<IEnumerable<CandidateExam>?> GetMarkedExams(Candidate candidate)
        {
            return await _db.CandidateExams
                .Where(ce => ce.Candidate == candidate && ce.CandidateScore != null)
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

        public async Task<bool> Exists(int id)
        {
            return await _db.CandidateExams.AnyAsync(e => e.Id == id);
        }

        // When a candidate sat for an examination and during the submit phase
        // the database already had data entries for their answers
        public async Task<bool> AlreadySubmitted(int id)
        {
            return await _db.CandidateExams
                .AnyAsync(ce => ce.Id == id && ce.CandidateExamStems != null);
        }

        // This is to show all the unmarked exams, so that the admin
        // knows which still need to be assigned to a Marker
        public async Task<IEnumerable<CandidateExam>?> GetUnmarkedExams()
        {
            return await _db.CandidateExams
                .Where(ce => ce.CandidateScore != null)
                .Include(ce => ce.Exam).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems).ToListAsync<CandidateExam>();
        }
    }
}
