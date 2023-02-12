using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class CandidateService : ControllerBase, ICandidateService
    {
        private UnitOfWork _unit;
        private WebAppDbContext _db;
        private MyDTO _myDTO;
        private readonly IMapper _mapper;
        public CandidateService(UnitOfWork unit, WebAppDbContext db, IMapper mapper)
        {
            _unit = unit;
            _db = db;
            _myDTO = new MyDTO();
            _mapper = mapper;
        }
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.Candidates == null || await _unit.Candidate.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                _myDTO.Candidates = _mapper.Map<List<CandidateDto>>(await _unit.Candidate.GetAllAsync());
            }
            else
            {
                _myDTO.View = "Details";
                Candidate candidate = await _unit.Candidate.GetAsync(id);
                _myDTO.Candidate = _mapper.Map<CandidateDto>(candidate);
                _myDTO.Candidate.UserEmail = candidate.IdentityUser.Email;
            }
            return _myDTO;
        }

        // Get all Candidates and map them to the Dto
        public async Task<IEnumerable<CandidateDto>?> GetAll()
        {
            return _mapper.Map<List<CandidateDto>>(await _unit.Candidate.GetAllAsync());
        }

        // Get All Candidate Users
        public async Task<IEnumerable<IdentityUser>?> GetUsers()
        {
            return await _unit.User.GetAllAsync();
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
            Candidate candidate = await _unit.Candidate.GetAsync(id);
            _myDTO.Candidate = _mapper.Map<CandidateDto>(candidate);
            _myDTO.Candidate.UserEmail = candidate.IdentityUser.Email;
            if (_myDTO.Candidate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> Add(int id, [Bind("Id,FirstName,MiddleName,LastName,Gender,NativeLanguage," +
            "CountryOfResidence,Birthdate,Email,LandlineNumber,MobileNumber,Address1,Address2,PostalCode,Town," +
            "Province,PhotoIdType,PhotoIdNumber,PhotoIdDate,UserEmail,User,CandidateExams,CandidateExamStems")] 
            CandidateDto candidateDto)
        {
            IdentityUser user = await _unit.User.GetByEmail(candidateDto.UserEmail);
            candidateDto.User = user;
            
            Candidate candidate = _mapper.Map<Candidate>(candidateDto);
            _unit.Candidate.AddOrUpdate(candidate);
            
            if (id != candidate.Id)
            {
                _myDTO.View = "Create";
                _myDTO.Message = "The candidate Id was compromised. The request could " +
                    "not be completed due to security reasons. Please try again later.";
                _myDTO.Candidate = candidateDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested candidate has been added successfully.";
                _myDTO.View = "Index";

                if (await _unit.User.EmailExists(candidate.IdentityUser.Id, candidate.IdentityUser.Email))
                {
                    _myDTO.View = "Create";
                    _myDTO.Message = "This email address has already been claimed. " +
                        "Please try providing a different one.";
                    _myDTO.Candidate = candidateDto;
                    return _myDTO;
                }
                if (candidateDto.Email != candidateDto.UserEmail)
                {
                    _myDTO.View = "Create";
                    _myDTO.Message = "The fields Email and User_Email must " +
                        "have the same inputs!";
                    _myDTO.Candidate = candidateDto;
                    return _myDTO;
                }
                await _unit.SaveAsync();
                _myDTO.Candidates = _mapper.Map<List<CandidateDto>>
                                    (await _unit.Candidate.GetAllAsync());
            }
            else
            {
                _myDTO.View = "Create";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.Candidate = candidateDto;
            }
            return _myDTO;
        }

        public async Task<MyDTO> Update(int id, [Bind("Id,FirstName,MiddleName,LastName,Gender,NativeLanguage," +
            "CountryOfResidence,Birthdate,Email,LandlineNumber,MobileNumber,Address1,Address2,PostalCode,Town," +
            "Province,PhotoIdType,PhotoIdNumber,PhotoIdDate,UserEmail,User,CandidateExams,CandidateExamStems")]
            CandidateDto candidateDto)
        {
            Candidate candidate = await _unit.Candidate.GetAsync(candidateDto.Id);
            IdentityUser user = await _unit.User.GetByEmail(candidate.IdentityUser.Email);
            candidateDto.User = user;   
            candidateDto.User.UserName = candidateDto.UserEmail;   
            candidateDto.User.NormalizedUserName = candidateDto.UserEmail.ToUpper();   
            candidateDto.User.Email = candidateDto.UserEmail;   
            candidateDto.User.NormalizedEmail = candidateDto.UserEmail.ToUpper();   
            
            candidate.FirstName = candidateDto.FirstName;
            candidate.MiddleName = candidateDto.MiddleName;
            candidate.LastName = candidateDto.LastName;
            candidate.Gender = candidateDto.Gender;
            candidate.NativeLanguage = candidateDto.NativeLanguage;
            candidate.CountryOfResidence = candidateDto.CountryOfResidence;
            candidate.Birthdate = candidateDto.Birthdate;
            candidate.Email = candidateDto.Email;
            candidate.LandlineNumber = candidateDto.LandlineNumber;
            candidate.MobileNumber = candidateDto.MobileNumber;
            candidate.Address1 = candidateDto.Address1;
            candidate.Address2 = candidateDto.Address2;
            candidate.PostalCode = candidateDto.PostalCode;
            candidate.Town = candidateDto.Town;
            candidate.Province = candidateDto.Province;
            candidate.PhotoIdType = candidateDto.PhotoIdType;
            candidate.PhotoIdNumber = candidateDto.PhotoIdNumber;
            candidate.PhotoIdDate = candidateDto.PhotoIdDate;
            candidate.IdentityUser = candidateDto.User;
            candidate.CandidateExams = candidateDto.CandidateExams;

            _unit.Candidate.AddOrUpdate(candidate);

            if (id != candidate.Id)
            {
                _myDTO.View = "Edit";
                _myDTO.Message = "The candidate Id was compromised. The request could " +
                    "not be completed due to security reasons. Please try again later.";
                _myDTO.Candidate = candidateDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested candidate has been updated successfully.";
                _myDTO.View = "Index";

                if (!await _unit.Candidate.Exists(candidate.Id))
                {
                    _myDTO.Message = "The requested candidate could not be found. " +
                        "Please try again later.";
                    return _myDTO;
                }
                if (await _unit.User.EmailExists(candidate.IdentityUser.Id, candidate.IdentityUser.Email))
                {
                    _myDTO.View = "Edit";
                    _myDTO.Message = "This email address has already been claimed. " +
                        "Please try providing a different one.";
                    _myDTO.Candidate = candidateDto;
                    return _myDTO;
                }
                if (candidateDto.Email != candidateDto.UserEmail)
                {
                    _myDTO.View = "Edit";
                    _myDTO.Message = "The fields Email and User_Email must " +
                        "have the same inputs!";
                    _myDTO.Candidate = candidateDto;
                    return _myDTO;
                }
                await _unit.SaveAsync();
                _myDTO.Candidates = _mapper.Map<List<CandidateDto>>
                                    (await _unit.Candidate.GetAllAsync());
            }
            else
            {
                _myDTO.View = "Edit";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.Candidate = candidateDto;
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
                _myDTO.Candidates = _mapper.Map<List<CandidateDto>>(await _unit.Candidate.GetAllAsync());
                return _myDTO;
            }
            _myDTO.Candidate = _mapper.Map<CandidateDto>(await _unit.Candidate.GetAsync(id));
            if (_myDTO.Candidate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested candidate could not be found. Please try again later.";
                _myDTO.Candidates = _mapper.Map<List<CandidateDto>>(await _unit.Candidate.GetAllAsync());
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
            Candidate candidate = await _unit.Candidate.GetAsync(id);
            _unit.User.Delete(candidate.IdentityUser);
            _unit.Candidate.Delete(candidate);
            await _unit.SaveAsync();
            _myDTO.Candidates = _mapper.Map<List<CandidateDto>>(await _unit.Candidate.GetAllAsync());
            return _myDTO;
        }
    }
}
