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
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        private readonly UserManager<IdentityUser> _userManager;
        public CandidateExamService(WebAppDbContext db, UnitOfWork unit,
            IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
            _userManager = userManager;
        }

        // Get all Exams that a specific Candidate has not sat for yet
        public async Task<IEnumerable<CandidateExam>?> GetAll()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            return await _unit.CandidateExam.GetAllAsync(candidate.Id);
        }

        // Get all Candidate Exam Stems that belong to a specific Candidate Exam
        public async Task<IEnumerable<CandidateExamStem>?> GetByExam(CandidateExam canExam)
        {
            IEnumerable<ExamStem> exStems = await _unit.ExamStem.GetExamStemsByExam(canExam.Exam);
            IEnumerable<CandidateExamStem> cExStems = _mapper.Map<List<CandidateExamStem>>(exStems);
            return cExStems;
        }

        
        // Submit a Candidate's Exam
        public async Task<MyDTO> SubmitAnswers([Bind("Id,SubmittedAnswer," +
                "Score,Candidate,ExamStem,CandidateExam")] IEnumerable<CandidateExamStem> cExStems)
        {
            if(cExStems == null)
            {
                _myDTO.View = "SitForExam";
                _myDTO.Message = "Your answers failed to submit. Please try again later.";
                _myDTO.CandidateExamStems = cExStems;
                return _myDTO;
            }

            _myDTO.View = "Index";
            _myDTO.Message = "Your Exam has been submitted successfully.";
            
            foreach (var cExStem in cExStems)
            {
                if (!ModelState.IsValid)
                {
                    _myDTO.View = "SitForExam";
                    _myDTO.Message = "A question was left unanswered. Please check your exam" +
                        " for unfilled answers.";
                    _myDTO.CandidateExamStems = cExStems;
                    return _myDTO;
                }
                else
                {
                    _unit.CandidateExamStem.AddOrUpdate(cExStem);
                }
            }
            if (await _unit.CandidateExam.AlreadySubmitted(cExStems.First().CandidateExam.Id))
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The Exam you tried to submit is already submitted. The operation failed.";
                return _myDTO;
            }

            _unit.CandidateExam.AddOrUpdate(cExStems.First().CandidateExam);
            await _unit.SaveAsync();
            return _myDTO;            
        }




        // We have to evaluate which of the following methods are needed -----------------------
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.CandidateExams == null || await _unit.CandidateExam.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync(id);
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);
            }
            return _myDTO;
        }


        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.CandidateExams == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);
            if (_myDTO.CandidateExam == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync(id);
            }
            return _myDTO;
        }


        public async Task<MyDTO> AddCandidateExam(int id, [Bind("Id,AssessmentTestCode,ExaminationDate," +
            "ScoreReportDate,CandidateScore,PercentageScore,AssessmentResultLabel,Candidate,Exam," +
            "CandidateExamStem")] CandidateExam candidateExam)
        {
            if (id != candidateExam.Id)
            {
                // I have to change this!!! --------------------------
                _myDTO.View = "Create";
                _myDTO.Message = "The Candidate Exam Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.CandidateExam = candidateExam;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested Candidate Exam has been added successfully.";
                if (!await _unit.CandidateExam.Exists(candidateExam.Id))
                {
                    _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                }
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
                return _myDTO;
            }
            else
            {
                // I have to change this!!! --------------------------
                _myDTO.View = "Create";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.CandidateExam = candidateExam;
            }
            return _myDTO;
        }

        public async Task<MyDTO> Update(int id, [Bind("Id,AssessmentTestCode,ExaminationDate," +
            "ScoreReportDate,CandidateScore,PercentageScore,AssessmentResultLabel,Candidate,Exam," +
            "CandidateExamStem")] CandidateExam candidateExam)
        {
            if (id != candidateExam.Id)
            {
                _myDTO.View = "Edit";
                _myDTO.Message = "The Candidate Exam Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.CandidateExam = candidateExam;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested Candidate Exam has been updated successfully.";
                if (!await _unit.CandidateExam.Exists(candidateExam.Id))
                {
                    _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                }
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
                return _myDTO;
            }
            else
            {
                _myDTO.View = "Edit";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.CandidateExam = candidateExam;
            }
            return _myDTO;
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.CandidateExams == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync(id);
                return _myDTO;
            }
            _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);
            if (_myDTO.CandidateExam == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync(id);
            }
            return _myDTO;
        }

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested exam has been deleted successfully.";
            if (!await _unit.CandidateExam.Exists(id))
            {
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                return _myDTO;
            }
            CandidateExam candidateExam = await _unit.CandidateExam.GetAsync(id);
            _unit.CandidateExam.Delete(candidateExam);
            await _unit.SaveAsync();
            _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync(id);
            return _myDTO;
        }
    }
}
