using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers {
    [Authorize(Roles = "Admin, Candidate")]
    public class EshopController : Controller 
    {
        private readonly WebAppDbContext _context;
        private readonly EShopService _service;
        private MyDTO _myDTO;
        public EshopController(WebAppDbContext context, EShopService service) 
        {
            _context = context;
            _service = service;
            _myDTO = new MyDTO();
        }

        // GET: Eshop
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public async Task<IActionResult> Index() 
        {
            return View(await _service.GetAll());
        }

        // GET: Eshop/Details/5
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public async Task<IActionResult> Details(int id) 
        {
            _myDTO = await _service.Get(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // GET: EShop/BuyExam

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
        public async Task<IActionResult> BuyExam([Bind("CertificateId, ΕxaminationDate, CandidateId")] BuyCertificateDTO buyCertificateDTO) {
            if (ModelState.IsValid) {
                
                Certificate certificate = _context.Certificates.Find(buyCertificateDTO.CertificateId);
                Candidate candidate = _context.Candidates.Find(buyCertificateDTO.CandidateId);
                string assessmentTestCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN()).Generate();
                DateTime examinationDate = buyCertificateDTO.ExaminationDate;
                Exam exam = new Exam(certificate);
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Eshop", new { id = exam.Id });
            }
            return View(buyCertificateDTO);
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