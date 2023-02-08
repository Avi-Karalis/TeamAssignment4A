using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICandidateService : IGenericService<CandidateDto>
    {
        new Task<IEnumerable<CandidateDto>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, CandidateDto candidateDto);
    }
}
