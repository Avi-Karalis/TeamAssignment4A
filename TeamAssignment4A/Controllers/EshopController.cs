using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Controllers {
    public class EshopController : Controller {
        private readonly WebAppDbContext _context;

        public EshopController(WebAppDbContext context) {
            _context = context;
        }

        // GET: Eshop
        public async Task<IActionResult> Index() {
            return View(await _context.CandidateExams.ToListAsync());
        }

        // GET: Eshop/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.CandidateExams == null) {
                return NotFound();
            }

            var candidateExam = await _context.CandidateExams
                .FirstOrDefaultAsync(m => m.CandidateExamId == id);
            if (candidateExam == null) {
                return NotFound();
            }

            return View(candidateExam);
        }

        // GET: EShop/Create
        public IActionResult BuyExam() {
            ViewBag.Certificates = new SelectList(_context.Certificates, "Id", "TitleOfCertificate");
            ViewBag.Candidates = new SelectList(_context.Candidates, "Id", "LastName");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyExam([Bind("CertificateId, CandidateId")] ExamCreateDTO examDTO) {
            if (ModelState.IsValid) {
                Certificate certificate = _context.Certificates.Find(examDTO.CertificateId);
                Candidate candidate = _context.Candidates.Find(examDTO.CandidateId);
                string assessmentTestCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN()).Generate();
                Exam exam = new Exam(assessmentTestCode, certificate, candidate);
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Eshop", new { id = exam.Id });
            }
            return View(examDTO);
        }


        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Exams == null) {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            ViewBag.Certificates = new SelectList(_context.Certificates, "Id", "TitleOfCertificate");
            ViewBag.Candidates = new SelectList(_context.Candidates, "Id", "LastName");
            if (exam == null) {
                return NotFound();
            }
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate,CandidateScore,PercentageScore,AssessmentResultLabel")] Exam exam) {
            if (id != exam.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ExamExists(exam.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }
        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Exams == null) {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null) {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Exams == null) {
                return Problem("Entity set 'WebAppDbContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null) {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id) {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}