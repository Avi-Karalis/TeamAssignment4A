using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Controllers
{
    [Authorize(Roles = "Admin, Marker")]
    public class ExamStemsController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ExamStemsController(WebAppDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ExamStems
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> CandidateExamsForMarking()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            return View(await _context.CandidateExams
                .Where(ce => ce.MarkerUserName == user.UserName).ToListAsync<CandidateExam>());
        }

        //Take an candidateExam
        public async Task<IActionResult> TakeExam(int id) {
            Exam exam = _context.Exams.Include(e => e.Certificate).Where(e => e.Id == id).FirstOrDefault();
            List <Stem> listOfStemsToBeExamined = _context.Stems.Include(s => s.Topic).Where(c => c.Topic.Certificate.Id == exam.Certificate.Id).ToList();
            List<ExamQuestion> examQuestions = new List<ExamQuestion>();
            foreach (var stem in listOfStemsToBeExamined) {
                ExamQuestion examQuestion = new ExamQuestion(stem, id);
                examQuestions.Add(examQuestion);
            }
            List<string> selections = new List<string>{ "A", "B", "C", "D" };
            ExamSubmissionDTO examSubmissionDTO = new ExamSubmissionDTO();
            examSubmissionDTO.ExamQuestions = examQuestions;
            ViewBag.Selections = new SelectList(selections);

            //ViewBag.ExamTakeDTOs = examTakeDTOs;
            return View(examSubmissionDTO);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamGrading(int id)
        {
            var candidateExam = await _context.CandidateExams.FirstOrDefaultAsync(x => x.Id == id);
            List<CandidateExamStem>? ces = candidateExam.CandidateExamStems as List<CandidateExamStem>;
            return View(ces);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamGrading(List<CandidateExamStem> ces, int examId, int examStemId) {

            if (ModelState.IsValid) {
                CandidateExam candidateExam = _context.CandidateExams.Find(examSubmissionDTO.ExamQuestions.FirstOrDefault().CandidateExamId);
                foreach (var examQuestion in examSubmissionDTO.ExamQuestions) {
                    CandidateExamStem candidateExamStem = new CandidateExamStem();
                    candidateExamStem.SubmittedAnswer = examQuestion.Answer;

                    candidateExamStem.CandidateExam = candidateExam;
                    candidateExamStem.ExamStem.Stem = _context.Stems.Find(examQuestion.StemId);
                    if (examQuestion.Answer == candidateExamStem.ExamStem.Stem.CorrectAnswer) {
                        candidateExamStem.Score = 25;
                    } else {
                        candidateExamStem.Score = 0;
                    }
                    _context.Add(candidateExamStem);

                }
                candidateExam.ExaminationDate = DateTime.Now;

                if (candidateExam.CandidateScore >= candidateExam.Exam.Certificate.PassingGrade) {
                    candidateExam.AssessmentResultLabel = "Pass";
                } else {
                    candidateExam.AssessmentResultLabel = "Fail";
                }
                var percent = (candidateExam.CandidateScore / 100) * 100;
                candidateExam.PercentageScore = Convert.ToString(percent) + "%";
                _context.Update(candidateExam);
                _context.SaveChanges();

                return RedirectToAction("CandidateExamsForMarking");
            }
            return RedirectToAction("Index", "Home");
        }

        private bool ExamStemExists(int id)
        {
            return _context.ExamStems.Any(e => e.Id == id);
        }
    }
}
