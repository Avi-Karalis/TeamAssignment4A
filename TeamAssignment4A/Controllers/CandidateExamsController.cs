using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Data;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    public class CandidateExamsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly ExamService _service;

        public CandidateExamsController(WebAppDbContext context, ExamService service)
        {
            _db = context;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());            
        }

        public async Task<IActionResult> SitForExam(int id)
        {
            return View();
        }
    }
}
