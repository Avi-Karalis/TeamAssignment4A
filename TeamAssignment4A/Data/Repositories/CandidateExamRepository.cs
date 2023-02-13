using Microsoft.AspNetCore.Identity;
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
                .Include(ce => ce.Exam.ExamStems)
                .Include(ce => ce.Exam.Certificate).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems).FirstOrDefaultAsync(ce => ce.Id == id);
        }

        // Get Candidate Exam by providing User and CandidateExam Id in order to fill
        // the CandidateExamStems correspoding to this Candidate Exam
        public async Task<CandidateExam?> GetCanExamStemsForInput(Candidate candidate, int id)
        {
            return await _db.CandidateExams.Include(ce => ce.Exam).Include(ce => ce.Exam.ExamStems)
                .Include(ce => ce.Exam.Certificate).Include(ce => ce.Candidate)
                .Include(ce => ce.CandidateExamStems)
                .FirstOrDefaultAsync(ce => ce.Candidate == candidate && ce.Exam.Id == id);
        }

        // Get all Examinations that a specific Candidate has not already sat for
        public async Task<IEnumerable<CandidateExam>?> GetBooked(int candidateId)
        {
            return await _db.CandidateExams
                .Where(ce => (ce.Candidate.Id == candidateId && ce.ExaminationDate != null) && ce.CandidateScore == null)
                .Include(ce => ce.Exam).Include(ce => ce.Candidate).Include(ce => ce.Exam.ExamStems)
                .Include(ce => ce.CandidateExamStems).ToListAsync<CandidateExam>();
        }

        // Get all Examinations that a specific Candidate has sat for
        // and are already marked
        public async Task<IEnumerable<CandidateExam>?> GetMarkedExams(Candidate candidate)
        {
            return await _db.CandidateExams
                .Where(ce => ce.Candidate == candidate && ce.CandidateScore != null)
                .Include(ce => ce.Exam).Include(ce => ce.Exam.Certificate).Include(ce => ce.Candidate)
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
