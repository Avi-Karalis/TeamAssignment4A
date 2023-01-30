using Microsoft.AspNetCore.Mvc;
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

        public CandidateExamsController(WebAppDbContext context,
            ExamService service, ExamStemService examStemService)
        {
            _db = context;
            _examService = service;
            _examStemService = examStemService;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Index()
        {
            return View(await _examService.GetAll());            
        }

        [HttpGet]
        [ProducesResponseType(typeof(ExamStemDto), 200)]
        public async Task<IActionResult> SitForExam(int id)
        {
            await _examStemService.GetExamStems(id);
            return View();
        }
    }
}
