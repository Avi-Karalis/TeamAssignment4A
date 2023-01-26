using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICandidateService : IGenericService<Candidate>
    {
        new Task<IEnumerable<Candidate>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, Candidate candidate);
    }
}
