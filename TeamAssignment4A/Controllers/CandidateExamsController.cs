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
        public async Task<IActionResult> SitForExam(int id)
        {
            List<string> selections = new List<string> { "A", "B", "C", "D" };
            ViewBag.Selections = new SelectList(selections);
            return View(await _service.GetExamStems(id));
        }

        [Authorize(Roles = "Admin, Candidate")]
        [HttpPost, ActionName("SubmitExam")]
        [ProducesResponseType(typeof(CandidateExamStem), 200)]
        public async Task<IActionResult> SubmitExam([Bind("Id,SubmittedAnswer,Score,ExamStem," +
            "CandidateExam")] List<CandidateExamStem> ces)
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            CandidateExam? canExam = 
                await _service.GetCanExamForInput(user, ces.ElementAt(1).ExamStem.Exam.Id);
            MyDTO myDTO = await _service.SubmitAnswers(canExam, ces);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "SitForExam")
            {
                return RedirectToAction($"{myDTO.View}", myDTO.CandidateExam);

            }
            return RedirectToAction($"{myDTO.View}", "Home");
        }
    }
}
