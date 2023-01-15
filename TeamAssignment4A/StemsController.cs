using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A
{
    public class StemsController : Controller
    {
        private WebAppDbContext _context;

        public StemsController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: Stems
        public ActionResult Index()
        {
              return View(_context.Stems.ToList());
        }

        // GET: Stems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || _context.Stems == null)
            {
                return NotFound();
            }

            var stem = _context.Stems
                .FirstOrDefault(m => m.Id == id);
            if (stem == null)
            {
                return NotFound();
            }

            return View(stem);
        }

        // GET: Stems/Create
        public ActionResult Create()
        {
            System.Diagnostics.Debug.WriteLine("Karalis");
            return View();
        }

        // POST: Stems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer")] Stem stem)
        {
            System.Diagnostics.Debug.WriteLine(stem.Question);
            System.Diagnostics.Debug.WriteLine("Karalis");

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("I am inside you");
                _context.Stems.Add(stem);
                //_context.Add(stem);
                _context.SaveChanges();
                TempData["messageSuccess"] = $"Stem with ID {stem.Id} was created successfuly";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Stems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || _context.Stems == null)
            {
                return NotFound();
            }

            var stem =  _context.Stems.Find(id);
            if (stem == null)
            {
                return NotFound();
            }
            return View(stem);
        }

        // POST: Stems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer")] Stem stem)
        {
            if (id != stem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stem);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StemExists(stem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(stem);
        }

        // GET: Stems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || _context.Stems == null)
            {
                return NotFound();
            }

            var stem = _context.Stems
                .FirstOrDefault(m => m.Id == id);
            if (stem == null)
            {
                return NotFound();
            }

            return View(stem);
        }

        // POST: Stems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_context.Stems == null)
            {
                return Problem("Entity set 'WebAppDbContext.Stems'  is null.");
            }
            var stem =  _context.Stems.Find(id);
            if (stem != null)
            {
                _context.Stems.Remove(stem);
            }
            
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StemExists(int id)
        {
          return _context.Stems.Any(e => e.Id == id);
        }
    }
}
