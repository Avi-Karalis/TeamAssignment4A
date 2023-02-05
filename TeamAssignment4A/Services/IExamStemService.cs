using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public interface IExamStemService : IGenericService<ExamStem>
    {
        new Task<IEnumerable<ExamStem>?> GetAll();        
        Task<IEnumerable<ExamStem>?> GetByExam(Exam exam);        
        new Task<MyDTO> AddOrUpdate(int id, ExamStem examStem);
    }
}
