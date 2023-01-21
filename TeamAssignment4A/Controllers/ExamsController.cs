using System;
using System.Collections;
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
    public class ExamsController : Controller
    {
        private readonly WebAppDbContext _context;

        public ExamsController(WebAppDbContext context){
            _context = context;
        }

        // GET: Exam
        public async Task<IActionResult> Index(){
            //List<Certificate> certList = _context.Certificates.ToList();
            //List<Topic> topicList = _context.Topics.ToList();
            List<Exam> examList = _context.Exams.Include(e => e.ExamTopics).Include(e => e.Certificate).ToList();
            List<ExamIndexDto> examIndexDtos = new List<ExamIndexDto>();
            foreach (var exam in examList) {
                ExamIndexDto examIndexDto = new ExamIndexDto();
                examIndexDto.Id = exam.Id;
                examIndexDto.ExamDescription = exam.ExamDescription;
                examIndexDto.CertificateTitle = exam.Certificate.TitleOfCertificate;
                List<ExamTopic> examTopics = exam.ExamTopics.ToList();
                examIndexDto.Topic1Description = examTopics[0].Topic.Description;
                examIndexDto.Topic2Description = examTopics[1].Topic.Description;
                examIndexDtos.Add(examIndexDto);
            }
            return View(examIndexDtos);
        }

        // GET: Exam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exam/Create
        public IActionResult Create()
        {
            ViewBag.Certificates = new SelectList(_context.Certificates, "Id", "TitleOfCertificate");
            ViewBag.Topics = new SelectList(_context.Topics, "Id", "Description");
            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamDescription, CertificateId, Topic1Id, Topic2Id")] ExamCreateDTO examCreateDTO)
        {
            if (ModelState.IsValid)
            {
                Exam exam = new Exam();
                exam.ExamDescription = examCreateDTO.ExamDescription;
                exam.CertificateId = examCreateDTO.CertificateId;
                _context.Add(exam);
                _context.SaveChanges();
                Topic topic1 = _context.Topics.Find(examCreateDTO.Topic1Id);
                Topic topic2 = _context.Topics.Find(examCreateDTO.Topic2Id);
                ExamTopic examTopic1 = new ExamTopic();
                examTopic1.Exam = exam;
                examTopic1.Topic = topic1;
                ExamTopic examTopic2 = new ExamTopic();
                examTopic2.Exam = exam;
                examTopic2.Topic = topic2;
                _context.Add(examTopic1);
                _context.Add(examTopic2);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Exam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExamDescription")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
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
            return View(exam);
        }

        // GET: Exam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'WebAppDbContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
