using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCASPCore.Models;

namespace MVCASPCore.Controllers
{
    public class RelativesController : Controller
    {
        private readonly cSharpContext _context;

        public RelativesController(cSharpContext context)
        {
            _context = context;
        }

        // GET: Relatives
        public async Task<IActionResult> Index()
        {
            var cSharpContext = _context.Relative.Include(r => r.U);
            return View(await cSharpContext.ToListAsync());
        }

        // GET: Relatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relative = await _context.Relative
                .Include(r => r.U)
                .FirstOrDefaultAsync(m => m.RelId == id);
            if (relative == null)
            {
                return NotFound();
            }

            return View(relative);
        }

        // GET: Relatives/Create
        public IActionResult Create()
        {
            ViewData["UId"] = new SelectList(_context.Users, "UId", "UId");
            return View();
        }

        // POST: Relatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelId,FName,LName,Relation,UId")] Relative relative)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UId"] = new SelectList(_context.Users, "UId", "UId", relative.UId);
            return View(relative);
        }

        // GET: Relatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relative = await _context.Relative.FindAsync(id);
            if (relative == null)
            {
                return NotFound();
            }
            ViewData["UId"] = new SelectList(_context.Users, "UId", "UId", relative.UId);
            return View(relative);
        }

        // POST: Relatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelId,FName,LName,Relation,UId")] Relative relative)
        {
            if (id != relative.RelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relative);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelativeExists(relative.RelId))
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
            ViewData["UId"] = new SelectList(_context.Users, "UId", "UId", relative.UId);
            return View(relative);
        }

        // GET: Relatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relative = await _context.Relative
                .Include(r => r.U)
                .FirstOrDefaultAsync(m => m.RelId == id);
            if (relative == null)
            {
                return NotFound();
            }

            return View(relative);
        }

        // POST: Relatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relative = await _context.Relative.FindAsync(id);
            _context.Relative.Remove(relative);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelativeExists(int id)
        {
            return _context.Relative.Any(e => e.RelId == id);
        }
    }
}
