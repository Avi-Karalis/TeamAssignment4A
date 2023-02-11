using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Models;
using Microsoft.AspNetCore.Identity;

namespace TeamAssignment4A.Services
{
    public class CandidateExamService : ControllerBase, ICandidateExamService
    {
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public CandidateExamService(UnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }

        // Get all Exams that a specific Candidate has not sat for yet
        public async Task<IEnumerable<CandidateExam>?> GetAll(IdentityUser user)
        {
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            return await _unit.CandidateExam.GetBooked(candidate.Id);
        }

        public async Task<CandidateExam>? GetById(int id)
        {
            return await _unit.CandidateExam.GetAsync(id);
        }

        // Get all Candidate Exam Stems that belong to a specific Candidate Exam
        public async Task<CandidateExam?> GetByExam(CandidateExam canExam)
        {
            IEnumerable<ExamStem> exStems = await _unit.ExamStem.GetExamStemsByExam(canExam.Exam);
            IEnumerable<CandidateExamStem> cExStems = _mapper.Map<List<CandidateExamStem>>(exStems);
            canExam.CandidateExamStems = cExStems;
            return canExam;
        }

        
        // Submit a Candidate's Exam
        public async Task<MyDTO> SubmitAnswers(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam)
        {
            if (id != candidateExam.Id)
            {
                _myDTO.View = "SitForExam";
                _myDTO.Message = "The exam Id was compromised. The request could\nnot " +
                    "be completed due to security reasons.\nPlease try again later.";
                _myDTO.CandidateExam = candidateExam;
                return _myDTO;
            }

            if (ModelState.IsValid)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "Your Exam has been submitted successfully.";
                if (candidateExam.CandidateExamStems == null)
                {
                    _myDTO.View = "SitForExam";
                    _myDTO.Message = "Your answers failed to submit. Please try again later.";
                    _myDTO.CandidateExam = candidateExam;
                    return _myDTO;
                }
                if (await _unit.CandidateExam.AlreadySubmitted(candidateExam.Id))
                {
                    _myDTO.Message = "You tried to submit an already submitted exam. The operation failed.";
                    return _myDTO;
                }
                if (!await _unit.CandidateExam.Exists(candidateExam.Id))
                {
                    _myDTO.Message = "You tried to submit a non existing exam.";
                    return _myDTO;
                }
                _unit.CandidateExam.AddOrUpdate(candidateExam);
                await _unit.SaveAsync();
                
            }
            return _myDTO;
        }
    }
}
