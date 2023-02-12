using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Models;
using Microsoft.AspNetCore.Identity;

namespace TeamAssignment4A.Services
{
    public class CandidateExamService : ControllerBase//, ICandidateExamService
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

        // Get CandidateExam by providing User and ExamId
        public async Task<CandidateExam?> GetCanExamForInput(IdentityUser user, int id)
        {
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            CandidateExam? canEx = await _unit.CandidateExam.GetCanExamStemsForInput(candidate, id);
            return canEx;
        }
        public async Task<CandidateExam>? GetById(int id)
        {
            return await _unit.CandidateExam.GetAsync(id);
        }

        // Get all Candidate Exam Stems that belong to a specific Candidate Exam
        public async Task<List<CandidateExamStem>?> GetExamStems(int id)
        {
            CandidateExam? canExam = await _unit.CandidateExam.GetAsync(id);
            List<ExamStem>? exStems = await _unit.ExamStem.GetExamStemsByExam(canExam.Exam) as List<ExamStem>;
            List<CandidateExamStem> cExStems = new List<CandidateExamStem>()
            {
                new CandidateExamStem(),
                new CandidateExamStem(),
                new CandidateExamStem(),
                new CandidateExamStem()
            }; //new List<CandidateExamStem>(4);
            for(int i = 0; i < exStems.Count(); i++)
            {
                cExStems.ElementAt(i).ExamStem = exStems[i];
            }
            //canExam.CandidateExamStems = cExStems;
            return cExStems;
        }

        
        // Submit a Candidate's Exam
        public async Task<MyDTO> SubmitAnswers([Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam, List<CandidateExamStem> canExStems)
        {
            if (ModelState.IsValid)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "Your Exam has been submitted successfully.";
                //_unit.CandidateExam.AddOrUpdate(candidateExam);
                candidateExam.CandidateExamStems = new List<CandidateExamStem>()
                    {
                        new CandidateExamStem(),
                        new CandidateExamStem(),
                        new CandidateExamStem(),
                        new CandidateExamStem()
                    };
                _unit.CandidateExam.AddOrUpdate(candidateExam);
                for (int i =0; i < canExStems.Count(); i++)
                {
                    //_unit.CandidateExamStem.AddOrUpdate(canExStems[i]);
                    canExStems[i].CandidateExam = candidateExam;
                    canExStems[i].ExamStem.Stem = await _unit.Stem.GetAsync(canExStems[i].ExamStem.Stem.Id);
                    //canExStems[i].ExamStem.Stem.Topic = await _unit.Topic.GetAsync(canExStems[i].ExamStem.Stem.Topic.Id);
                    canExStems[i].ExamStem.Exam = await _unit.Exam.GetAsync(canExStems[i].ExamStem.Exam.Id);
                    //canExStems[i].ExamStem = await _unit.ExamStem.GetAsync(canExStems[i].ExamStem.Id);
                    //candidateExam.CandidateExamStems.ElementAt(i).CandidateExam = candidateExam;

                    //canExStems[i].CandidateExam = candidateExam;
                }
                    candidateExam.CandidateExamStems = canExStems;
                    await _unit.SaveAsync();
                
                if (candidateExam.CandidateExamStems == null)
                {
                    _myDTO.View = "SitForExam";
                    _myDTO.Message = "Your answers failed to submit. Please try again later.";
                    _myDTO.CandidateExam = candidateExam;
                    return _myDTO;
                }

                if (candidateExam.CandidateExamStems.Count() != 0)
                {
                    _myDTO.Message = "You tried to submit an already submitted exam. The operation failed.";
                    return _myDTO;
                }

                for (int i = 0; i < canExStems.Count(); i++)
                {
                    //_unit.CandidateExamStem.AddOrUpdate(canExStems[i]);
                    canExStems[i].CandidateExam = candidateExam;
                }

                if (!await _unit.CandidateExam.Exists(candidateExam.Id))
                {
                    _myDTO.Message = "You tried to submit a non existing exam.";
                    return _myDTO;
                }

                
                
                
            }
            return _myDTO;
        }
    }
}
