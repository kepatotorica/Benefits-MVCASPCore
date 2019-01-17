using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Benefacts.Models;
using Microsoft.AspNetCore.Http;

namespace Benefacts.Controllers
{
    public class RelativesController : Controller
    {
        private readonly cSharpContext _context;

        public RelativesController(cSharpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Servers the form for creation 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0) //I would have made this a function, but the problem is the return needs to break us out of the function
            {                                            //to do this would still require the if statement, atleast how I thought if. I could defenitly be wrong
                return RedirectToAction("Login", "Admins");
            }
            ViewData["UId"] = id; //force the id to be that of the user we are creating this relative from
            return View();
        }


        /// <summary>
        /// The database logic for the creation of a new relative
        /// </summary>
        /// <param name="relative"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the view containing the form that allows us to edit a relative
        /// </summary>
        /// <param name="id"> the id of the relative we are editing</param>
        /// <returns> A view for the relative with the passed in id</returns>
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


        /// <summary>
        /// the database interactions needed to edit a relative. Happens after we post from
        /// the form provided by the Edit(int? id) method
        /// </summary>
        /// <param name="id"> The id of the relative we are editing </param>
        /// <param name="relative"> the updated values for the relative we are editing</param>
        /// <returns> A view to the user that this relative is linked to</returns>
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

        /// <summary>
        /// Returns the view that contains the delete form for given relative
        /// </summary>
        /// <param name="id"> the relative we are asking to remove</param>
        /// <returns> A view that displays a relative, and prompts if we are sure about deleting them</returns>
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

        /// <summary>
        /// The database interatctions needed to delete a specified user from the database. Happens
        /// after we post from the Delete(int? id)
        /// </summary>
        /// <param name="id"> The id of the relative that we are removing</param>
        /// <returns> A view of the user that this relative USED to belong to</returns>
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
        
        /// <summary>
        /// checks if a relative exits
        /// </summary>
        /// <param name="id"> the id of the relative we are checking for</param>
        /// <returns> returns true or false depending on if a relative can be found</returns>
        private bool RelativeExists(int id)
        {
            return _context.Relative.Any(e => e.RelId == id);
        }
    }
}
