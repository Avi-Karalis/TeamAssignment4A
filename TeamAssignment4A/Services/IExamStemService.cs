using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public interface IExamStemService : IGenericService<ExamStem>
    {
        new Task<IEnumerable<ExamStem>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, ExamStem examStem);
        Task<IEnumerable<ExamStem>?> GetByExamId(int id);
    }
}
