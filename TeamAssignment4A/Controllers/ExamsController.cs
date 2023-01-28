using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    public class ExamsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly ExamService _service;        

        public ExamsController(WebAppDbContext context, ExamService service)
        {
            _db = context;
            _service = service;
        }
        //private readonly WebAppDbContext _context;

        //public ExamsController(WebAppDbContext context)
        //{
        //    _context = context;
        //}

        // GET: Exams

        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }
        //public async Task<IActionResult> Index()
        //{

        //    return View(await _context.Exams.ToListAsync());
        //}

        // GET: Exams/Details/5
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Details(int id)
        {
            MyDTO myDTO = await _service.Get(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            return View($"{myDTO.View}", myDTO.ExamDto);
        }
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Exams == null)
        //    {
        //        return NotFound();
        //    }

        //    var exam = await _context.Exams
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (exam == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(exam);
        //}

        // GET: Exams/Create
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public IActionResult Create()
        {
            ViewBag.Candidates = new SelectList(_db.Candidates, "Id", "Id");
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View();
        }
        //public IActionResult Create()
        //{
        //    ViewBag.Certificates = new SelectList(_context.Certificates, "Id", "TitleOfCertificate");
        //    ViewBag.Candidates = new SelectList(_context.Candidates, "Id", "LastName");
        //    return View();
        //}

        // POST: Exams/Create
        [HttpPost]
        [ProducesResponseType(typeof(ExamDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
            "CandidateScore,PercentageScore,AssessmentResultLabel,CandidateId,Candidate,TitleOfCertificate,Certificate")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            ViewBag.Candidates = new SelectList(_db.Candidates, "Id", "Id");
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View($"{myDTO.View}", myDTO.ExamDto);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("assessmentTestCode, CertificateId, CandidateId")] ExamCreateDTO examDTO)
        //{
        //    if (ModelState.IsValid) {
        //        Certificate certificate = _context.Certificates.Find(examDTO.CertificateId);
        //        Candidate candidate = _context.Candidates.Find(examDTO.CandidateId);
        //        Exam exam = new Exam(examDTO.assessmentTestCode, certificate, candidate);
        //        _context.Add(exam);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("TakeExam", "ExamStems", new { id = exam.Id });
        //    }
        //    return View(examDTO);
        //}

        // GET: Exams/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Edit(int id)
        {
            MyDTO myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = myDTO.Message;
            ViewBag.Candidates = new SelectList(_db.Candidates, "Id", "Id");
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            return View($"{myDTO.View}", myDTO.ExamDto);
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Exams == null)
        //    {
        //        return NotFound();
        //    }

        //    var exam = await _context.Exams.FindAsync(id);
        //    ViewBag.Certificates = new SelectList(_context.Certificates, "Id", "TitleOfCertificate");
        //    ViewBag.Candidates = new SelectList(_context.Candidates, "Id", "LastName");
        //    if (exam == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(exam);
        //}

        // POST: Exams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
            "CandidateScore,PercentageScore,AssessmentResultLabel,CandidateId,Candidate,TitleOfCertificate,Certificate")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            ViewBag.Candidates = new SelectList(_db.Candidates, "Id", "Id");
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View($"{myDTO.View}", myDTO.ExamDto);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate,CandidateScore,PercentageScore,AssessmentResultLabel")] Exam exam)
        //{
        //    if (id != exam.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(exam);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ExamExists(exam.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(exam);
        //}

        // GET: Exams/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            MyDTO myDTO = await _service.GetForDelete(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            return View($"{myDTO.View}", myDTO.ExamDto);
        }
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Exams == null)
        //    {
        //        return NotFound();
        //    }

        //    var exam = await _context.Exams
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (exam == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(exam);
        //}

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MyDTO myDTO = await _service.Delete(id);
            ViewBag.Message = myDTO.Message;
            return View($"{myDTO.View}", myDTO.ExamDtos);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Exams == null)
        //    {
        //        return Problem("Entity set 'WebAppDbContext.Exams'  is null.");
        //    }
        //    var exam = await _context.Exams.FindAsync(id);
        //    if (exam != null)
        //    {
        //        _context.Exams.Remove(exam);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ExamExists(int id)
        //{
        //    return _context.Exams.Any(e => e.Id == id);
        //}
    }
}
