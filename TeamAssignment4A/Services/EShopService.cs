using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        private readonly UserManager<IdentityUser> _userManager;
        public EShopService(WebAppDbContext db, UnitOfWork unit, 
            UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
            _userManager = userManager;
        }

        // Get Certificate by its Id
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
    
        // Get Candidate Exam template filled with Certificate field
        public async Task<MyDTO> GetExam(int id)
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            Candidate? candidate = await _unit.Candidate.GetByUser(user);
            Exam? exam = await _unit.Exam.GetByCertId(id);
            string assessmentTestCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN()).Generate();
            if(candidate != null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "There was a problem while retrieving your User Id. " +
                    "Please try logging out and logging in again.";
                return _myDTO;
            }
            else if(exam != null)
            {
                _myDTO.View = "ListOfCertificates";
                _myDTO.Message = "The requested certificate could not be retrieved. " +
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
                    AssessmentTestCode = assessmentTestCode
                }; //(candidate, exam, assessmentTestCode);
                _myDTO.View = "BuyExamVoucher";
                _myDTO.Message = "Please select the preferred date and time for your exam.";
            }
            
            return _myDTO;
        }

        public async Task<MyDTO> Add([Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam)
        {
            


        }
    }
}
