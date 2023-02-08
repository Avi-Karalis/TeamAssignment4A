using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    internal interface ICandidateExamService
    {
        Task<IEnumerable<CandidateExam>?> GetAll(int candidateId);
        Task<IEnumerable<CandidateExamStem>?> GetByExam(CandidateExam canExam);
        Task<MyDTO> SubmitAnswers(IEnumerable<CandidateExamStem> cExStems);
    }
}