using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public class ExamStemService : ControllerBase, IExamStemService
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private MyDTO _myDTO;
        public ExamStemService(WebAppDbContext db, UnitOfWork unit)
        {
            _db = db;
            _unit = unit;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.ExamStems == null || await _unit.ExamStem.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                _myDTO.ExamStems = await _unit.ExamStem.GetAllAsync();
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.ExamStem = await _unit.ExamStem.GetAsync(id);
            }
            return _myDTO;
        } 
        
        public async Task<IEnumerable<ExamStem>?> GetAll()
        {
            return await _unit.ExamStem.GetAllAsync();
        }

        // Get all Exam Stems that have a specific Exam
        public async Task<IEnumerable<ExamStem>?> GetByExam(Exam exam)
        {  
            return await _unit.ExamStem.GetStemsByExam(exam);
        }

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.ExamStems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                _myDTO.ExamStems = await _unit.ExamStem.GetAllAsync();
                return _myDTO;
            }
            _myDTO.ExamStem = await _unit.ExamStem.GetAsync(id);
            if (_myDTO.ExamStem == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                _myDTO.ExamStems = await _unit.ExamStem.GetAllAsync();
            }
            return _myDTO;
        }

        public async Task<MyDTO> AddOrUpdate(int id,
                    [Bind("Id,Exam,Stem")] ExamStem examStem)
        {   
            EntityState state = _unit.ExamStem.AddOrUpdate(examStem);
            if (id != examStem.Id)
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The exam stem Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.ExamStem = examStem;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested exam stem has been added successfully.";
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested exam stem has been updated successfully.";
                }
                if (state == EntityState.Modified && !await _unit.ExamStem.Exists(examStem.Id))
                {
                    _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                }                
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                _myDTO.ExamStems = await _unit.ExamStem.GetStemsByExam(examStem.Exam);
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
                _myDTO.ExamStem = examStem;
            }
            return _myDTO;
        }

        //public async Task<MyDTO> SubmitExamStems(int examId, IEnumerable<ExamStem> examStems)
        //{
        //    foreach(var examStem in examStems)
        //    {
        //        if (examId != examStem.Exam.Id)
        //        {                    
        //            _myDTO.Message = "The exam Id was compromised. " +
        //                "The request could not be completed due to security reasons. " +
        //                "Please try submitting your answers again later.";
        //            _myDTO.ExamStems = examStems;
        //            return _myDTO;
        //        }
        //        _unit.ExamStem.AddOrUpdate(examStem);

        //    }            
        //    if (examId != examStem.Id)
        //    {
        //        if (state == EntityState.Added)
        //        {
        //            _myDTO.View = "Create";
        //        }
        //        if (state == EntityState.Modified)
        //        {
        //            _myDTO.View = "Edit";
        //        }
        //        _myDTO.Message = "The exam stem Id was compromised. The request could not be completed due to security reasons. Please try again later.";
        //        _myDTO.ExamStem = examStem;
        //        return _myDTO;
        //    }
        //}

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.ExamStems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                _myDTO.ExamStems = await _unit.ExamStem.GetAllAsync();
                return _myDTO;
            } 
            _myDTO.ExamStem = await _unit.ExamStem.GetAsync(id);
            if (_myDTO.ExamStem == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                _myDTO.ExamStems = await _unit.ExamStem.GetAllAsync();
            }
            return _myDTO;
        }

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested exam stem has been deleted successfully.";
            if (!await _unit.ExamStem.Exists(id))
            {
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                return _myDTO;
            }
            ExamStem examStem = await _unit.ExamStem.GetAsync(id);
            _unit.ExamStem.Delete(examStem);
            await _unit.SaveAsync(); 
            _myDTO.ExamStems = await _unit.ExamStem.GetAllAsync();
            return _myDTO;
        }
    }
}
