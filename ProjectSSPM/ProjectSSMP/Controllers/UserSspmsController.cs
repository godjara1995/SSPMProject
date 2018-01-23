using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSSMP.Models;

namespace ProjectSSMP.Controllers
{
    public class UserSspmsController : Controller
    {
        private readonly sspmContext _context;

        public UserSspmsController(sspmContext context)
        {
            _context = context;
        }

        // GET: UserSspms
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserSspm.ToListAsync());
        }

        // GET: UserSspms/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSspm = await _context.UserSspm
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userSspm == null)
            {
                return NotFound();
            }

            return View(userSspm);
        }

        // GET: UserSspms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserSspms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password,Firstname,Lastname,UserCreateBy,UserCreateDate,UserEditBy,UserEditDate")] UserSspm userSspm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSspm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userSspm);
        }

        // GET: UserSspms/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSspm = await _context.UserSspm.SingleOrDefaultAsync(m => m.UserId == id);
            if (userSspm == null)
            {
                return NotFound();
            }
            return View(userSspm);
        }

        // POST: UserSspms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,Username,Password,Firstname,Lastname,UserCreateBy,UserCreateDate,UserEditBy,UserEditDate")] UserSspm userSspm)
        {
            if (id != userSspm.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSspm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSspmExists(userSspm.UserId))
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
            return View(userSspm);
        }

        // GET: UserSspms/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSspm = await _context.UserSspm
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userSspm == null)
            {
                return NotFound();
            }

            return View(userSspm);
        }

        // POST: UserSspms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userSspm = await _context.UserSspm.SingleOrDefaultAsync(m => m.UserId == id);
            _context.UserSspm.Remove(userSspm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSspmExists(string id)
        {
            return _context.UserSspm.Any(e => e.UserId == id);
        }
    }
}
