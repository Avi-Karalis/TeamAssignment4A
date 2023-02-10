using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly CandidateExamService _service;
        private readonly UserManager<IdentityUser> _userManager;
        public CandidateExamsController(CandidateExamService service,
            UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, QA, Candidate")]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> Index()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            return View(await _service.GetAll(user));            
        }

        [Authorize(Roles = "Admin, Candidate")]
        [HttpGet, ActionName("SitForExam")]
        [ProducesResponseType(typeof(CandidateExamStem), 200)]
        public async Task<IActionResult> SitForExam(CandidateExam candExam)
        {
            List<string> selections = new List<string> { "A", "B", "C", "D" };
            ViewBag.Selections = new SelectList(selections);
            return View(await _service.GetByExam(candExam));
        }

        [Authorize(Roles = "Admin, Candidate")]
        [HttpPost, ActionName("SubmitExam")]
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
