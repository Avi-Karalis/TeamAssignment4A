using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TeamAssignment4A.Services
{
    public class CandidateExamService : ControllerBase//, ICandidateExamService
    {
        private UnitOfWork _unit;
        private MyDTO _myDTO;
        private WebAppDbContext _db;
        public CandidateExamService(WebAppDbContext db, UnitOfWork unit)
        {
            _unit = unit;
            _myDTO = new MyDTO();
            _db = db;
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
            }; 
            for(int i = 0; i < 4; i++)
            {
                cExStems.ElementAt(i).ExamStem = exStems[i];
            }
            
            return cExStems;
        }

        public async Task<MyDTO> InsertCanExStems([Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam, List<CandidateExamStem> canExStems)
        {
            //_unit.CandidateExam.AddOrUpdate(candidateExam);
            bool answers = true;
            foreach(var ces in canExStems)
            {
                if(ces.SubmittedAnswer == null)
                {
                    answers= false;
                }
            }

            if(!answers)
            {
                _myDTO.View = "SitForExam";
                _myDTO.Message = "One or more questions were left unanswered." +
                    "\nPlease try again later.";
                _myDTO.CandidateExamStems = canExStems;
                return _myDTO;
            }
            if (candidateExam.CandidateExamStems.Count() != 0)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "You tried to submit an already submitted exam." +
                    "\nThe operation failed.";
                return _myDTO;
            }

            if (ModelState.IsValid)
            {
                
                var exam = await _unit.Exam.GetForSitExam(canExStems[0].ExamStem.Exam.Id);
                List<ExamStem> examStems = new List<ExamStem>()
                {
                    new  ExamStem { Exam = exam },
                    new  ExamStem { Exam = exam },
                    new  ExamStem { Exam = exam },
                    new  ExamStem { Exam = exam }
                };
                for (int i = 0; i < canExStems.Count(); i++)
                {
                    
                    var stem = await _unit.Stem.GetAsync(canExStems[i].ExamStem.Stem.Id);

                    examStems[i].Stem = stem;
                   
                    
                }
                var cExStems = new List<CandidateExamStem>()
                {
                    new CandidateExamStem { ExamStem = examStems[0], CandidateExam = candidateExam, SubmittedAnswer = canExStems[0].SubmittedAnswer },
                    new CandidateExamStem { ExamStem = examStems[1], CandidateExam = candidateExam, SubmittedAnswer = canExStems[1].SubmittedAnswer },
                    new CandidateExamStem { ExamStem = examStems[2], CandidateExam = candidateExam, SubmittedAnswer = canExStems[2].SubmittedAnswer },
                    new CandidateExamStem { ExamStem = examStems[3], CandidateExam = candidateExam, SubmittedAnswer = canExStems[3].SubmittedAnswer }
                };
                
                foreach(var canExStem in cExStems)
                {
                    
                    _db.Entry(canExStem.ExamStem.Exam).State = EntityState.Unchanged;
                    _unit.CandidateExamStem.AddOrUpdate(canExStem);
                    await _unit.SaveAsync();
                }
            }
            _myDTO.CandidateExam = candidateExam;
            return _myDTO;
        }

        // Submit a Candidate's Exam
        public async Task<MyDTO> SubmitAnswers([Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam, List<CandidateExamStem> canExStems)
        {
            _myDTO = await InsertCanExStems(candidateExam, canExStems);

            if (ModelState.IsValid)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "Your Exam has been submitted successfully.";
                
                if (!await _unit.CandidateExam.Exists(candidateExam.Id))
                {
                    _myDTO.Message = "You tried to submit a non existing exam.";
                    return _myDTO;
                }
                await _unit.SaveAsync();
                
                
                
            }
            return _myDTO;
        }
    }
}
