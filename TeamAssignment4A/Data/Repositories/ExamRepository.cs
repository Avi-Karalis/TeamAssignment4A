using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class ExamRepository : IGenericRepository<Exam>
    {
        private readonly WebAppDbContext _db;
        public ExamRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Exam?> GetAsync(int id)
        {
            return await _db.Exams.Include(exam => exam.Candidate).Include(exam => exam.Certificate).FirstOrDefaultAsync(x => x.Id == id);
        }        

        public async Task<IEnumerable<Exam>?> GetAllAsync()
        {
            await _db.Exams.ToListAsync<Exam>();
            var exams = await _db.Exams.ToListAsync<Exam>();
            foreach (var exam in exams)
            {
                if (exam.Candidate != null)
                    _db.Entry(exam).Reference(e => e.Candidate).Load();
                if (exam.Certificate != null)
                    _db.Entry(exam).Reference(e => e.Certificate).Load();
            }
            return exams;
        }

        public EntityState AddOrUpdate(Exam exam)
        {
            _db.Exams.Update(exam);
            return _db.Entry(exam).State;
        }

        public void Delete(Exam exam)
        {
            _db.Exams.Remove(exam);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Exams.AnyAsync(e => e.Id == id);
        }
    }
}
