using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
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
        [ProducesResponseType(typeof(CandidateExamDto), 200)]
        public async Task<IActionResult> Index()
        {
            return View(await _examService.GetAll());            
        }

        [HttpGet, ActionName("sitforexam")]
        [ProducesResponseType(typeof(ExamStemDto), 200)]
        public async Task<IActionResult> SitForExam(int id)
        {
            List<string> selections = new List<string> { "A", "B", "C", "D" };
            ViewBag.Selections = new SelectList(selections);
            IEnumerable<ExamStemDto> examStems = await _examStemService.GetExamStems(id);            
            return View(examStems);
        }

        [HttpPost, ActionName("submitexam")]
        [ProducesResponseType(typeof(ExamStemDto), 200)]
        public async Task<IActionResult> SubmitExam(ExamStemDto examStems)
        {
            
        }
    }
}
