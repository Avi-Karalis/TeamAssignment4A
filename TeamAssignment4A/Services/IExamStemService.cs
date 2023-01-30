using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface IExamStemService : IGenericService<ExamStemDto>
    {
        new Task<IEnumerable<ExamStemDto>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, ExamStemDto examStemDto);
    }
}
