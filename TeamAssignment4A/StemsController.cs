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
        private readonly WebAppDbContext _context;

        public StemsController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: Stems
        public async Task<IActionResult> Index()
        {
              return View(await _context.Stems.ToListAsync());
        }

        // GET: Stems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stems == null)
            {
                return NotFound();
            }

            var stem = await _context.Stems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stem == null)
            {
                return NotFound();
            }

            return View(stem);
        }

        // GET: Stems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer")] Stem stem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stem);
        }

        // GET: Stems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stems == null)
            {
                return NotFound();
            }

            var stem = await _context.Stems.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer")] Stem stem)
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
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(stem);
        }

        // GET: Stems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stems == null)
            {
                return NotFound();
            }

            var stem = await _context.Stems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stem == null)
            {
                return NotFound();
            }

            return View(stem);
        }

        // POST: Stems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stems == null)
            {
                return Problem("Entity set 'WebAppDbContext.Stems'  is null.");
            }
            var stem = await _context.Stems.FindAsync(id);
            if (stem != null)
            {
                _context.Stems.Remove(stem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StemExists(int id)
        {
          return _context.Stems.Any(e => e.Id == id);
        }
    }
}
