using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public interface IExamStemService //: IGenericService<ExamStem>
    {

        Task<MyDTO> Get(int id);
        Task<IEnumerable<ExamStem>?> GetByExam(Exam exam);        
        Task<MyDTO> GetForUpdate(int id);        
        Task<MyDTO> AddOrUpdate(int id, ExamStem examStem);
        //Task<MyDTO> GetForDelete(int id);
        //Task<MyDTO> Delete(int id);
        
    }
}
