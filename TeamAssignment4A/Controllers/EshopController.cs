using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        public EshopController(WebAppDbContext context, EShopService service,UserManager<IdentityUser> userManager) 
        {
            _context = context;
            _service = service;
            _myDTO = new MyDTO();
            _userManager = userManager;
        }

        // GET: Eshop
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Certificates List
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public async Task<IActionResult> ListOfCertificates() 
        {
            return View(await _service.GetAll());
        }

        // GET: Certificates List/Details/5
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public async Task<IActionResult> Details(int id) 
        {
            _myDTO = await _service.GetCert(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // GET: EShop/BuyExamVoucher
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> BuyExamVoucher(int id) 
        {
            _myDTO = await _service.GetExam(id)
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // POST: EShop/BuyExamVoucher

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> BuyExamVoucher(
            [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam) 
        {
            if (ModelState.IsValid) {
                if (User.Identity.IsAuthenticated) {
                    var user = await _userManager.GetUserAsync(User);
                    var UserId = user.Id;
                    Certificate certificate = _context.Certificates.Find(buyCertificateDTO.CertificateId);
                    Candidate candidate = _context.Candidates.Where(candidate => candidate.IdentityUserID == UserId).First();
                    string assessmentTestCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN()).Generate();
                    DateTime examinationDate = buyCertificateDTO.ExaminationDate;
                    Exam exam = new Exam(certificate);
                    _context.Add(exam);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Eshop", new { id = exam.Id });
                }
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