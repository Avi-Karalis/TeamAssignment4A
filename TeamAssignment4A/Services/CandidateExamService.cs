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
            return await _unit.CandidateExam.GetBooked(candidate.Id);
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
                    _myDTO.Message = "A question was left unanswered." +
                        "\nPlease check your exam for unfilled answers.";
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


    }
}
