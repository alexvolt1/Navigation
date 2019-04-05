using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedNav.Data;
using SharedNav.Models;

namespace NavigationWeb.Controllers
{
    public class NavigationItemViewSinglesController : Controller
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemViewSinglesController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: NavigationItemViewSingles
        public async Task<IActionResult> Index()
        {
            return View(await _context.NavigationItemViewSingle.ToListAsync());
        }

        // GET: NavigationItemViewSingles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemViewSingle = await _context.NavigationItemViewSingle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationItemViewSingle == null)
            {
                return NotFound();
            }

            return View(navigationItemViewSingle);
        }

        // GET: NavigationItemViewSingles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavigationItemViewSingles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,TopicId,ViewId,Rtype")] NavigationItemViewSingle navigationItemViewSingle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navigationItemViewSingle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navigationItemViewSingle);
        }

        // GET: NavigationItemViewSingles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemViewSingle = await _context.NavigationItemViewSingle.FindAsync(id);
            if (navigationItemViewSingle == null)
            {
                return NotFound();
            }
            return View(navigationItemViewSingle);
        }

        // POST: NavigationItemViewSingles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,TopicId,ViewId,Rtype")] NavigationItemViewSingle navigationItemViewSingle)
        {
            if (id != navigationItemViewSingle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navigationItemViewSingle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationItemViewSingleExists(navigationItemViewSingle.Id))
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
            return View(navigationItemViewSingle);
        }

        // GET: NavigationItemViewSingles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemViewSingle = await _context.NavigationItemViewSingle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationItemViewSingle == null)
            {
                return NotFound();
            }

            return View(navigationItemViewSingle);
        }

        // POST: NavigationItemViewSingles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var navigationItemViewSingle = await _context.NavigationItemViewSingle.FindAsync(id);
            _context.NavigationItemViewSingle.Remove(navigationItemViewSingle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationItemViewSingleExists(int id)
        {
            return _context.NavigationItemViewSingle.Any(e => e.Id == id);
        }
    }
}
