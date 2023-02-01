using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Data.Repositories
{
    public class ExamStemRepository : IGenericRepository<ExamStem>
    {
        private readonly WebAppDbContext _db;
        public ExamStemRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<ExamStem?> GetAsync(int id)
        {
            return await _db.ExamStems.Include(exs => exs.Exam)
                .Include(exs => exs.Stem).FirstOrDefaultAsync(x => x.Id == id);
        }        

        public async Task<IEnumerable<ExamStem>?> GetAllAsync()
        {
            return await _db.ExamStems.Include(exs => exs.Exam)
                .Include(exs => exs.Stem).ToListAsync<ExamStem>();
        }

        public async Task<IEnumerable<ExamStem>?> GetStemsByExam(Exam exam)
        {
            return await _db.ExamStems.Include(exs => exs.Exam).Include(exs => exs.Stem)
                .Where(exs => exs.Exam == exam).ToListAsync();
        }
        public async Task<IEnumerable<int>?> GetStemIdsByExam(Exam exam)
        {
            return await _db.ExamStems.Where(exs => exs.Exam == exam).Select(exs => exs.Id).ToListAsync();
        }

        public EntityState AddOrUpdate(ExamStem examStem)
        {
            _db.ExamStems.Update(examStem);
            return _db.Entry(examStem).State;
        }

        public void Delete(ExamStem examStem)
        {
            _db.ExamStems.Remove(examStem);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.ExamStems.AnyAsync(e => e.Id == id);
        }           
    }
}
