using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Benefacts.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Benefacts.Controllers
{
    public class AdminsController : Controller
    {
        private readonly cSharpContext _context;

        public AdminsController(cSharpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a view containing a list of all admins
        /// </summary>
        /// <returns> A view containing a list of all admins </returns>
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            return View(await _context.Admin.ToListAsync());
        }

        /// <summary>
        /// Returns a view containing the form for the creation of an admin
        /// </summary>
        /// <returns> A view containing the form for the creation of an admin </returns>
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            return View();
        }

        /// <summary>
        /// The database creation of a new admin
        /// </summary>
        /// <param name="admin"> the admin that we are adding to our database </param>
        /// <returns> Returns the index view </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AId,Username,Password,Email")] Admin admin)
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

        /// <summary>
        /// Returns a form that is prepropigated with the admins data associated with the id, and allows us to edit it
        /// </summary>
        /// <param name="id"> the id of the admin that we are editing </param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (id == null)
            {
                return RedirectToAction(nameof(Index));//asdf;
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return RedirectToAction(nameof(Index));//asdf;
            }
            admin.Password = "";
            return View(admin);
        }

        /// <summary>
        /// The database interaction needed for the editing of an admin
        /// </summary>
        /// <param name="id"> the id of the admin we are editing </param>
        /// <param name="admin"> The admin we are editing </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AId,Username,Password,Email")] Admin admin)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (id != admin.AId)
            {
                return RedirectToAction(nameof(Index));//asdf;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    admin.Password = GetMd5Hash(MD5.Create(), "xfo3ip2a51s23d15g5j" + admin.Password + "$4Lt");
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AId))
                    {
                        return RedirectToAction(nameof(Index));//asdf;
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

        /// <summary>
        /// Serves the form needed for deletion of an admin
        /// </summary>
        /// <param name="id"> id of the admin we are deleting</param>
        /// <returns> a view asking us if we want to delete the admin </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (id == null)
            {
                return RedirectToAction(nameof(Index));//asdf;
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AId == id);
            if (admin == null)
            {
                return RedirectToAction(nameof(Index));//asdf;
            }

            return View(admin);
        }

        /// <summary>
        /// the database interation needed for removing the selected admin
        /// </summary>
        /// <param name="id"> the id of the admin that we are removing </param>
        /// <returns> Redirects us to the index for admins</returns>
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

        /// <summary>
        /// Checks if an admin exists with the specified id
        /// </summary>
        /// <param name="id"> checks to see if an admin exists</param>
        /// <returns> if a user exists </returns>
        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.AId == id);
        }

        /// <summary>
        /// logs out a user. Pretty much just makes there session invalid.
        /// </summary>
        /// <returns> to the login view</returns>
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetInt32("AId", -1);
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// The form used to allow a user to try and log in
        /// </summary>
        /// <returns> A view containing a login form</returns>
        public async Task<IActionResult> Login()
        {
            var attempts = HttpContext.Session.GetInt32("Attempts");
            if(attempts == null)
            {
                HttpContext.Session.SetInt32("Attempts", 0);
            }
            
            HttpContext.Session.SetInt32("AId", -1);
            return View();
        }

        /// <summary>
        /// We hit this method after posting from the login form. We check if our credientials work
        /// if they don't we increase the number of attempts we have used, then it reloads the original
        /// view. If is is successful we are taken to the default Users/Index that shows a list of all
        /// emplyees
        /// </summary>
        /// <param name="admin">  Misleading name, but our attempt at the credentials for an admin</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] Admin admin)
        {
            var attempts = HttpContext.Session.GetInt32("Attempts");//get num attempts
            HttpContext.Session.SetInt32("Attempts", (int)++attempts);//set num attempts

            //find an admin
            var lAdmin = _context.Admin.SingleOrDefault(x => x.Password == GetMd5Hash(MD5.Create(), "xfo3ip2a51s23d15g5j" + admin.Password + "$4Lt") && (x.Username == admin.Username || x.Email == admin.Username));

            if (lAdmin != null) //if there is an admin reset our attempts, log us in, then take us to the users/index
            {
                HttpContext.Session.SetInt32("Attempts", 0);
                HttpContext.Session.SetInt32("AId", lAdmin.AId);
                return RedirectToAction("Index", "Users");
            }
            else //if there is no admin with our credentials, set us to logged out (this should already be the case, but it doesn't hurt to reset it) then return our default view
            {          
                HttpContext.Session.SetInt32("AId", -1);
                return View();
            }
  
        }

        /// <summary>
        /// Builds a hash string that we can use to salt our passwords. I found this at this link
        /// https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?redirectedfrom=MSDN&view=netframework-4.7.2 
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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



