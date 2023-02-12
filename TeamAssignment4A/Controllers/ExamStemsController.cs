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
            var list = await _context.CandidateExams
                .Where(ce => ce.MarkerUserName == user.UserName).ToListAsync<CandidateExam>();
            return View(list);
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
        [ProducesResponseType(typeof(CandidateExamStem), 200)]
        public async Task<IActionResult> ExamGrading(int id)
        {
            var candidateExam = await _context.CandidateExams.Include(ce => ce.Exam)
                .Include(ce => ce.Exam.ExamStems)
                .Include(x => x.CandidateExamStems)
                .Include(x => x.Candidate).FirstOrDefaultAsync(x => x.Id == id);
            for(int i =0; i <4; i++)
            {
                candidateExam.Exam.ExamStems.ToList()[i] = _context.ExamStems
                    .Include(x => x.Stem).FirstOrDefault(x => x.Id == candidateExam.Exam.ExamStems.ToList()[i].Id);

            }
            List<CandidateExamStem>? ces = candidateExam.CandidateExamStems.ToList<CandidateExamStem>();
            return View(ces);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamGrading(List<CandidateExamStem> ces) {

            
            CandidateExam? candidateExam = await _context.CandidateExams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ces[1].CandidateExam.Id);
            candidateExam.Exam = await _context.Exams.Include(x => x.Certificate).AsNoTracking()
                .FirstOrDefaultAsync(x => x == ces[0].ExamStem.Exam);
            int i = 0;
            ces[0].CandidateExam = candidateExam;
            ces[1].CandidateExam = candidateExam;
            ces[2].CandidateExam = candidateExam;
            ces[3].CandidateExam = candidateExam;
            foreach (var cExStem in ces)
            {
                _context.Entry(cExStem.ExamStem).State = EntityState.Unchanged;
                _context.Update(cExStem);
                cExStem.ExamStem = _context.ExamStems
                    .Include(x => x.Stem).Include(x => x.Exam)
                    .FirstOrDefault(x => x.Id == cExStem.ExamStem.Id);
                
                if(cExStem.SubmittedAnswer == cExStem.ExamStem.Stem.CorrectAnswer)
                {
                    cExStem.Score = 25;
                    i++;
                    
                }
                
            }
            candidateExam.CandidateScore = i * 25;
            candidateExam.ScoreReportDate = DateTime.Now;
            if (candidateExam.CandidateScore >= candidateExam.Exam.Certificate.PassingGrade)
            {
                candidateExam.AssessmentResultLabel = "Pass";
            }
            else
            {
                candidateExam.AssessmentResultLabel = "Fail";
            }
            candidateExam.PercentageScore = Convert.ToString(candidateExam.CandidateScore) + "%";
            _context.Update(candidateExam);
            _context.SaveChanges();
            return RedirectToAction("CandidateExamsForMarking");
            
        }

        private bool ExamStemExists(int id)
        {
            return _context.ExamStems.Any(e => e.Id == id);
        }
    }
}
