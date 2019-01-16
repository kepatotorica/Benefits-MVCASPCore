﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Benefacts.Models;
using X.PagedList; //this is an awesome package, it allows you to do pagenation very easily
using Microsoft.AspNetCore.Http;

namespace Benefacts.Controllers
{
    public class UsersController : Controller
    {
        private readonly cSharpContext _context;

        public UsersController(cSharpContext context)
        {
            _context = context;
        }


        /// <summary>
        /// allows us to see users, in paginated form, we can also search and sort the users, a lot of this method
        /// was provided here https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-2.1
        /// but didin't work right away so it had to be changed to work with the app I have built.
        /// </summary>
        /// <param name="search"> the search parameter lol, duh</param>
        /// <param name="sortOrder">the order in which we are going to sort, stored as a bool</param>
        /// <param name="currentSearch"> what the current search value is, for persistence across refreshes </param>
        /// <param name="page"> the paginated page we requested </param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string search, int sortOrder, string currentSearch, int? page)//I think the questionmark is to say it can't be null,
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            ViewData["CurrentSort"] = sortOrder; //reset current sort order, for state preservation
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentSearch; //if we have changed our pagination, or we have no search then reset search to our current search, which is stored in ViewData["CurrentSearch"]
                //this isn't working
            }

            ViewData["CurrentSearch"] = search; //reset current search, for state preservation

            var users = from s in _context.Users //make a list of users
                        select s;

            if (!String.IsNullOrEmpty(search)) // check if what we are searching by is null
            {
                search = search.ToUpper();
                users = users.Where(s => s.LName.ToUpper().Contains(search) || s.FName.ToUpper().Contains(search) || s.Email.ToUpper().Contains(search)); // check last name, first name and email for a match
            }

            switch (sortOrder)
            {
                case 1://first name
                    users = users.OrderBy(s => s.FName);
                    break;
                case 2://last name
                    users = users.OrderBy(s => s.LName);
                    break;
                case 3://email
                    users = users.OrderBy(s => s.Email);
                    break;
                default://gender
                    users = users.OrderBy(s => s.Gender);//PROBABLY don't want to sort by gender, but I have it here for consistency, I may remove it later
                    break;
            }


            int pageSize = 10;//TODO: maybe make it so that a user can enter a page size (number of users)
            int pageNumber = page ?? 1; //return the left side if page isn't null, return 1 otherwize was
            return View(users.ToPagedList(pageNumber, pageSize));//X.PagedList is not asyncronous, so we aren't using await
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UId == id);
            if (users == null)
            {
                return RedirectToAction(nameof(Index));//asdf
            }

            List<Relative> temp = new List<Relative>(); //make a list of relatives

            var relatives = from s in _context.Relative.Where(rel => rel.U.UId == id)//make a list of relatives model instances that point to this user
                        select s;
            
            foreach (Relative rel in relatives)//this for each loop is used to add each relative to our temporary list that will be passed to the view
            {
                temp.Add(rel); // add each relative
            }

            ViewData["Relatives"] = temp;
            if(temp.Count == 0)
            {
                ViewData["Relatives"] = null;
            }
            ViewData["UId"] = users.UId;
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FName,LName,Email,Gender")] Users users)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));//asdf
                return RedirectToAction("Details", "Users", new { id = users.UId });
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return RedirectToAction(nameof(Index));//asdf
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return RedirectToAction(nameof(Index));//asdf
            }
            ViewData["UId"] = id;
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,FName,LName,Email,Gender")] Users users)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id != users.UId)
            {
                return RedirectToAction(nameof(Index));//asdf
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UId))
                    {
                        return RedirectToAction(nameof(Index));//asdf
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));//asdf
                return RedirectToAction("Details", "Users", new { id = users.UId });
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return RedirectToAction(nameof(Index));//asdf
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UId == id);
            if (users == null)
            {
                return RedirectToAction(nameof(Index));//asdf
            }
            ViewData["UId"] = id;
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("AId") < 0)
            {
                return RedirectToAction("Login", "Admins");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null) //shouldn't need this if, but I need to fix the routing
            {
                _context.Relative.RemoveRange(_context.Relative.Where(rel => rel.U.UId == id)); //remove all the relatives of the user we are removing
                _context.Users.Remove(users); //remove the user after clearning it's dependencies
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UId == id);
        }
    }
}
