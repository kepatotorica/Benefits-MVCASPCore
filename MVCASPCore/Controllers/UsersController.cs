using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCASPCore.Models;
using X.PagedList; //this is an awesome package, it allows you to do pagenation very easily

namespace MVCASPCore.Controllers
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
            ViewData["CurrentSort"] = sortOrder; //reset current sort order, for state preservation

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentSearch; //if we have changed out pagination, or we have no search then reset search to our current search, which is stored in ViewData["CurrentSearch"]
            }

            ViewData["CurrentSearch"] = search; //reset current search, for state preservation

            var users = from s in _context.Users //make a list of users
                        select s;

            if (!String.IsNullOrEmpty(search)) // check if what we are searching by is null
            {
                users = users.Where(s => s.LName.Contains(search) || s.FName.Contains(search) || s.Email.Contains(search)); // check last name, first name and email for a match TODO make it non case sensitive
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


            int pageSize = 10;//page size TODO make it so that a user can enter a page size
            int pageNumber = page ?? 1; //return the left side if page isn't null, return 1 otherwize was
            return View(users.ToPagedList(pageNumber, pageSize));//X.PagedList is not asyncronous, so we aren't using await
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();//TODO if our id is null, which I don't thik would even happen unless you change the url string manually. Need to fix this 404 error
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UId == id);
            if (users == null)
            {
                return NotFound(); //if our user isn't found, return that, todo make it so that this error is handled more gracefully, currently it is just a 404
            }

            //TODO look into using the colletion provided in the modle.Users instead of this
            List<Relative> temp = new List<Relative>(); //make a list of relatives

            var relatives = from s in _context.Relative.Where(rel => rel.U.UId == id)//make a list of relatives model instances that point to this user
                        select s;

            foreach (Relative rel in relatives)//this for each loop is used to add each relative to our temporary list that will be passed to the view
            {
                temp.Add(rel); // add each relative
            }
            ViewData["Relatives"] = temp;

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UId,FName,LName,Email,Gender")] Users users)
        {
            //TODO for some reason I am trying to submit on UId over a key that already exists.

            //this is the stupidest way to do this, and it must be soooooo slow cause you have to query up until you find a free space
            while (_context.Users.Where(user => user.UId == users.UId) != null) //check to see if there is a user that already has this id
            {
                users.UId++;
            }
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,FName,LName,Email,Gender")] Users users)
        {
            if (id != users.UId)
            {
                return NotFound();
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Relative.RemoveRange(_context.Relative.Where(rel => rel.U.UId == id)); //remove all the relatives of the user we are removing
            _context.Users.Remove(users); //remove the user after clearning it's dependencies
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UId == id);
        }
    }
}
