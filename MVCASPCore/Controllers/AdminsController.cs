using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCASPCore.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace MVCASPCore.Controllers
{
    public class AdminsController : Controller
    {
        private readonly cSharpContext _context;

        public AdminsController(cSharpContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            return View(await _context.Admin.ToListAsync());
        }

        // GET: Admins/Details/5
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

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AId,Username,Password")] Admin admin)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (ModelState.IsValid)
            {
                admin.Password = GetMd5Hash(MD5.Create(), "xfo3ip2a51s23d15g5j" + admin.Password + "$4Lt");
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
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

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AId,Username,Password")] Admin admin)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (id != admin.AId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AId))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
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

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            var admin = await _context.Admin.FindAsync(id);
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.AId == id);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetInt32("AId", -1);
            return RedirectToAction(nameof(Login));
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Login()
        {
            HttpContext.Session.SetInt32("AId", -1);
            return View();
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] Admin admin)
        {
            var lAdmin = _context.Admin.SingleOrDefault(x => x.Password == GetMd5Hash(MD5.Create(), "xfo3ip2a51s23d15g5j" + admin.Password + "$4Lt") && x.Username == admin.Username);
            if(lAdmin != null)
            {
                ViewData["invalid"] = false;
                HttpContext.Session.SetInt32("AId", lAdmin.AId);
                return RedirectToAction("Index", "Users");
                //return View();
            }
            else
            {
                ViewData["invalid"] = true;
                HttpContext.Session.SetInt32("AId", -1);
                return View();
            }
  
        }

        //Build a hash string for the password. https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?redirectedfrom=MSDN&view=netframework-4.7.2 
        string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}



