using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;


namespace TeamAssignment4A.Services
{
    public interface IExamService : IGenericService<ExamDto>
    {
        new Task<IEnumerable<ExamDto>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, ExamDto examDto);
    }
}
