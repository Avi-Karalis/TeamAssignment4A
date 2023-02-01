using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Controllers
{
    public class ExamStemsController : Controller
    {
        private readonly WebAppDbContext _context;

        public ExamStemsController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: ExamStems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExamStems.ToListAsync());
        }

        //Take an exam
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExamSubmit(ExamSubmissionDTO examSubmissionDTO) {

        //    if (ModelState.IsValid) {
        //    Exam exam = _context.Exams.Find(examSubmissionDTO.ExamQuestions.FirstOrDefault().ExamId);
        //        foreach(var examQuestion in examSubmissionDTO.ExamQuestions) {
        //            ExamStem examStem = new ExamStem();
        //            examStem.SubmittedAnswer = examQuestion.Answer;
                    
        //            examStem.Exam = exam;
        //            examStem.Stem = _context.Stems.Find(examQuestion.StemId);
        //            if (examQuestion.Answer == examStem.Stem.CorrectAnswer) {
        //                examStem.Score = 1;
        //            } else {
        //                examStem.Score = 0;
        //            }
        //            _context.Add(examStem);
                    
        //        }
        //        exam.ExaminationDate = DateTime.Now;
        //        _context.Update(exam);
        //        _context.SaveChanges();
                
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}

        // GET: ExamStems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExamStems == null)
            {
                return NotFound();
            }

            var examStem = await _context.ExamStems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examStem == null)
            {
                return NotFound();
            }

            return View(examStem);
        }

        // GET: ExamStems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamStems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubmittedAnswer,Score")] ExamStem examStem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examStem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examStem);
        }

        // GET: ExamStems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExamStems == null)
            {
                return NotFound();
            }

            var examStem = await _context.ExamStems.FindAsync(id);
            if (examStem == null)
            {
                return NotFound();
            }
            return View(examStem);
        }

        // POST: ExamStems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubmittedAnswer,Score")] ExamStem examStem)
        {
            if (id != examStem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examStem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamStemExists(examStem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(examStem);
        }

        // GET: ExamStems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExamStems == null)
            {
                return NotFound();
            }

            var examStem = await _context.ExamStems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examStem == null)
            {
                return NotFound();
            }

            return View(examStem);
        }

        // POST: ExamStems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExamStems == null)
            {
                return Problem("Entity set 'WebAppDbContext.ExamStems'  is null.");
            }
            var examStem = await _context.ExamStems.FindAsync(id);
            if (examStem != null)
            {
                _context.ExamStems.Remove(examStem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamStemExists(int id)
        {
            return _context.ExamStems.Any(e => e.Id == id);
        }
    }
}
