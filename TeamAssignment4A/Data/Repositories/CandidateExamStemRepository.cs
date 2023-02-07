using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Data.Repositories
{
    public class CandidateExamStemRepository : IGenericRepository<CandidateExamStem>
    {
        private readonly WebAppDbContext _db;
        public CandidateExamStemRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<CandidateExamStem?> GetAsync(int id)
        {
            return await _db.CandidateExamStems.Include(ces => ces.CandidateExam)
                .Include(ces => ces.ExamStem).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CandidateExamStem>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public EntityState AddOrUpdate(CandidateExamStem cExStem)
        {
            throw new NotImplementedException();
        }

        public void Delete(CandidateExamStem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
