using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    public class CandidateExamsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly ExamService _examService;
        private readonly ExamStemService _examStemService;
        private readonly IMapper _mapper;

        public CandidateExamsController(WebAppDbContext context,
            ExamService service, ExamStemService examStemService, IMapper mapper)
        {
            _db = context;
            _examService = service;
            _examStemService = examStemService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> Index()
        {
            return View(await _examService.GetAll());            
        }

        [HttpGet, ActionName("sitforexam")]
        [ProducesResponseType(typeof(ExamStem), 200)]
        public async Task<IActionResult> SitForExam(Exam exam)
        {
            List<string> selections = new List<string> { "A", "B", "C", "D" };
            ViewBag.Selections = new SelectList(selections);
            IEnumerable<ExamStem> examStems = await _examStemService.GetByExam(exam);            
            return View(examStems);
        }

        [HttpPost, ActionName("submitexam")]
        [ProducesResponseType(typeof(ExamStem), 200)]
        public async Task<IActionResult> SubmitExam(int examId,
                        [Bind("Id,SubmittedAnswer,Score,Exam,Stem")] IEnumerable<ExamStem> examStems)
        {
            //MyDTO myDTO = await _examStemService.SubmitExamStems(examId, examStems);
            //ViewBag.Message = myDTO.Message;
            //if (myDTO.View == "Index")
            //{
            //    return View($"{myDTO.View}", myDTO.Candidates);
            //}
            return View();
        }
    }
}
