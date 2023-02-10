using Microsoft.AspNetCore.Identity;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    internal interface ICandidateExamService
    {
        Task<IEnumerable<CandidateExam>?> GetAll(IdentityUser user );
        Task<CandidateExam?> GetByExam(CandidateExam canExam);
        Task<MyDTO> SubmitAnswers(int id, CandidateExam candidateExam);
    }
}