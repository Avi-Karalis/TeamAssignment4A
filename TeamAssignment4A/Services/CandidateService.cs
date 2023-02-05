using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class CandidateService : ControllerBase, ICandidateService
    {
        private UnitOfWork _unit;
        private WebAppDbContext _db;
        private MyDTO _myDTO;
        public CandidateService(UnitOfWork unit, WebAppDbContext db)
        {
            _unit = unit;
            _db = db;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.Candidates == null || await _unit.Candidate.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                _myDTO.Candidates = await _unit.Candidate.GetAllAsync();
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.Candidate = await _unit.Candidate.GetAsync(id);
            }
            return _myDTO;
        }

        public async Task<IEnumerable<Candidate>?> GetAll()
        {
            return await _unit.Candidate.GetAllAsync();
        }

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.Candidates == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Candidate = await _unit.Candidate.GetAsync(id);
            if (_myDTO.Candidate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> AddOrUpdate(int id, [Bind("Id,FirstName,MiddleName,LastName,Gender,NativeLanguage," +
            "CountryOfResidence,Birthdate,Email,LandlineNumber,MobileNumber,Address1,Address2,PostalCode,Town," +
            "Province,PhotoIdType,PhotoIdNumber,PhotoIdDate")] Candidate candidate)
        {
            EntityState state = _unit.Candidate.AddOrUpdate(candidate);
            if (id != candidate.Id)
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The candidate Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.Candidate = candidate;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested candidate has been added successfully.";
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested candidate has been updated successfully.";
                }
                if (state == EntityState.Modified && !await _unit.Candidate.Exists(candidate.Id))
                {
                    _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                }
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                _myDTO.Candidates = await _unit.Candidate.GetAllAsync();
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
                _myDTO.Candidate = candidate;
            }
            return _myDTO;
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.Candidates == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                _myDTO.Candidates = await _unit.Candidate.GetAllAsync();
                return _myDTO;
            }
            _myDTO.Candidate = await _unit.Candidate.GetAsync(id);
            if (_myDTO.Candidate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                _myDTO.Candidates = await _unit.Candidate.GetAllAsync();
            }
            return _myDTO;
        }

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested candidate has been deleted successfully.";
            if (!await _unit.Candidate.Exists(id))
            {
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Candidate = await _unit.Candidate.GetAsync(id);
            _unit.Candidate.Delete(_myDTO.Candidate);
            await _unit.SaveAsync();
            _myDTO.Candidates = await _unit.Candidate.GetAllAsync();
            return _myDTO;
        }
    }
}
