using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

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
                _myDTO.ExamDto.ExamStemIds = await _unit.ExamStem.GetStemIdsByExam(exam);
            }
            return _myDTO;
        }
        public async Task<IEnumerable<ExamDto>?> GetAll()
        {
            var exams = await _unit.Exam.GetAllAsync();
            _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
            return _myDTO.ExamDtos;
        } 
        
        public async Task<IEnumerable<int>?> GetExamStemIds(ExamDto examDto)
        {
            Exam exam = _mapper.Map<Exam>(examDto);
            return await _unit.ExamStem.GetStemIdsByExam(exam);
        }

        public async Task<IEnumerable<Stem>?> GetStemIds(ExamDto examDto)
        {
            Exam exam = _mapper.Map<Exam>(examDto);
            return await _unit.Stem.GetByCert(exam.Certificate);
        }

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
            if (_myDTO.ExamDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> AddCert(int id,
            [Bind("Id,TitleOfCertificate,Certificate,ExamStemIds,ExamStems")] ExamDto examDto)
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
                _myDTO.ExamDto = examDto;
                //IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
                //_myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
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

        public async Task<MyDTO> AddOrUpdate(int id, 
            [Bind("Id,TitleOfCertificate,Certificate,ExamStemIds,ExamStems")] ExamDto examDto)
        {            
            //Certificate certificate = await _unit.Certificate.GetAsyncByTilteOfCert(examDto.TitleOfCertificate);            
            //examDto.Certificate = await _unit.Certificate.GetAsyncByTilteOfCert(examDto.TitleOfCertificate);
            
            
            foreach(var examStem in examDto.ExamStems)
            {

            }
            Exam exam = _mapper.Map<Exam>(examDto);
            exam.ExamStems = await _unit.ExamStem.GetStemsByExam(exam);

            EntityState state = _unit.Exam.AddOrUpdate(exam);
            if (id != exam.Id)
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The exam Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.ExamDto = examDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {                 
                _myDTO.Message = "The requested exam has been added successfully.";                
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested exam has been updated successfully.";
                }
                if (state == EntityState.Modified && !await _unit.Exam.Exists(exam.Id))
                {
                    _myDTO.Message = "The requested exam could not be found. Please try again later.";
                }
                //if (await _unit.Exam.CodeExists(exam.Id, exam.AssessmentTestCode))
                //{
                //    if (state == EntityState.Added)
                //    {
                //        _myDTO.View = "Create";
                //    }
                //    if (state == EntityState.Modified)
                //    {
                //        _myDTO.View = "Edit";
                //    }
                //    _myDTO.Message = "This exam assessment test code already exists. Please try providing a different code.";
                //    _myDTO.ExamDto = examDto;
                //    return _myDTO;
                //}
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                IEnumerable<Exam> exams = await _unit.Exam.GetAllAsync();
                _myDTO.ExamDtos = _mapper.Map<List<ExamDto>>(exams);
                return _myDTO;
            }
            else
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.ExamDto = examDto;
            }
            return _myDTO;
        }

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
