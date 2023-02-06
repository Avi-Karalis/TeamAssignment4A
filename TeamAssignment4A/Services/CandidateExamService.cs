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
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.CandidateExams == null || await _unit.CandidateExam.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later."; 
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);                
            }
            return _myDTO;
        }



        public async Task<IEnumerable<CandidateExam>?> GetAll()
        { 
            _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
            return _myDTO.CandidateExams;
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
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
            }
            return _myDTO;
        }


        public async Task<MyDTO> AddCandidateExam(int id, [Bind("Id,AssessmentTestCode,ExaminationDate," +
            "ScoreReportDate,CandidateScore,PercentageScore,AssessmentResultLabel,Candidate,Exam," +
            "CandidateExamStem")] CandidateExam candidateExam)
        {
            //examDto.Certificate = await _unit.Certificate.GetAsyncByTilteOfCert(examDto.TitleOfCertificate);
            //Exam exam = _mapper.Map<Exam>(examDto);
            //_unit.Exam.AddOrUpdate(exam);

            //foreach (var stemId in examDto.StemIds)
            //{
            //    Stem stem = await _unit.Stem.GetAsync(stemId);
            //    ExamStem examStem = new ExamStem(exam, stem);
            //    _unit.ExamStem.AddOrUpdate(examStem);
            //    await _unit.SaveAsync();
            //}            
            //exam.ExamStems = await _unit.ExamStem.GetStemsByExam(exam);

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
            //examDto.Certificate = await _unit.Certificate.GetAsyncByTilteOfCert(examDto.TitleOfCertificate);
            //Exam exam = await _unit.Exam.GetByCert(examDto.Certificate);
            //exam.ExamStems = await _unit.ExamStem.GetStemsByExam(exam);

            //List<int> stemIds = examDto.StemIds;
            //examDto = _mapper.Map<ExamDto>(exam);
            //for (int i = 0; i < examDto.ExamStems.Count(); i++)
            //{
            //    Stem stem = await _unit.Stem.GetAsync(stemIds[i]);
            //    examDto.ExamStems[i].Stem = stem;
            //    exam = _mapper.Map<Exam>(examDto);
            //    _unit.ExamStem.AddOrUpdate(exam.ExamStems.FirstOrDefault(x => x == examDto.ExamStems[i]));
            //}
            //await _unit.SaveAsync();

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
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
                return _myDTO;
            }
            _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);
            if (_myDTO.CandidateExam == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Candidate Exam could not be found. Please try again later.";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
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
            _myDTO.CandidateExams = await _unit.CandidateExam.GetAllAsync();
            return _myDTO;
        }
    }
}
