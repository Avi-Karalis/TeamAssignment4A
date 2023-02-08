using Microsoft.AspNetCore.Identity;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICandidateService 
    {
        Task<MyDTO> Get(int id);
        Task<IEnumerable<CandidateDto>?> GetAll();
        Task<IEnumerable<IdentityUser>?> GetUsers();
        Task<MyDTO> Add(int id, CandidateDto candidateDto);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> Update(int id, CandidateDto candidateDto);
        Task<MyDTO> GetForDelete(int id);
        Task<MyDTO> Delete(int id);
    }
}
