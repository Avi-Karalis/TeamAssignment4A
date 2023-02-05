using AutoMapper;
using Fare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public class ExamService : ControllerBase, IExamService
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public ExamService(WebAppDbContext db, UnitOfWork unit, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }

        // Find a specific exam in the database using its Id
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.Exams == null || await _unit.Exam.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                var exams = await _unit.Exam.GetAllAsync();
                _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
            }
            else
            {
                _myDTO.View = "Details";
                Exam exam = await _unit.Exam.GetAsync(id);
                _myDTO.ExamDto = _mapper.Map<ExamDto>(exam);
                _myDTO.ExamDto.ExamStemIds = await _unit.ExamStem.GetStemIdsByExam(exam) as List<int>;
            }
            return _myDTO;
        }

        // This method is used to find a specific exam in the database when you have already filled its
        // Certificate field and want to fill the ExamStem field next
        public async Task<MyDTO> GetByExam(ExamDto examDto)
        {
            if (examDto.Id == null || _db.Exams == null || await _unit.Exam.GetAsync(examDto.Id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "An unexpected error occured. Please try again later.";
                var exams = await _unit.Exam.GetAllAsync();
                _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
            }
            else
            {
                _myDTO.View = "CreateExamStems";
                Exam exam = await _unit.Exam.GetAsync(examDto.Id);
                _myDTO.ExamDto = _mapper.Map<ExamDto>(exam);
                _myDTO.ExamDto.StemIds = await _unit.Stem.GetStemIdsByCert(exam.Certificate) as List<int>;
            }
            return _myDTO;
        }

        // Get all exams that exist in the database
        public async Task<IEnumerable<ExamDto>?> GetAll()
        {
            var exams = await _unit.Exam.GetAllAsync();
            _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
            return _myDTO.ExamDtos;
        } 
        
        // Get all Stems for a specific Exam
        public async Task<IEnumerable<int>?> GetExamStemIds(ExamDto examDto)
        {
            Exam exam = _mapper.Map<Exam>(examDto);
            return await _unit.ExamStem.GetStemIdsByExam(exam);
        }

        // Get all possible Stems for a specific Certificate of selected Exam
        public async Task<IEnumerable<int>?> GetStemIds(ExamDto examDto)
        {
            Exam exam = _mapper.Map<Exam>(examDto);
            return await _unit.Stem.GetStemIdsByCert(exam.Certificate);
        }

        // Get all Certificates (in order to use it for the drop-down list
        public async Task<IEnumerable<Certificate>?> GetCerts()
        {
            return await _unit.Certificate.GetAllAsync();
        }

        // Find the exam you want to update in the database
        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.Exams == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                return _myDTO;
            }
            Exam exam = await _unit.Exam.GetAsync(id);
            _myDTO.ExamDto = _mapper.Map<ExamDto>(exam);
            _myDTO.ExamDto.ExamStems = await _unit.ExamStem.GetStemsByExam(exam) as List<ExamStem>;
            if (_myDTO.ExamDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
            }
            return _myDTO;
        }

        // Fill the Certificate field for the selected exam
        public async Task<MyDTO> AddCert(int id,
            [Bind("Id,TitleOfCertificate,Certificate,StemIds,Stems,ExamStemIds,ExamStems")] ExamDto examDto)
        {
            examDto.Certificate = await _unit.Certificate.GetAsyncByTilteOfCert(examDto.TitleOfCertificate);
            Exam exam = _mapper.Map<Exam>(examDto);
            _unit.Exam.AddOrUpdate(exam);
            
            if (id != exam.Id)
            {                
                _myDTO.View = "Create";
                _myDTO.Message = "The exam Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.ExamDto = examDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested Title of Certificate has been added successfully.";
                await _unit.SaveAsync();
                _myDTO.View = "CreateExamStems"; 
                _myDTO.ExamDto = _mapper.Map<ExamDto>(exam);
                return _myDTO;
            }
            else
            {                
                _myDTO.View = "Create";                
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.ExamDto = examDto;
            }
            return _myDTO;
        }

        // Fill the ExamStem field for the selected exam 
        public async Task<MyDTO> AddStems(int id, 
            [Bind("Id,TitleOfCertificate,Certificate,StemIds,Stems,ExamStemIds,ExamStems")] ExamDto examDto)
        {    
            Exam exam = await _unit.Exam.GetAsync(examDto.Id);
                                    
            foreach (var stemId in examDto.StemIds)
            {
                Stem stem = await _unit.Stem.GetAsync(stemId);
                ExamStem examStem = new ExamStem(exam, stem);
                _unit.ExamStem.AddOrUpdate(examStem);
            }
            _unit.Exam.AddOrUpdate(exam);

            if (id != exam.Id)
            {                
                _myDTO.View = "CreateExamStems";                
                _myDTO.Message = "The exam Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.ExamDto = examDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {                 
                _myDTO.Message = "The requested exam has been added successfully."; 
                if (!await _unit.Exam.Exists(exam.Id))
                {
                    _myDTO.Message = "The requested exam could not be found. Please try again later.";
                }                
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
            return _myDTO;
        }

        // Update the ExamStems of the selected exam
        public async Task<MyDTO> Update(int id,
            [Bind("Id,TitleOfCertificate,Certificate,StemIds,Stems,ExamStemIds,ExamStems")] ExamDto examDto)
        {             
            Exam exam = await _unit.Exam.GetAsync(examDto.Id);
            List<int> stemIds = examDto.StemIds;
            examDto = _mapper.Map<ExamDto>(exam);
            
            for(int i = 0; i < examDto.ExamStems.Count(); i++)
            { 
                Stem stem = await _unit.Stem.GetAsync(stemIds[i]);
                examDto.ExamStems[i].Stem = stem; 
                exam = _mapper.Map<Exam>(examDto);
                _unit.ExamStem.AddOrUpdate(exam.ExamStems.FirstOrDefault(x => x == examDto.ExamStems[i]));                
            }       

            if (id != exam.Id)
            {                
                _myDTO.View = "Edit";
                _myDTO.Message = "The exam Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.ExamDto = examDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested exam has been updated successfully.";                
                if (!await _unit.Exam.Exists(exam.Id))
                {
                    _myDTO.Message = "The requested exam could not be found. Please try again later.";
                }                
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
                _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
                return _myDTO;
            }
            else
            {                
                _myDTO.View = "Edit";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.ExamDto = examDto;
            }
            return _myDTO;
        }

        // Find the exam you want to delete in the database
        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.Exams == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
                _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
                return _myDTO;
            }
            Exam exam = await _unit.Exam.GetAsync(id);
            _myDTO.ExamDto = _mapper.Map<ExamDto>(exam);
            if (_myDTO.ExamDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
                _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
            }
            return _myDTO;
        }

        // Delete the selected exam
        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested exam has been deleted successfully.";
            if (!await _unit.Exam.Exists(id))
            {
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                return _myDTO;
            }
            Exam exam = await _unit.Exam.GetAsync(id);
            _unit.Exam.Delete(exam);
            await _unit.SaveAsync();
            IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
            _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
            return _myDTO;
        }
    }
}
