using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class CandidateExamService : ControllerBase
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public CandidateExamService(WebAppDbContext db, UnitOfWork unit, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }

        // Get all Exams that a specific Candidate has not sat for yet
        public async Task<IEnumerable<CandidateExam>?> GetAll(int candidateId)
        {
            return await _unit.CandidateExam.GetAllAsync(candidateId);
        }

        // Get all Candidate Exam Stems that belong to a specific Candidate Exam
        public async Task<IEnumerable<CandidateExamStem>?> GetByExam(CandidateExam canExam)
        {
            IEnumerable<ExamStem> exStems = await _unit.ExamStem.GetExamStemsByExam(canExam.Exam);
            IEnumerable<CandidateExamStem> cExStems = _mapper.Map<List<CandidateExamStem>>(exStems);
            return cExStems;
        }

        public async Task<MyDTO> SubmitAnswers([Bind("Id,SubmittedAnswer," +
                "Score,Candidate,ExamStem,CandidateExam")] IEnumerable<CandidateExamStem> cExStems)
        {
            if(cExStems == null)
            {
                _myDTO.View = "sitforexam";
                _myDTO.Message = "Your answers failed to submit. Please try again later.";
                _myDTO.CandidateExamStems = cExStems;
                return _myDTO;
            }
            _myDTO.Message = "Your Exam has been submitted successfully.";
            _unit.CandidateExam.AddOrUpdate(cExStems.First().CandidateExam);
            foreach (var cExStem in cExStems)
            {
                _unit.CandidateExamStem.AddOrUpdate(cExStem);
                if (ModelState.IsValid)
                {   
                    await _unit.SaveAsync();
                    _myDTO.View = "Index";
                    IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
                    _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
                    return _myDTO;
                }
                else
                {
                    _myDTO.View = "CreateExamStems";
                    _myDTO.Message = "Invalid entries. Please try again later.";
                    _myDTO.ExamDto = examDto;
                }
            }
            if (await _unit.CandidateExam.AlreadySubmitted(cExStems.First().CandidateExam.Id))
            {
                _myDTO.Message = "You tried to submit an already submitted Exam. The operation failed.";
            }

            return _myDTO;
            
        }

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
