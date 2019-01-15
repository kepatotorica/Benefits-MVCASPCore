using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Benefacts.Models;
using Microsoft.AspNetCore.Http;
//using static Benefacts.Controllers.UsersController;

namespace Benefacts.Controllers
{
    public class RelativesController : Controller
    {
        private readonly cSharpContext _context;

        public RelativesController(cSharpContext context)
        {
            _context = context;
        }

        // GET: Relatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

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

        // GET: Users/Create
        public IActionResult Create(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            ViewData["UId"] = id; //force the id to be that of the user we are creating this relative from
            return View();
        }


        // POST: Relatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelId,FName,LName,Relation,UId")] Relative relative)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (ModelState.IsValid)
            {
                _context.Add(relative);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Users", new { id = relative.UId });
            }
            ViewData["UId"] = new SelectList(_context.Users, "UId", "UId", relative.UId);
            Console.WriteLine(ViewData["UId"]);
            return View(relative);
        }

        // GET: Relatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (id == null)
            {
                return NotFound();
            }

            var relative = await _context.Relative.FindAsync(id);
            if (relative == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FirstOrDefaultAsync(u => u.UId == relative.UId);
            ViewData["UId"] = users.UId;
            ViewData["Types"] = new SelectList(_context.Relative.Select(t => t.Relation).Distinct());

            return View(relative);
        }

        // POST: Relatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelId,FName,LName,Relation,UId")] Relative relative)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

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
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Users", new { id = relative.UId });
            }
            ViewData["UId"] = new SelectList(_context.Users, "UId", "UId", relative.UId);
            return View(relative);
        }

        // GET: Relatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

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
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            var relative = await _context.Relative.FindAsync(id);
            _context.Relative.Remove(relative);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Details", "Users", new { id = relative.UId });
        }

        private bool RelativeExists(int id)
        {
            return _context.Relative.Any(e => e.RelId == id);
        }
    }
}
