using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    [Authorize(Roles = "Admin, QA, Candidate")]
    public class CandidateExamsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly ExamService _examService;
        private readonly ExamStemService _examStemService;
        private readonly CandidateExamService _service;
        private readonly IMapper _mapper;

        public CandidateExamsController(WebAppDbContext context, 
            ExamStemService examStemService, CandidateExamService service, IMapper mapper)
        {
            _db = context;
            _service = service;
            _examStemService = examStemService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, QA, Candidate")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet, ActionName("examslist")]
        [Authorize(Roles = "Admin, QA, Candidate")]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> CandidateExamsList(int candidateId)
        {
            return View(await _service.GetAll(candidateId));            
        }

        [Authorize(Roles = "Admin, Candidate")]
        [HttpGet, ActionName("sitforexam")]
        [ProducesResponseType(typeof(CandidateExamStem), 200)]
        public async Task<IActionResult> SitForExam(CandidateExam candExam)
        {
            List<string> selections = new List<string> { "A", "B", "C", "D" };
            ViewBag.Selections = new SelectList(selections);
            return View(await _service.GetByExam(candExam));
        }

        [Authorize(Roles = "Admin, Candidate")]
        [HttpPost, ActionName("submitexam")]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> SubmitExam([Bind("Id,SubmittedAnswer," +
                "Score,Candidate,ExamStem,CandidateExam")] IEnumerable<CandidateExamStem> cExStems)
        {
            MyDTO myDTO = await _service.SubmitAnswers(cExStems);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "sitforexam")
            {
                return RedirectToAction($"{myDTO.View}", myDTO.CandidateExamStems);
                
            }
            return RedirectToAction($"{myDTO.View}", "Home");
        }
    }
}
