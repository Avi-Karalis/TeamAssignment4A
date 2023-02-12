using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Data.Repositories
{
    public class CandidateExamStemRepository 
    {
        private readonly WebAppDbContext _db;
        public CandidateExamStemRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<CandidateExamStem?> GetAsync(int id)
        {
            return await _db.CandidateExamStems.AsNoTracking().Include(ces => ces.CandidateExam)
                .Include(ces => ces.ExamStem).FirstOrDefaultAsync(x => x.Id == id);
        }

        public EntityState AddOrUpdate(CandidateExamStem cExStem)
        {
            _db.CandidateExamStems.Update(cExStem);
            return _db.Entry(cExStem).State;
        }

    }
}
