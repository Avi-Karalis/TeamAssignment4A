using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public class EShopService : ControllerBase
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private MyDTO _myDTO;
        public EShopService(WebAppDbContext db, UnitOfWork unit)
        {
            _db = db;
            _unit = unit;
            _myDTO = new MyDTO();
        }

        // Get Candidate Exam by its Id
        public async Task<CandidateExam?> Get(int id)
        {
            return await _unit.CandidateExam.GetAsync(id);
        }

        // Get Certificate by its Certificate Id
        public async Task<MyDTO> GetCert(int id)
        {
            if (id == null || _db.Certificates == null || await _unit.Certificate.GetAsync(id) == null)
            {
                _myDTO.View = "ListOfCertificates";
                _myDTO.Message = "The requested Certificate could not be found. Please try again later.";
                _myDTO.Certificates = await _unit.Certificate.GetAllAsync();
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.Certificate = await _unit.Certificate.GetAsync(id);
            }
            return _myDTO;
        }

        // Get all Certificates from database
        public async Task<IEnumerable<Certificate>?> GetAll()
        {
            return await _unit.Certificate.GetAllAsync();
        }
    
        // Get Candidate_Exam template filled with Candidate, Exam
        // and AssessmentTestCode fields
        public async Task<MyDTO> GetExam(int id,IdentityUser user)
        {
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            Exam? exam = await _unit.Exam.GetByCertId(id);
            
            if(candidate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "There was a problem while retrieving your User Id.\n" +
                    "Please try logging out and logging in again.\nIf the problem persists " +
                    "please contact our support team.";
                return _myDTO;
            }
            else if(exam == null)
            {
                _myDTO.View = "ListOfCertificates";
                _myDTO.Message = "The requested certificate could not be retrieved.\n" +
                    "Please try selecting a certificate again later.";
                _myDTO.Certificates = await _unit.Certificate.GetAllAsync();
                return _myDTO;
            }
            else
            {
                CandidateExam candidateExam = new CandidateExam
                {
                    Candidate = candidate,
                    Exam = exam,
                    AssessmentTestCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN()).Generate()
            };
                _myDTO.View = "BuyExamVoucher";
                _myDTO.Message = "Please select the preferred date and time for your exam.";
                _myDTO.CandidateExam = candidateExam;
                _unit.CandidateExam.AddOrUpdate(candidateExam);
                await _unit.SaveAsync();
            }
            
            return _myDTO;
        }

        // Get Marked Exam for a specific User
        public async Task<MyDTO> GetMarkedExam(int id, IdentityUser user)
        {
            if (id == null || _db.CandidateExams == null || await _unit.CandidateExam.GetAsync(id) == null)
            {
                _myDTO.View = "MarkedCertifications";
                _myDTO.Message = "The requested Certification could not be found." +
                    "\nPlease try again later.";               
                Candidate? candidate = await _unit.Candidate.GetByUser(user);
                _myDTO.CandidateExams = await _unit.CandidateExam.GetMarkedExams(candidate);
            }
            else
            {
                _myDTO.View = "ExamDetails";
                _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);
            }
            return _myDTO;
        }

        // Confirm Exam Voucher buy and add examination in database
        public async Task<MyDTO> BuyExam(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam)
        {
            if (id != candidateExam.Id)
            {
                _myDTO.View = "BuyExamVoucher";
                _myDTO.Message = "The exam Id was compromised. The request could\nnot " +
                    "be completed due to security reasons.\nPlease try again later.";
                _myDTO.CandidateExam = candidateExam;
                return _myDTO;
            }
            
            if (ModelState.IsValid)
            {
                var candidate = await _unit.Candidate.GetCandForExam(candidateExam.Candidate.Id);
                var exam = await _unit.Exam.GetAsync(candidateExam.Exam.Id);
                
                
                _myDTO.View = "Index";
                _myDTO.Message = "The purchase was completed successfully." +
                    "\nYour exam voucher has been sent to your email.";
                candidateExam.Candidate = candidate;
                candidateExam.Exam = exam;
                int i = 0;
                foreach(var examStem in candidateExam.Exam.ExamStems)
                {
                    candidateExam.Exam.ExamStems.ToList()[i]
                    = await _unit.ExamStem.GetAsync(examStem.Id);
                    i++;
                }
                _db.Entry(candidateExam.Candidate).State = EntityState.Unchanged;
                _db.Entry(candidateExam.Exam).State = EntityState.Unchanged;
                _unit.CandidateExam.AddOrUpdate(candidateExam);
                await _unit.SaveAsync();
                return _myDTO;

            }
            else
            {
                _myDTO.View = "BuyExamVoucher";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.CandidateExam = candidateExam;
            }
            return _myDTO;
        }
    
        // Get booked exams for a Candidate that he has not already sat for
        // in order for them to change the examination date
        public async Task<IEnumerable<CandidateExam>?> GetBooked(IdentityUser user)
        {
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            return await _unit.CandidateExam.GetBooked(candidate.Id);
        }

        // Get the specific exam that the user wants to update
        public async Task<MyDTO> GetForUpdate(int id, IdentityUser user)
        {
            _myDTO.View = "ChangeDate";
            if (id == null || _db.CandidateExams == null)
            {
                _myDTO.View = "BookedExams";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                Candidate? candidate = await _unit.Candidate.GetByUser(user);
                _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
                return _myDTO;
            }
            
            _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);
            
            if (_myDTO.CandidateExam == null)
            {
                _myDTO.View = "BookedExams";
                _myDTO.Message = "The requested exam could not be found. Please try again later.";
                Candidate? candidate = await _unit.Candidate.GetByUser(user);
                _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
            }
            return _myDTO;
        }

        // Change the Examination date
        public async Task<MyDTO> UpdateDate(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam, IdentityUser user)
        {
            if (id != candidateExam.Id)
            {
                _myDTO.View = "ChangeDate";
                _myDTO.Message = "The exam Id was compromised. The request could\nnot " +
                    "be completed due to security reasons.\nPlease try again later.";
                _myDTO.CandidateExam = candidateExam;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The examination date has been updated successfully.";
                if (!await _unit.CandidateExam.Exists(candidateExam.Id))
                {
                    _myDTO.View = "BookedExams";
                    _myDTO.Message = "The requested exam could not be found." +
                        "\nPlease try again later.";
                    Candidate? candidate = await _unit.Candidate.GetByUser(user);
                    _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
                    return _myDTO;
                }
                _unit.CandidateExam.AddOrUpdate(candidateExam);
                await _unit.SaveAsync();
                
            }
            else
            {
                _myDTO.View = "ChangeDate";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.CandidateExam = candidateExam;
            }
            return _myDTO;
        }

        // Find the exam you want to delete in the database
        public async Task<MyDTO> GetForDelete(int id, IdentityUser user)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.CandidateExams == null)
            {
                _myDTO.View = "BookedExams";
                _myDTO.Message = "The requested exam could not be found." +
                    "\nPlease try again later.";
                Candidate? candidate = await _unit.Candidate.GetByUser(user);
                _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
                return _myDTO;
            }
            _myDTO.CandidateExam = await _unit.CandidateExam.GetAsync(id);

            if (_myDTO.CandidateExam == null)
            {
                _myDTO.View = "BookedExams";
                _myDTO.Message = "The requested exam could not be found." +
                    "\nPlease try again later.";
                Candidate? candidate = await _unit.Candidate.GetByUser(user);
                _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
            }
            return _myDTO;
        }

        // Delete the selected exam
        public async Task<MyDTO> Delete(int id, IdentityUser user)
        {
            _myDTO.View = "BookedExams";
            _myDTO.Message = "The requested exam has been refunded successfully.";
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            if (!await _unit.CandidateExam.Exists(id))
            {
                _myDTO.Message = "The requested exam could not be found." +
                    "\nPlease try again later.";
                _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
                return _myDTO;
            }
            CandidateExam candidateExam = await _unit.CandidateExam.GetAsync(id);
            _unit.CandidateExam.Delete(candidateExam);
            await _unit.SaveAsync();
            _myDTO.CandidateExams = await _unit.CandidateExam.GetBooked(candidate.Id);
            return _myDTO;
        }

        // Get all marked exams of a candidate
        public async Task<MyDTO> GetMarkedExams(IdentityUser user)
        {
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            _myDTO.CandidateExams = await _unit.CandidateExam.GetMarkedExams(candidate);
            if (_myDTO.CandidateExams == null)
            {
                _myDTO.Message = "You do not have Marked Certificates yet." +
                    "\nPlease try again later.";
            }
            return _myDTO;
        }
    }
}
