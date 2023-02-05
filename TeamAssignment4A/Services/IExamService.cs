using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;


namespace TeamAssignment4A.Services
{
    public interface IExamService 
    {
        Task<MyDTO> Get(int id);
        Task<IEnumerable<ExamDto>?> GetAll();
        Task<MyDTO> AddCert(int id, ExamDto examDto);
        Task<MyDTO> AddStems(int id, ExamDto examDto);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> Update(int id, ExamDto examDto);
        Task<MyDTO> GetForDelete(int id);
        Task<MyDTO> Delete(int id);                
    }
}
